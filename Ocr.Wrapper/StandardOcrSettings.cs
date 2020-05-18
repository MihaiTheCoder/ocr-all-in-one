
﻿using System.Configuration;
﻿using Ocr.Wrapper.ImageManipulation;
using Ocr.Wrapper.TesseractOcr;
using System;
using System.IO;

namespace Ocr.Wrapper
{
    public class StandardOcrSettings
    {
        public StandardOcrSettings(bool useStandardImageCompressor=false)
        {
            if (useStandardImageCompressor)
                ImageCompressor = new ImageMagickCompressor(false);
        }

        public StandardOcrSettings(IImageCompressor imageCompressor)
        {
            ImageCompressor = imageCompressor;
        }
        public AzureOcrSettings AzureOcrSettings { get; set; }

        public AwsOcrSettings AwsOcrSettings { get; set; }

        public GoogleOcrSettings GoogleOcrSettings { get; set; }

        public TesseractOcrSettings TesseractOcrSettings { get; set; }

        public WindowsOcrSettings WindowsOcrSettings { get; set; }
        public IImageCompressor ImageCompressor { get; }
    }

    public class AzureOcrSettings
    {
        public AzureOcrSettings()
        {
            SubscriptionKey = ConfigurationManager.AppSettings["azureSubscriptionKey"];
            Endpoint = ConfigurationManager.AppSettings["azureEndpoint"];
        }

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
        public GoogleOcrSettings()
        {
            ApiToken = ConfigurationManager.AppSettings["googleApiToken"];
        }

        public GoogleOcrSettings(string apiToken)
        {
            ApiToken = apiToken;
        }

        public string ApiToken { get; set; }
    }

    public class TesseractOcrSettings
    {

        public TesseractOcrSettings(string tesseractDir = TesseractInstaller.DefaultInstallDir, string dataDir = null,
            bool shouldTryToAutomaticallInstall=true)
        {            
            TesseractDir = tesseractDir;
            DataDir = dataDir;
            ShouldTryToAutomaticallInstall = shouldTryToAutomaticallInstall;
            if (!ShouldTryToAutomaticallInstall && !Directory.Exists(tesseractDir))
                throw new ArgumentException($"Provided path to tesseract -{tesseractDir}- dir does not exist and TesseractInstaller is not set.");
        }

        public string TesseractDir { get; set; }

        public string DataDir { get; set; }
        public bool ShouldTryToAutomaticallInstall { get; }
        public ITesseractInstaller Installer { get; internal set; }
    }

    public class WindowsOcrSettings
    {
        public WindowsOcrSettings()
        {

        }
    }
}
