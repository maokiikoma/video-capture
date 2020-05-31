using System.Threading.Tasks;

namespace VideoCaptureApp.Services
{
    public interface IVideoCaptureService
    {
        (bool result, string message) Validate(string fileName, string outPath, string interval);
        ServiceResultModel Capture(string fileName, string outPath, string interval);
        Task<ServiceResultModel> CaptureAsync(string fileName, string outPath, string interval);
    }
}
