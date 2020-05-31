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

            Debug.WriteLine($"fullPathfileName: {fullPathfileName}");
            Debug.WriteLine($"outPath: {outPath}");
            Debug.WriteLine($"interval: {interval} millisec");

            var fileName = Path.GetFileName(fullPathfileName);

            using (var img = new Mat())
            using (var capture = new VideoCapture(fullPathfileName))
            {
                var frameCount = capture.FrameCount;
                var fps = capture.Fps;
                var secCount = frameCount / fps;
                var capturCount = 1000 / interval * secCount;
                var frameAdditionCount = frameCount / capturCount;

                Debug.WriteLine($"FrameCount: {frameCount}");
                Debug.WriteLine($"FPS: {fps}");
                Debug.WriteLine($"SecCount: {secCount}");
                Debug.WriteLine($"capturCount: {capturCount}");
                Debug.WriteLine($"frameAdditionCount: {frameAdditionCount}");

                var currentCaptureCount = 0;

                for (int i = 0; i < capturCount; i++)
                {
                    try
                    {
                        capture.Set(VideoCaptureProperties.PosFrames, currentCaptureCount);
                        capture.Read(img);
                        Debug.WriteLine($"currentCaptureCount: {currentCaptureCount}");
                        img.SaveImage(Path.Combine(outPath, $"{fileName}_{currentCaptureCount}.png"));

                        currentCaptureCount += (int)Math.Round(frameAdditionCount);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "!_img.empty()")
                        {
                            Debug.WriteLine(ex.Message);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            Debug.WriteLine("end.");
        }
    }
}
