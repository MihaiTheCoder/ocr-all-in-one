using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Ocr.Wrapper.AzureOcr;
using Ocr.Wrapper;
using Ocr.Wrapper.WindowsOcr;
using Ocr.Wrapper.TesseractOcr;
using Ocr.Wrapper.GoogleOcr;
using Ocr.Wrapper.AwsRekognitionOcr;
using System.Configuration;
using System.Collections.Generic;

namespace Ocr.Wrapper.Tests
{
    [TestClass()]
    public class OcrTests
    {
        [TestMethod()]
        public async Task AzureOcr()
        {
            var subscriptionKey = ConfigurationManager.AppSettings["azureSubscriptionKey"];
            var endpoint = ConfigurationManager.AppSettings["azureEndpoint"];


            AzureOcrService azureOcrExecutor = new AzureOcrService(subscriptionKey, endpoint);
            AzureOcrResults result = await azureOcrExecutor.GetOcrResultAsync(@"data/abc.JPG");
            Assert.IsNotNull(result);
            GenericOcrResponse genericResult = result.Map();
            Assert.IsNotNull(genericResult);
        }

        [TestMethod()]
        public async Task WindowsOcr()
        {
            WindowsOcrService windowsOcrExecutor = new WindowsOcrService();
            WindowsOcrResult result = await windowsOcrExecutor.GetOcrResultAsync(@"data/abc.JPG", "en");
            Assert.IsNotNull(result);
            GenericOcrResponse genericResult = result.Map();
            Assert.IsNotNull(genericResult);
        }

        [TestMethod]
        public async Task Tesseract5()
        {
            TesseractOcrService tesseractService = new TesseractOcrService();
            TesseractResponse result = await tesseractService.GetOcrResultAsync(@"data/abc.JPG", "eng");
            Assert.IsNotNull(result);
            GenericOcrResponse genericResult = result.Map();
            Assert.IsNotNull(genericResult);
        }

        [TestMethod]
        public async Task GoogleOCR()
        {
            var apiToken = ConfigurationManager.AppSettings["googleApiToken"];
            GoogleOcrService googleOcrService = new GoogleOcrService(apiToken);

            GoogleOcrResponse result = await googleOcrService.GetOcrResultAsync(@"data/abc.JPG");
            var descriptions = result.Responses.SelectMany(r => r.Annotations).ToList();
            Assert.IsNotNull(result);
            GenericOcrResponse genericResult = result.Map();
            Assert.IsNotNull(genericResult);
        }

        [TestMethod]
        public async Task AwsOcr()
        {
            var accessKey = ConfigurationManager.AppSettings["awsAccessKey"];
            var secretKey = ConfigurationManager.AppSettings["awsSecretKey"];
            AwsOcrService awsOcrService = new AwsOcrService(accessKey, secretKey);
            AwsOcrResponse result = await awsOcrService.GetOcrResultAsync(@"data/abc.JPG");
            Assert.IsNotNull(result);
            GenericOcrResponse genericResult = result.Map();
            Assert.IsNotNull(genericResult);
        }

        [TestMethod]
        public async Task RunAllOcrs()
        {
            StandardOcrSettings standardOcrSettings = GetStandardOcrSettings();

            MultiOcrRunner genericOcrRunner = await new StandardMultiOcrRunnerFactory(standardOcrSettings)
                .GetMultiOcrRunner();
            Dictionary<string, GenericOcrResponse> results = await genericOcrRunner.RunAllOcrEnginesOnImage(@"data/abc.JPG");
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task RunOcrsWithCache()
        {
            StandardOcrSettings standardOcrSettings = GetStandardOcrSettings();

            var fullPath = Path.GetFullPath(@"..\Data\Cache\");
            MultiOcrRunner multiOcrRunner = await new StandardMultiOcrRunnerFactory(standardOcrSettings, fullPath)
                .GetMultiOcrRunner();
            Dictionary<string, GenericOcrResponse> results = await multiOcrRunner.RunAllOcrEnginesOnImage(@"data/abc.JPG");
            Assert.IsNotNull(results);
        }

        private static StandardOcrSettings GetStandardOcrSettings()
        {
            var azureSubscriptionKey = ConfigurationManager.AppSettings["azureSubscriptionKey"];
            var azureEndpoint = ConfigurationManager.AppSettings["azureEndpoint"];
            var googleApiToken = ConfigurationManager.AppSettings["googleApiToken"];
            var awsAcessKey = ConfigurationManager.AppSettings["awsAccessKey"];
            var awsSecretKey = ConfigurationManager.AppSettings["awsSecretKey"];
            StandardOcrSettings standardOcrSettings = new StandardOcrSettings(true)
            {
                AwsOcrSettings = new AwsOcrSettings(awsAcessKey, awsSecretKey),
                AzureOcrSettings = new AzureOcrSettings(azureSubscriptionKey, azureEndpoint),
                GoogleOcrSettings = new GoogleOcrSettings(googleApiToken),
                TesseractOcrSettings = new TesseractOcrSettings(),
                WindowsOcrSettings = new WindowsOcrSettings()
            };
            return standardOcrSettings;
        }
    }
}