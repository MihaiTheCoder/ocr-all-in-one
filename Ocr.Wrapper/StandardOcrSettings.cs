
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
        public AwsOcrSettings(string accessKey, string secretKey, string region=null)
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
            Region = region;
        }

        public string AccessKey { get; private set; }

        public string SecretKey { get; private set; }
        public string Region { get; private set; }
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

        public TesseractOcrSettings(string tesseractExecutable = null, string dataDir = null,
            bool shouldTryToAutomaticallInstall=true)
        {            
            TesseractExecutable = tesseractExecutable;
            DataDir = dataDir;
            ShouldTryToAutomaticallInstall = shouldTryToAutomaticallInstall;
            //if (!ShouldTryToAutomaticallInstall && !Directory.Exists(tesseractExecutable))
            //    throw new ArgumentException($"Provided path to tesseract -{tesseractExecutable}- dir does not exist and TesseractInstaller is not set.");
        }

        public string TesseractExecutable { get; set; }

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
