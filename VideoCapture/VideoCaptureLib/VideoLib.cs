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

            try
            {
                var fileName = Path.GetFileName(fullPathfileName);

                using (var img = new Mat())
                using (var capture = new VideoCapture(fullPathfileName))
                {
                    capture.PosFrames = capture.FrameCount - 2;
                    var lastMs = capture.PosMsec * 1000;

                    for (int posMs = 0; posMs < lastMs; posMs += interval)
                    {
                        capture.PosMsec = posMs;
                        capture.Read(img);
                        img.SaveImage(
                            Path.Combine(
                                outPath,
                                $"{fileName}_{new TimeSpan(0, 0, 0, 0, posMs):hh\\hmm\\mss\\sfff\\m\\s}.png"));
                    }
                }

                Debug.WriteLine("end.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
