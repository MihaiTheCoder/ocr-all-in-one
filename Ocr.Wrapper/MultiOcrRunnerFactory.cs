using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;
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
        public StandardOcrSettings Settings { get; }
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
            this.Settings = settings;
            this.ocrCache = ocrCache;
            this.imageCompressor = settings.ImageCompressor;
        }

        public async Task<MultiOcrRunner> GetMultiOcrRunner()
        {
            var standardOcrs = await GetStandardOcrs(ocrCache, Settings, imageCompressor);
            return new MultiOcrRunner(standardOcrs.ToArray());
        }


        public static async Task<List<IGenericOcrRunner>> GetStandardOcrs(IOcrCache ocrCache, 
            StandardOcrSettings standardOcrSettings, IImageCompressor imageCompressor)
        {
            List<IGenericOcrRunner> genericOcrRunners = new List<IGenericOcrRunner>();
            var ocrParams = new HighLevelOcrServiceParams { OcrCache = ocrCache, ImageCompressor = imageCompressor };
            if (standardOcrSettings.AwsOcrSettings != null)
            {
                var awsSettings = standardOcrSettings.AwsOcrSettings;
                genericOcrRunners.Add(new AwsOcrService(awsSettings.AccessKey, awsSettings.SecretKey, ocrParams));
            }

            if (standardOcrSettings.AzureOcrSettings != null)
            {
                var azureSettings = standardOcrSettings.AzureOcrSettings;
                genericOcrRunners.Add(new AzureOcrService(azureSettings.SubscriptionKey, azureSettings.Endpoint, ocrParams));
            }

            if (standardOcrSettings.GoogleOcrSettings != null)
            {
                var googleSettings = standardOcrSettings.GoogleOcrSettings;
                genericOcrRunners.Add(new GoogleOcrService(googleSettings.ApiToken, ocrParams));
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
                genericOcrRunners.Add(new TesseractOcrService(tesseractSettings.TesseractDir, tesseractSettings.DataDir, ocrParams));
            }

            if (standardOcrSettings.WindowsOcrSettings != null)
            {
                genericOcrRunners.Add(new WindowsOcrService(ocrParams));
            }
            return genericOcrRunners;
        }

    }
}

