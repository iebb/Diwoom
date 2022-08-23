using InTheHand.Net.Sockets;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diwoom
{
    public partial class DiwoomForm : Form
    {
        public BlueClient client = new BlueClient();
        public List<BluetoothDeviceInfo> btDevices = new List<BluetoothDeviceInfo>();
        public Color color = Color.LightGray;
        public bool ServerStarted = false;
        long lastMS = 0;

        Random randomizer = new Random();
        HttpListener listener;
        public DiwoomForm()
        {
            InitializeComponent();
        }


        byte[] ImageHandler(HttpListenerContext context)
        {
            using (var memstream = new MemoryStream())
            {
                context.Request.InputStream.CopyTo(memstream);
                memstream.Position = 0;
                var im = SixLabors.ImageSharp.Image.Load(memstream).CloneAs<Rgb24>();
                picturePicker.Image = Util.To256PaletteBitmap(Util.ResizeBitmap(im, 64));
                client.DrawBitmapDevice(im);
            }
            return new byte[] { 65 };
        }
        byte[] RawHandler(HttpListenerContext context)
        {
            using (var memstream = new MemoryStream())
            {
                context.Request.InputStream.CopyTo(memstream);
                var payload = memstream.ToArray();
                client._client.GetStream().Write(payload, 0, payload.Length);
            }
            return new byte[] { 65 };
        }
        byte[] CommandHandler(HttpListenerContext context)
        {
            using (var memstream = new MemoryStream())
            {
                context.Request.InputStream.CopyTo(memstream);
                var payload = memstream.ToArray();
                var cmdStr = context.Request.QueryString.Get("cmd");
                if (Byte.TryParse(cmdStr, out byte cmd))
                {
                    var encoded = Util.BuildCommandArgs(cmd, payload);
                    client._client.GetStream().Write(encoded, 0, encoded.Length);
                }
                else
                {
                    Console.WriteLine("cmd could not be parsed.");
                }
            }
            return new byte[] { 65 };
        }

        private void ProcessRequestHandler(Task<HttpListenerContext> result)
        {
            var context = result.Result;

            if (!listener.IsListening)
                return;

            // Start new listener which replace this
            listener.GetContextAsync().ContinueWith(ProcessRequestHandler);

            var responseBytes = Encoding.UTF8.GetBytes("stuck");
            var timestampMs = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            if (timestampMs - lastMS > 50)
            {
                lastMS = timestampMs + 5000;
                switch (context.Request.Url.LocalPath)
                {
                    case "/cmd":
                        responseBytes = CommandHandler(context);
                        break;
                    case "/img":
                        responseBytes = ImageHandler(context);
                        break;
                    case "/raw":
                    default:
                        responseBytes = RawHandler(context);
                        break;
                }
                lastMS = timestampMs;
            }
            context.Response.ContentLength64 = responseBytes.Length;
            var output = context.Response.OutputStream;
            output.WriteAsync(responseBytes, 0, responseBytes.Length);
            output.Close();
        }

        public void StartServer(int port)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(String.Format("http://127.0.0.1:{0}/", port));
            if (listener.IsListening)
                return;

            listener.Start();
            ServerStarted = true;
            listener.GetContextAsync().ContinueWith(ProcessRequestHandler);
        }
        public void StopServer()
        {
            if (listener.IsListening)
                listener.Stop();
            ServerStarted = false;
        }


        async Task Discover(bool otherDevices)
        {
            await Task.Run(() =>
            {
                progressBar1.BeginInvoke((Action)delegate ()
                {
                    progressBar1.Style = ProgressBarStyle.Marquee;
                });
                btDevices.Clear();
                var devices = client.ConnectedDevices();
                foreach (var device in devices)
                {
                    btDevices.Add(device);
                    // Console.WriteLine(device.DeviceAddress.ToString());
                }
                deviceSelector.BeginInvoke((Action)delegate ()
                {
                    deviceSelector.Items.Clear();
                    // deviceSelector.SelectedIndex = 0;
                    if (btDevices.Count == 0)
                    {
                        btnConnect.Enabled = false;
                    }
                    else
                    {
                        foreach (var device in btDevices)
                        {
                            deviceSelector.Items.Add(device.DeviceName + " [" + device.DeviceAddress.ToString() + "]");
                        }
                        deviceSelector.SelectedIndex = 0;
                    }
                });
                foreach (var device in client.Discover())
                {
                    deviceSelector.BeginInvoke((Action)delegate ()
                    {
                        btnConnect.Enabled = true;
                        btDevices.Add(device);
                        deviceSelector.Items.Add(device.DeviceName + " [" + device.DeviceAddress.ToString() + "]");
                        if (deviceSelector.SelectedIndex == -1)
                        {
                            deviceSelector.SelectedIndex = 0;
                        }
                    });
                    // Console.WriteLine(device.DeviceAddress.ToString());
                }
                progressBar1.BeginInvoke((Action)delegate ()
                {
                    progressBar1.Style = ProgressBarStyle.Continuous;
                    progressBar1.Value = 0;
                });
                //....
            });
        }

        private void Diwoom_Load(object sender, EventArgs e)
        {
            Discover(false);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client.Connect(btDevices[deviceSelector.SelectedIndex]);
            button1.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            Size = new Size(320, 320);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Discover(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (ServerStarted)
            {
                StopServer();
                button1.Text = "Start";
            }
            else
            {
                StartServer(((int)portSelector.Value));
                button1.Text = "Stop";
            }
            button1.Enabled = true;
        }

        private void picturePicker_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var im = SixLabors.ImageSharp.Image.Load(open.FileName).CloneAs<Rgb24>();
                picturePicker.Image = Util.To256PaletteBitmap(Util.ResizeBitmap(im, 64));
                client.DrawBitmapDevice(im);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            client.SendCommand(69, new byte[]{
                0, 1, 0
            });
        }


        private void button5_Click(object sender, EventArgs e)
        {
            client.SendCommand(69, new byte[]{
                1, color.R, color.G, color.B, 100, 0, 1, 0, 0, 0
            });
        }
        private void btnLightning_Click(object sender, EventArgs e)
        {
            client.SendCommand(69, new byte[]{
                1, color.R, color.G, color.B, 100, 1, 1, 0, 0, 0
            });
        }

        private void buttonVis_Click(object sender, EventArgs e)
        {
            client.SendCommand(69, new byte[]{
                4, (byte)randomizer.Next(0, 12),
            });
        }
        private void buttonVJ_Click(object sender, EventArgs e)
        {
            client.SendCommand(69, new byte[]{
                3, (byte)randomizer.Next(0, 16),
            });
        }

        private void buttonAnim_Click(object sender, EventArgs e)
        {
            client.SendCommand(69, new byte[]{
                5
            });
        }

        private void buttonScore_Click(object sender, EventArgs e)
        {
            var scoreRed = ((int)textBoxR.Value);
            var scoreBlue = ((int)textBoxB.Value);
            client.SendCommand(69, new byte[]{
                6, 0,  (byte)(scoreRed & 0xff), (byte)(scoreRed >> 8), (byte)(scoreBlue & 0xff), (byte)(scoreBlue >> 8),
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {

            client.SendCommand(69, new byte[]{
                2
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color;
                button2.BackColor = color;
                client.SendCommand(69, new byte[]{
                    1, color.R, color.G, color.B, 100, 0, 1, 0, 0, 0
                });
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            client.SendCommand(0x74, new byte[]{
                (byte)trackBar1.Value
            });
        }
    }
}
