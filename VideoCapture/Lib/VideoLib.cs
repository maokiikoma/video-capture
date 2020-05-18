using System;
using System.Diagnostics;
using System.IO;
using OpenCvSharp;

namespace Lib
{
    public class VideoLib
    {
        const int DefaultInterval = 200; // フレームを取得する間隔（ミリ秒）
        const string DefaultOutPath = @"C:\Users\user\Pictures\capture";

        private string _fileFullName;
        private string _outPath;
        private int _interval;

        public bool Exec(string fullPathfileName, string outPath, int interval)
        {
            if (!ValidateArgAndSetFields(fullPathfileName, outPath, interval))
            {
                return false;
            }

            Debug.WriteLine("capture start");

            try
            {
                var fileName = Path.GetFileName(_fileFullName);
                
                using (var img = new Mat())
                using (var capture = new OpenCvSharp.VideoCapture(_fileFullName))
                {
                    capture.PosFrames = capture.FrameCount - 2;
                    var lastMs = capture.PosMsec * 1000;

                    for (int posMs = 0; posMs < lastMs; posMs += _interval)
                    {
                        capture.PosMsec = posMs;
                        capture.Read(img);
                        img.SaveImage(
                            Path.Combine(
                                _outPath, 
                                $"{fileName}_{new TimeSpan(0, 0, 0, 0, posMs).ToString(@"hh\hmm\mss\sfff\m\s")}.png"));
                    }
                }

                Debug.WriteLine("end.");
            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }

        private bool ValidateArgAndSetFields(string fullPathfileName, string outPath, int interval)
        {
            if(string.IsNullOrWhiteSpace(fullPathfileName)) return false;
            if (!File.Exists(fullPathfileName)) return false;

            _fileFullName = fullPathfileName;

            if (string.IsNullOrWhiteSpace(outPath))
            {
                _outPath = DefaultOutPath;
            }
            else if (!Directory.Exists(outPath))
            {
                return false;
            }
            else
            {
                _outPath = outPath;
            }

            if (interval < 0)
            {
                _interval = DefaultInterval;
            }
            else
            {
                _interval = interval;
            }

            return true;
        }
    }
}
