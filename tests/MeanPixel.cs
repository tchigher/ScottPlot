﻿using System;

namespace ScottPlotTests
{
    public class MeanPixel
    {
        public readonly double A, R, G, B;
        public double RGB { get => (R + G + B) / 3; }
        public readonly int pixels;

        public MeanPixel(System.Drawing.Bitmap bmp)
        {
            (A, R, G, B) = TestTools.MeanPixel(bmp);
            pixels = bmp.Width * bmp.Height;
        }

        public override string ToString()
        {
            string[] parts = { "R", "=", "G", "=", "B", "=", "R" };
            if (R < G) parts[1] = "<";
            if (R > G) parts[1] = ">";
            if (G < B) parts[3] = "<";
            if (G > B) parts[3] = ">";
            if (B < R) parts[5] = "<";
            if (B > R) parts[5] = ">";
            string comparisons = string.Join("", parts);
            string rgbVals = $"R={Math.Round(R, 3)}, G={Math.Round(G, 3)}, B={Math.Round(B, 3)}";
            return $"{pixels} pixels: {rgbVals} ({comparisons}) total={Math.Round(RGB, 3)}";
        }

        public bool IsEqualTo(System.Drawing.Bitmap bmp)
        {
            (_, double r, double g, double b) = TestTools.MeanPixel(bmp);
            return (R == r) && (G == g) && (B == b);
        }

        public bool IsDarkerThan(MeanPixel comparison) => RGB < comparison.RGB;

        public bool IsGray() => (R == G) && (G == B) && (B == R);

        public bool IsNotGray() => !IsGray();

        public bool IsMoreBlueThan(MeanPixel comparison) => B > comparison.B;
    }
}
