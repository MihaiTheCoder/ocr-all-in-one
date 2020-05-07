using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsOcrWrapper;
using System.IO;
using System.Threading.Tasks;

namespace WindowsOcrWrapperTests
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
                TesseractOcrSettings = new TesseractOcrSettings(),
                WindowsOcrSettings = new WindowsOcrSettings()
            };
            var fullPath = Path.GetFullPath(@"..\Data\Cache\");
            multiOcrRunner = new StandardMultiOcrRunnerFactory(standardOcrSettings, fullPath).GetMultiOcrRunner();
        }
        
        #endregion

        [TestMethod]
        public async Task RunAll()
        {
            var filePath = "data/TLCShot.png";
            var result = await multiOcrRunner.RunAllOcrEnginesOnImage(filePath);

            Assert.IsNotNull(result);
        }
    }
}
