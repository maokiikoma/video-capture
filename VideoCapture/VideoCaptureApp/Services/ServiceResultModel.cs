namespace VideoCaptureApp.Services
{
    public class ServiceResultModel
    {
        public bool Result { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ServiceResultModel CreateOkResult() => new ServiceResultModel {Result = true};

        public static ServiceResultModel CreateErrorResult(string errorMessage) =>
            new ServiceResultModel {Result = false, ErrorMessage = errorMessage};
    }
}
