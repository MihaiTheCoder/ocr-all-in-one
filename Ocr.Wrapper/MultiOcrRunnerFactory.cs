using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocr.Wrapper.AwsRekognitionOcr;
using Ocr.Wrapper.AzureOcr;
using Ocr.Wrapper.GoogleOcr;
using Ocr.Wrapper.ImageManipulation;
using Ocr.Wrapper.TesseractOcr;
using Ocr.Wrapper.WindowsOcr;

namespace Ocr.Wrapper
{
    public class StandardMultiOcrRunnerFactory
    {
        private readonly StandardOcrSettings settings;
        private readonly IOcrCache ocrCache;
        private readonly IImageCompressor imageCompressor;

        public StandardMultiOcrRunnerFactory(StandardOcrSettings settings) : 
            this(settings, new NoOpOcrCache())
        {

        }

        public StandardMultiOcrRunnerFactory(StandardOcrSettings settings, string cacheRootDirectory) : 
            this(settings, new FileStorageOcrCache(cacheRootDirectory))
        {
            
        }        

        public StandardMultiOcrRunnerFactory(StandardOcrSettings settings, IOcrCache ocrCache)
        {
            this.settings = settings;
            this.ocrCache = ocrCache;
            this.imageCompressor = settings.ImageCompressor;
        }

        public async Task<MultiOcrRunner> GetMultiOcrRunner()
        {
            var standardOcrs = await GetStandardOcrs(ocrCache, settings, imageCompressor);
            return new MultiOcrRunner(standardOcrs.ToArray());
        }


        public static async Task<List<IGenericOcrRunner>> GetStandardOcrs(IOcrCache ocrCache, 
            StandardOcrSettings standardOcrSettings, IImageCompressor imageCompressor)
        {
            List<IGenericOcrRunner> genericOcrRunners = new List<IGenericOcrRunner>();

            if (standardOcrSettings.AwsOcrSettings != null)
            {
                var awsSettings = standardOcrSettings.AwsOcrSettings;
                genericOcrRunners.Add(new AwsOcrService(ocrCache, awsSettings.AccessKey, awsSettings.SecretKey, imageCompressor));
            }

            if (standardOcrSettings.AzureOcrSettings != null)
            {
                var azureSettings = standardOcrSettings.AzureOcrSettings;
                genericOcrRunners.Add(new AzureOcrExecutor(ocrCache, azureSettings.SubscriptionKey, azureSettings.Endpoint, imageCompressor));
            }

            if (standardOcrSettings.GoogleOcrSettings != null)
            {
                var googleSettings = standardOcrSettings.GoogleOcrSettings;
                genericOcrRunners.Add(new GoogleOcrService(ocrCache, googleSettings.ApiToken, imageCompressor));
            }

            if (standardOcrSettings.TesseractOcrSettings != null)
            {                
                var tesseractSettings = standardOcrSettings.TesseractOcrSettings;
                if (tesseractSettings.ShouldTryToAutomaticallInstall)
                {
                    ITesseractInstaller tesseractInstaller = new TesseractInstaller(tesseractSettings.TesseractDir);
                    await tesseractInstaller.Install();
                    tesseractSettings.Installer = tesseractInstaller;
                }
                genericOcrRunners.Add(new TesseractService(ocrCache, tesseractSettings.TesseractDir, tesseractSettings.DataDir, imageCompressor));
            }

            if (standardOcrSettings.WindowsOcrSettings != null)
            {
                genericOcrRunners.Add(new WindowsOcrExecutor(ocrCache, imageCompressor));
            }
            return genericOcrRunners;
        }

    }
}

