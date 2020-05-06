using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsOcrWrapper.AwsRekognitionOcr;
using WindowsOcrWrapper.AzureOcr;
using WindowsOcrWrapper.GoogleOcr;
using WindowsOcrWrapper.TesseractOcr;
using WindowsOcrWrapper.WindowsOcr;

namespace WindowsOcrWrapper
{
    public class GenericOcrRunner
    {
        public List<IGenericOcrRunner> OcrRunners { get; private set; }

        public GenericOcrRunner(StandardOcrSettings settings, params IGenericOcrRunner[] otherGenericOcrRunners )
        {
            OcrRunners = GetStandardOcrs(settings);
            OcrRunners.AddRange(otherGenericOcrRunners.Where(ocr => ocr != null));
        }

        public async Task<Dictionary<string, GenericOcrResponse>> RunAllOcrEnginesOnImage(string inputImagePath, string language=null)
        {
            return await RunAllOcrEnginesOnImage(OcrRunners, inputImagePath, language);
        }

        public static async Task<Dictionary<string, GenericOcrResponse>> RunAllOcrEnginesOnImage(
            IEnumerable<IGenericOcrRunner> ocrEngines,
            string inputImagePath, string language = null)
        {
            var responses = new Dictionary<string, GenericOcrResponse>();
            foreach (var ocr in ocrEngines)
            {
                responses[ocr.Name] = await ocr.RunAsync(inputImagePath, language);
            }
            return responses;
        }

        public static List<IGenericOcrRunner> GetStandardOcrs(StandardOcrSettings standardOcrSettings)
        {
            List<IGenericOcrRunner> genericOcrRunners = new List<IGenericOcrRunner>();

            if (standardOcrSettings.AwsOcrSettings != null)
            {
                var awsSettings = standardOcrSettings.AwsOcrSettings;
                genericOcrRunners.Add(new AwsOcrService(awsSettings.AccessKey, awsSettings.SecretKey));
            }

            if (standardOcrSettings.AzureOcrSettings != null)
            {
                var azureSettings = standardOcrSettings.AzureOcrSettings;
                genericOcrRunners.Add(new AzureOcrExecutor(azureSettings.SubscriptionKey, azureSettings.Endpoint));
            }

            if (standardOcrSettings.GoogleOcrSettings != null)
            {
                var googleSettings = standardOcrSettings.GoogleOcrSettings;
                genericOcrRunners.Add(new GoogleOcrService(googleSettings.ApiToken));
            }

            if (standardOcrSettings.TesseractOcrSettings != null)
            {
                var tesseractSettings = standardOcrSettings.TesseractOcrSettings;
                genericOcrRunners.Add(new TesseractService(tesseractSettings.TesseractDir, tesseractSettings.DataDir));
            }

            if (standardOcrSettings.WindowsOcrSettings != null)
            {
                genericOcrRunners.Add(new WindowsOcrExecutor());
            }
            return genericOcrRunners;
        }
    }

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
        public TesseractOcrSettings(string tesseractDir= @"C:\Program Files\Tesseract-OCR", string dataDir=null)
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
