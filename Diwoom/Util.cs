using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Diwoom
{
    internal class Util
    {
        private static byte[] Int2B(int p)
        {
            return new byte[]{(byte)(p & 0x00ff), (byte)(p >> 8)};
        }
        private static byte[] FrameChecksum(byte[] bytes)
        {
            return Int2B(bytes.Sum(pp => (int)pp));
        }
        public static Bitmap ResizeBitmap(Bitmap bmp, int size)
        {
            Bitmap result = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, size, size);
            }

            return result;
        }
        public static byte[] EncodeFrame(byte[] payload)
        {
            var buffer = new List<byte>();
            buffer.Add(0x01);
            buffer.AddRange(payload);
            buffer.AddRange(FrameChecksum(payload));
            buffer.Add(0x02);
            return buffer.ToArray();
        }
        public static byte[] DrawBitmap(Bitmap b, bool is32)
        {
            var buffer = new List<byte>();
            var bitmap = EncodeBitmapPixels(b);
            var frame_size = bitmap.Length + 6;
            buffer.AddRange(new byte[] {
                0x0, 0x0A, 0x0A, 0x04,
                0xAA, (byte)(frame_size & 0xFF), (byte)((frame_size >> 8) & 0xFF),
                0, 0, (byte)(is32 ? 3 : 0),
            });
            buffer.AddRange(bitmap);
            return buffer.ToArray();
        }
        public static byte[] EncodeBitmapPixels(Bitmap b)
        {
            var buffer = new List<byte>();
            var range = new Rectangle(0, 0, b.Width, b.Height);
            Bitmap indexed = b.Clone(range, PixelFormat.Format8bppIndexed);
            Int32 stride;
            int n_pale = indexed.Palette.Entries.Length;
            byte[] rawData = GetImageData(indexed, out stride, true);

            buffer.AddRange(Int2B(n_pale));

            foreach (var entry in indexed.Palette.Entries)
            {
                buffer.AddRange(new byte[] { entry.R, entry.G, entry.B });
            }
            int totalBits = (int)Math.Ceiling(Math.Log(n_pale) / Math.Log(2));
            if (totalBits == 0)
            {
                totalBits = 1;
            }
            int bitCnt = 0;
            ushort currentByte = 0;

            foreach (var idx in rawData)
            {
                currentByte <<= totalBits;
                currentByte |= idx;
                bitCnt += totalBits;
                if (bitCnt >= 8)
                {
                    buffer.Add((byte)(currentByte >> (bitCnt - 8)));
                    currentByte = (ushort)(currentByte & ~(1 << (bitCnt - 8) - 1));
                    bitCnt -= 8;
                }
            }

            return buffer.ToArray();
        }
        public static byte[] BuildCommandArgs(byte cmd, byte[] args)
        {
            var buffer = new List<byte>();
            buffer.AddRange(Int2B(args.Length + 3));
            buffer.Add(cmd);
            buffer.AddRange(args);
            return EncodeFrame(buffer.ToArray());
        }
        public static byte[] Escape(byte[] bytes)
        {
            // Escape the payload. 
            // It is not allowed to have occurrences of the codes 0x01, 0x02 and 0x03.
            // They must be escaped by a leading 0x03 followed by 0x04, 0x05 or 0x06 instead
            List<byte> result = new List<byte>();

            foreach (byte b in bytes)
            {
                if (b == 0x01)
                    result.AddRange(new Byte[] { 0x03, 0x04 });
                else if (b == 0x02)
                    result.AddRange(new Byte[] { 0x03, 0x05 });
                else if (b == 0x03)
                    result.AddRange(new Byte[] { 0x03, 0x06 });
                else
                    result.Add(b);
            }

            return result.ToArray();
        }
        /// <summary>
        /// Gets the raw bytes from an image.
        /// </summary>
        /// <param name="sourceImage">The image to get the bytes from.</param>
        /// <param name="stride">Stride of the retrieved image data.</param>
        /// <param name="collapseStride">Collapse the stride to the minimum required for the image data.</param>
        /// <returns>The raw bytes of the image.</returns>
        public static Byte[] GetImageData(Bitmap sourceImage, out Int32 stride, Boolean collapseStride)
        {
            if (sourceImage == null)
                throw new ArgumentNullException("sourceImage", "Source image is null!");
            Int32 width = sourceImage.Width;
            Int32 height = sourceImage.Height;
            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            stride = sourceData.Stride;
            Byte[] data;
            if (collapseStride)
            {
                Int32 actualDataWidth = ((Image.GetPixelFormatSize(sourceImage.PixelFormat) * width) + 7) / 8;
                Int64 sourcePos = sourceData.Scan0.ToInt64();
                Int32 destPos = 0;
                data = new Byte[actualDataWidth * height];
                for (Int32 y = 0; y < height; ++y)
                {
                    Marshal.Copy(new IntPtr(sourcePos), data, destPos, actualDataWidth);
                    sourcePos += stride;
                    destPos += actualDataWidth;
                }
                stride = actualDataWidth;
            }
            else
            {
                data = new Byte[stride * height];
                Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            }
            sourceImage.UnlockBits(sourceData);
            return data;
        }
    }
}
