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
using static WindowsOcrWrapper.MultiOcrRunner;

namespace WindowsOcrWrapper
{
    public class StandardMultiOcrRunnerFactory
    {
        private readonly StandardOcrSettings settings;
        private readonly IOcrCache ocrCache;

        public StandardMultiOcrRunnerFactory(StandardOcrSettings settings, string cacheRootDirectory): 
            this(settings, new FileStorageOcrCache(cacheRootDirectory))
        {
            
        }

        public StandardMultiOcrRunnerFactory(StandardOcrSettings settings, IOcrCache ocrCache)
        {
            this.settings = settings;
            this.ocrCache = ocrCache;
        }

        public MultiOcrRunner GetMultiOcrRunner()
        {
            return new MultiOcrRunner(GetStandardOcrs(ocrCache, settings).ToArray());
        }


        public static List<IGenericOcrRunner> GetStandardOcrs(IOcrCache ocrCache, StandardOcrSettings standardOcrSettings)
        {
            List<IGenericOcrRunner> genericOcrRunners = new List<IGenericOcrRunner>();

            if (standardOcrSettings.AwsOcrSettings != null)
            {
                var awsSettings = standardOcrSettings.AwsOcrSettings;
                genericOcrRunners.Add(new AwsOcrService(ocrCache, awsSettings.AccessKey, awsSettings.SecretKey));
            }

            if (standardOcrSettings.AzureOcrSettings != null)
            {
                var azureSettings = standardOcrSettings.AzureOcrSettings;
                genericOcrRunners.Add(new AzureOcrExecutor(ocrCache, azureSettings.SubscriptionKey, azureSettings.Endpoint));
            }

            if (standardOcrSettings.GoogleOcrSettings != null)
            {
                var googleSettings = standardOcrSettings.GoogleOcrSettings;
                genericOcrRunners.Add(new GoogleOcrService(ocrCache, googleSettings.ApiToken));
            }

            if (standardOcrSettings.TesseractOcrSettings != null)
            {
                var tesseractSettings = standardOcrSettings.TesseractOcrSettings;
                genericOcrRunners.Add(new TesseractService(ocrCache, tesseractSettings.TesseractDir, tesseractSettings.DataDir));
            }

            if (standardOcrSettings.WindowsOcrSettings != null)
            {
                genericOcrRunners.Add(new WindowsOcrExecutor(ocrCache));
            }
            return genericOcrRunners;
        }

    }
}

