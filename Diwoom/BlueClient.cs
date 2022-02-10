using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;


namespace Diwoom
{
    public class BlueClient
    {
        HttpListener listener;
        public bool ServerStarted = false;

        BluetoothClient _client = new BluetoothClient();
        BluetoothDeviceInfo _device;
        long lastMS = 0;
        bool connected = false;

        public IReadOnlyCollection<BluetoothDeviceInfo> Discover()
        {
            return _client.DiscoverDevices();
        }

        public void Connect(BluetoothDeviceInfo device)
        {
            if (!connected)
            {
                _device = device;
                try
                {
                    _client.Connect(_device.DeviceAddress, BluetoothService.SerialPort);
                }
                finally
                {
                    connected = true;
                }
            }
        }

        byte[] ImageHandler(HttpListenerContext context)
        {
            using (var memstream = new MemoryStream())
            {
                context.Request.InputStream.CopyTo(memstream);
                try
                {
                    var im = Image.FromStream(memstream);
                    DrawBitmapDevice((Bitmap)im);
                } catch
                {
                    return Encoding.UTF8.GetBytes("malformed");
                }
            }
            return new byte[] { 65 };
        }
        byte[] RawHandler(HttpListenerContext context)
        {
            using (var memstream = new MemoryStream())
            {
                context.Request.InputStream.CopyTo(memstream);
                var payload = memstream.ToArray();
                _client.GetStream().Write(payload, 0, payload.Length);
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
                    _client.GetStream().Write(encoded, 0, encoded.Length);
                }
                else
                {
                    Console.WriteLine("cmd could not be parsed.");
                }
            }
            return new byte[] { 65 };
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
        public void SendCommand(byte cmd, byte[] payload)
        {
            if (connected)
            {
                var encoded = Util.BuildCommandArgs(cmd, payload);
                _client.GetStream().Write(encoded, 0, encoded.Length);
            }
        }
        public void DrawBitmapDevice(Bitmap im)
        {
            var is32 = _device.DeviceName == "Pixoo-Max";
            var b = Util.ResizeBitmap(im, is32 ? 32 : 16);
            var payload = Util.DrawBitmap(b, is32);
            SendCommand(68, payload);
        }

        public void StopServer()
        {
            if (listener.IsListening)
                listener.Stop();
            ServerStarted = false;
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
    }

}
