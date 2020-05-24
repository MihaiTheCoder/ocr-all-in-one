using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using Ocr.Wrapper.TesseractOcr;

namespace Ocr.Wrapper.Tests
{
    /// <summary>
    /// Summary description for MultiOcrRunner
    /// </summary>
    [TestClass]
    public class MultiOcrRunnerTests
    {

        MultiOcrRunner multiOcrRunner;

        #region Additional test attributes

        [TestInitialize()]
        public async Task MultiOcrRunnerTestInitialize() 
        {
            StandardOcrSettings standardOcrSettings = new StandardOcrSettings(true)
            {                
                WindowsOcrSettings = new WindowsOcrSettings(),
                AzureOcrSettings = new AzureOcrSettings(),
                GoogleOcrSettings = new GoogleOcrSettings(),
                TesseractOcrSettings = new TesseractOcrSettings(),
            };
            var fullPath = Path.GetFullPath(@"..\Data\Cache\");
            multiOcrRunner = await new StandardMultiOcrRunnerFactory(standardOcrSettings, fullPath).GetMultiOcrRunner();
        }
        
        #endregion

        [TestMethod]
        public async Task RunAll()
        {
            var filePath = "data/TLCShot.png";
            var result = await multiOcrRunner.RunAllOcrEnginesOnImage(filePath, Language.English);

               Assert.IsNotNull(result);
        }        
    }
}
