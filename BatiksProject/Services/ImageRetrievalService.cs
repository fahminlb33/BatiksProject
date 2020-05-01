using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BatiksProject.Models;
using OpenCvSharp;

namespace BatiksProject.Services
{
    public interface IImageRetrievalService
    {
        IEnumerable<Batik> GetMostSimilar(float[] query, IEnumerable<Batik> collection);
        float[] Describe(string path);
        float[] Describe(Stream stream);
    }

    public class ImageRetrievalService : IImageRetrievalService
    {
        public IEnumerable<Batik> GetMostSimilar(float[] query, IEnumerable<Batik> collection)
        {
            return collection.OrderBy(x => CosineSimilarity(query, x.Features)).Take(6);
        }

        public float[] Describe(string path)
        {
            using var fs = new FileStream(path, FileMode.Open);
            return Describe(fs);
        }

        public float[] Describe(Stream stream)
        {
            
            using var raw = Mat.FromStream(stream, ImreadModes.Color);
            using var resized = raw.Resize(new Size(244, 244));
            using var image = resized.CvtColor(ColorConversionCodes.BGR2HSV);

            int width = image.Width, height = image.Height;
            int centerX = (int) (width * 0.5), centerY = (int) (height * 0.5);
            var segments = new List<Segment>
            {
                new Segment(0, centerX, 0, centerY),
                new Segment(centerX, width, 0, centerY),
                new Segment(centerX, width, centerY, height),
                new Segment(0, centerX, centerY, height)
            };

            int axesX = (int) Math.Floor(width * 0.375), axesY = (int) Math.Floor(height * 0.375);
            using var ellipseMask = (Mat) Mat.Zeros(new Size(width, height), MatType.CV_8U);
            Cv2.Ellipse(ellipseMask, new Point(centerX, centerY), new Size(axesX, axesY), 0, 0, 360, 255, -1);

            var features = new List<float[]>();
            float[] histogramVector;
            foreach (Segment segment in segments)
            {
                using var cornerMask = (Mat) Mat.Zeros(new Size(width, height), MatType.CV_8U);
                Cv2.Rectangle(cornerMask, new Point(segment.StartX, segment.StartY),
                    new Point(segment.EndX, segment.EndY), 255, -1);
                Cv2.Subtract(cornerMask, ellipseMask, cornerMask);

                histogramVector = Histogram(image, cornerMask);
                features.Add(histogramVector);
            }

            histogramVector = Histogram(image, ellipseMask);
            features.Add(histogramVector);

            return features.SelectMany(x => x).ToArray();
        }

        private float[] Histogram(Mat image, Mat mask)
        {
            using var hist = new Mat<float>();
            var histSize = new[] {8, 12, 3};
            var ranges = new[] {new Rangef(0, 180), new Rangef(0, 256), new Rangef(0, 256)};

            Cv2.CalcHist(new[] {image}, new[] {0, 1, 2}, mask, hist, 3, histSize, ranges);
            Cv2.Normalize(hist, hist);
            
            return hist.Reshape(1).ToArray();
        }

        private double CosineSimilarity(IEnumerable<float> a, IEnumerable<float> b)
        {
            const double c = 1e-10;
            double sum = 0;
            foreach (var (x, y) in a.Zip(b))
            {
                sum += Math.Pow(x - y, 2) / (x + y + c);
            }

            return sum / 2;
        }

        private class Segment
        {
            public int StartX { get; }
            public int EndX { get; }
            public int StartY { get; }
            public int EndY { get; }

            public Segment(int startX, int endX, int startY, int endY)
            {
                StartX = startX;
                EndX = endX;
                StartY = startY;
                EndY = endY;
            }
        }
    }
}
