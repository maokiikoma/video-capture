using System;
using Lib;

namespace VideoCapture
{
    class Program
    {
        const int DefaultInterval = 200; // フレームを取得する間隔（ミリ秒）
        const string DefaultOutPath = @"C:\Users\user\Pictures\capture";

        static void Main(string[] args)
        {
            string fullFileName = null;
            string outPath = null;
            int interval = -1;
            try
            {
                switch (args.Length)
                {
                    case 3:
                        fullFileName = args[0];
                        outPath = args[1];
                        interval = int.Parse(args[2]);
                        break;
                    case 2:
                        fullFileName = args[0];
                        outPath = args[1];
                        break;
                    case 1:
                        fullFileName = args[0];
                        break;
                    default:
                        PrintHelpMessage();
                        return;
                }

                var lib = new VideoLib();
                lib.ExtractImage(fullFileName, outPath, interval);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static void PrintHelpMessage()
        {
            Console.WriteLine("arg0: fileFullName.");
            Console.WriteLine($"arg1: outPath.  default value: {DefaultOutPath}");
            Console.WriteLine($"arg2: interval(milliseconds).  default value: {DefaultInterval}");
        }
    }
}
