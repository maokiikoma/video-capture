using System;
using System.Diagnostics;
using System.IO;
using OpenCvSharp;

namespace Lib
{
    public class VideoLib : IVideoLib
    {
        public void ExtractImage(string fullPathfileName, string outPath, int interval)
        {
            Debug.WriteLine("capture start");
            PrintParameter(fullPathfileName, outPath, interval);

            var fileName = Path.GetFileName(fullPathfileName);

            using (var img = new Mat())
            using (var capture = new VideoCapture(fullPathfileName))
            {
                var secCount = capture.FrameCount / capture.Fps;
                var capturCount = 1000 / interval * secCount;
                var frameAdditionCount = capture.FrameCount / capturCount;

                PrintVideoInfo(capture.FrameCount, capture.Fps, secCount, capturCount, frameAdditionCount);

                var currentCaptureCount = 0;

                for (int i = 0; i < capturCount; i++)
                {
                    try
                    {
                        Debug.WriteLine($"currentCaptureCount: {currentCaptureCount}");

                        var currentTime = TimeSpan.FromMilliseconds(i * interval);
                        capture.Set(VideoCaptureProperties.PosFrames, currentCaptureCount);
                        capture.Read(img);
                        img.SaveImage(Path.Combine(outPath, $"{fileName}_{currentTime:hh\\-mm\\-ss\\-fff}.png"));

                        currentCaptureCount += (int)Math.Round(frameAdditionCount);
                    }
                    catch (OpenCVException ex)
                    {
                        if (ex.Message == "!_img.empty()")
                        {
                            Debug.WriteLine("skip. empty image frame.");
                            currentCaptureCount += (int)Math.Round(frameAdditionCount);
                            continue;
                        }
                        throw;
                    }
                }
            }

            Debug.WriteLine("end.");
        }

        private void PrintParameter(string fullPathfileName, string outPath, int interval)
        {
            Debug.WriteLine($"fullPathfileName: {fullPathfileName}");
            Debug.WriteLine($"outPath: {outPath}");
            Debug.WriteLine($"interval: {interval} millisec");
        }

        private void PrintVideoInfo(int frameCount, double fps, double secCount, double capturCount, double frameAdditionCount)
        {
            Debug.WriteLine($"FrameCount: {frameCount}");
            Debug.WriteLine($"FPS: {fps}");
            Debug.WriteLine($"SecCount: {secCount}");
            Debug.WriteLine($"capturCount: {capturCount}");
            Debug.WriteLine($"frameAdditionCount: {frameAdditionCount}");
        }
    }
}
