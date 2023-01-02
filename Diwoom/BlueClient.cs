using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Diwoom
{
    public class BlueClient
    {

        public BluetoothClient _client = new BluetoothClient();
        BluetoothDeviceInfo _device;
        bool connected = false;

        string DivoomPrefix = "117558";

        public List<BluetoothDeviceInfo> Discover()
        {
            List<BluetoothDeviceInfo> l = new List<BluetoothDeviceInfo>();
            foreach (var device in _client.DiscoverDevices())
            {
                if (device.DeviceAddress.ToString().StartsWith(DivoomPrefix) && !l.Contains(device)) l.Add(device);
            }
            return l;
        }
        public List<BluetoothDeviceInfo> ConnectedDevices()
        {
            List<BluetoothDeviceInfo> l = new List<BluetoothDeviceInfo>();
            foreach (var device in _client.PairedDevices)
            {
                if (device.DeviceAddress.ToString().StartsWith(DivoomPrefix)) l.Add(device);
            }
            return l;
        }

        public void Connect(BluetoothDeviceInfo device)
        {
            if (connected)
            {
                _client.Close();
            }
            _device = device;
            try
            {
                _client.Connect(_device.DeviceAddress, BluetoothService.SerialPort);
                connected = true;
            }
            catch
            {
                connected = false;
            }
        }

        public void SendCommand(byte cmd, byte[] payload)
        {
            if (connected)
            {
                var encoded = Util.BuildCommandArgs(cmd, payload);
                _client.GetStream().Write(encoded, 0, encoded.Length);
            }
        }
        public void DrawBitmapDevice(SixLabors.ImageSharp.Image im)
        {
            var is32 = _device.DeviceName.Contains("Pixoo-Max");
            var b = Util.ResizeBitmap(im, is32 ? 32 : 16);
            var payload = Util.DrawBitmap(b, is32);
            SendCommand(68, payload);
        }


    }

}
