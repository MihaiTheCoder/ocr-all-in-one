using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

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
        public void MultiOcrRunnerTestInitialize() 
        {
            StandardOcrSettings standardOcrSettings = new StandardOcrSettings
            {                
                WindowsOcrSettings = new WindowsOcrSettings(),
                AzureOcrSettings = new AzureOcrSettings(),
                GoogleOcrSettings = new GoogleOcrSettings(),
                TesseractOcrSettings = new TesseractOcrSettings(),
            };
            var fullPath = Path.GetFullPath(@"..\Data\Cache\");
            multiOcrRunner = new StandardMultiOcrRunnerFactory(standardOcrSettings, fullPath).GetMultiOcrRunner();
        }
        
        #endregion

        [TestMethod]
        public async Task RunAll()
        {
            //var filePath = "data/TLCShot.png";
            var filePath = @"C:\Ioana\Facturi\IMG_20200113_215442.jpg";
            var result = await multiOcrRunner.RunAllOcrEnginesOnImage(filePath, "ron");

            Assert.IsNotNull(result);
        }
    }
}
