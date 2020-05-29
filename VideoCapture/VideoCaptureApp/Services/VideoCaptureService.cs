using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Lib;

namespace VideoCaptureApp.Services
{
    public class VideoCaptureService : IVideoCaptureService
    {
        public (bool result, string message) Validate(string fileName, string outPath, string interval)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return (false, "動画ファイルを指定してください。");
            }

            if (string.IsNullOrWhiteSpace(outPath))
            {
                return (false, "画像出力先フォルダを指定してください。");
            }

            if (string.IsNullOrWhiteSpace(interval))
            {
                return (false, "画像を抽出する間隔(ミリ秒)を指定してください。");
            }

            if (!int.TryParse(interval, out int result))
            {
                return (false, "画像を抽出する間隔(ミリ秒)には数字を入力してください。");
            }

            return (true, null);
        }

        public ServiceResultModel Capture(string fileName, string outPath, string interval)
        {
            try
            {
                var lib = new VideoLib();
                lib.Exec(fileName, outPath, int.Parse(interval));

                return ServiceResultModel.CreateOkResult();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return ServiceResultModel.CreateErrorResult("Error");
            }
        }
    }
}
