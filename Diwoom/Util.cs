﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace Diwoom
{
    internal class Util
    {
        private static byte[] Int2B(int p)
        {
            return new byte[] { (byte)(p & 0x00ff), (byte)(p >> 8) };
        }
        private static byte[] FrameChecksum(byte[] bytes)
        {
            return Int2B(bytes.Sum(pp => (int)pp));
        }
        public static SixLabors.ImageSharp.Image ResizeBitmap(SixLabors.ImageSharp.Image im, int size)
        {
            return im.Clone(x => x.Resize(size, size));
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
        public static Bitmap To256PaletteBitmap(SixLabors.ImageSharp.Image image)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                var options = new PngEncoder { Quantizer = new WuQuantizer(new QuantizerOptions { Dither = null, MaxColors = 256 }), ColorType = PngColorType.Palette };
                image.CloneAs<Rgb24>().Save(stream, options);
                stream.Position = 0;
                return (Bitmap)System.Drawing.Image.FromStream(stream);
            }
        }
        public static SixLabors.ImageSharp.Image ImageSharpFromBitmap(Bitmap b)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                b.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                return SixLabors.ImageSharp.Image.Load<Rgb24>(stream);
            }
        }
        public static byte[] DrawBitmap(SixLabors.ImageSharp.Image b, bool use32BitFormat)
        {
            var buffer = new List<byte>();

            var quantizer = new WuQuantizer(new QuantizerOptions { Dither = null, MaxColors = 255 }); // not using 256 in order to simplify
            ImageFrame<Rgb24> frame = b.CloneAs<Rgb24>().Frames.RootFrame;

            using IQuantizer<Rgb24> frameQuantizer = quantizer.CreatePixelSpecificQuantizer<Rgb24>(Configuration.Default);
            using IndexedImageFrame<Rgb24> result = frameQuantizer.BuildPaletteAndQuantizeFrame(frame, frame.Bounds());

            int n_pale = 255; // result.Palette.Length;
            int totalBits = (int)Math.Max(Math.Ceiling(Math.Log2(n_pale)), 1);
            var dataSize = b.Height * b.Width * totalBits / 8;

            var frame_size = result.Palette.Length * 3 + dataSize + (use32BitFormat ? 8 : 7);
            buffer.AddRange(new byte[] {
                0x0, 0x0A, 0x0A, 0x04,
                0xAA, (byte)(frame_size & 0xFF), (byte)((frame_size >> 8) & 0xFF),
                0, 0, (byte)(use32BitFormat ? 3 : 0),
            });


            buffer.AddRange(use32BitFormat ? new byte[] { 255, 0 } : new byte[] { 255 });

            foreach (var entry in result.Palette.Span)
            {
                buffer.AddRange(new byte[] { entry.R, entry.G, entry.B });
            }
            for (var i = result.Palette.Span.Length; i < 255; i++)
            {
                buffer.AddRange(new byte[] { 0, 0, 0 });
            }

            for (int i = 0; i < b.Height; i++)
            {
                var row = result.DangerousGetRowSpan(i);
                for (int j = 0; j < b.Width; j++)
                {
                    buffer.Add(row[j]);
                }
            }

            /*
            buffer.AddRange(use32BitFormat ? new byte[] { n_pale, 0 } : new byte[] { n_pale }); // 256 needs { 0, 1 } 

            foreach (var entry in result.Palette.Span)
            {
                buffer.AddRange(new byte[] { entry.R, entry.G, entry.B });
            }

            int bitCnt = 0;
            ushort currentByte = 0;

            for (int i = 0; i < b.Height; i++)
            {
                var row = result.DangerousGetRowSpan(i);
                for (int j = 0; j < b.Width; j++)
                {
                    var idx = row[j];
                    if (totalBits == 8)
                    {
                        buffer.Add(idx);
                    }
                    else
                    {
                        currentByte <<= totalBits;
                        currentByte |= idx;
                        bitCnt += totalBits;
                        if (bitCnt >= 8)
                        {
                            buffer.Add((byte)(currentByte >> (bitCnt - 8)));
                            var mask = (1 << (bitCnt - 8)) - 1;
                            currentByte = (ushort)(currentByte & mask);
                            bitCnt -= 8;
                        }
                    }
                }
            }
            */

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
    }
}
