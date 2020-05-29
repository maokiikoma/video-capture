namespace VideoCaptureApp.Services
{
    public interface IVideoCaptureService
    {
        (bool result, string message) Validate(string fileName, string outPath, string interval);
        ServiceResultModel Capture(string fileName, string outPath, string interval);
    }
}
