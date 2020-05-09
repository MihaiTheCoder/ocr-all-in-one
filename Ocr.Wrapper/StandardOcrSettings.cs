namespace Ocr.Wrapper
{
    public class StandardOcrSettings
    {
        public AzureOcrSettings AzureOcrSettings { get; set; }
        public AwsOcrSettings AwsOcrSettings { get; set; }

        public GoogleOcrSettings GoogleOcrSettings { get; set; }

        public TesseractOcrSettings TesseractOcrSettings { get; set; }

        public WindowsOcrSettings WindowsOcrSettings { get; set; }
    }

    public class AzureOcrSettings
    {
        public AzureOcrSettings(string subscriptionKey, string endpoint)
        {
            SubscriptionKey = subscriptionKey;
            Endpoint = endpoint;
        }

        public string SubscriptionKey { get; private set; }

        public string Endpoint { get; private set; }
    }

    public class AwsOcrSettings
    {
        public AwsOcrSettings(string accessKey, string secretKey)
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
        }

        public string AccessKey { get; private set; }

        public string SecretKey { get; private set; }
    }

    public class GoogleOcrSettings
    {
        public GoogleOcrSettings(string apiToken)
        {
            ApiToken = apiToken;
        }

        public string ApiToken { get; set; }
    }

    public class TesseractOcrSettings
    {
        public TesseractOcrSettings(string tesseractDir = @"C:\Program Files\Tesseract-OCR", string dataDir = null)
        {
            TesseractDir = tesseractDir;
            DataDir = dataDir;
        }

        public string TesseractDir { get; set; }

        public string DataDir { get; set; }
    }

    public class WindowsOcrSettings
    {
        public WindowsOcrSettings()
        {

        }
    }
}
