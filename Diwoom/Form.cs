using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diwoom
{
    public partial class DiwoomForm : Form
    {
        public BlueClient client = new BlueClient();
        public List<BluetoothDeviceInfo> btDevices = new List<BluetoothDeviceInfo>();
        public Color color = Color.LightGray;

        Random randomizer = new Random();
        public DiwoomForm()
        {
            InitializeComponent();
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
            if (client.ServerStarted)
            {
                client.StopServer();
                button1.Text = "Start";
            }
            else
            {
                client.StartServer(((int)portSelector.Value));
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
                var im = new Bitmap(open.FileName);
                picturePicker.Image = Util.ResizeBitmap(im, 64);
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
