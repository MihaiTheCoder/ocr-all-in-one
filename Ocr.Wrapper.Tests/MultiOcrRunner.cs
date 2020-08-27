using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task MultiOcrRunnerTestInitialize() 
        {
            StandardOcrSettings standardOcrSettings = new StandardOcrSettings(true)
            {
                WindowsOcrSettings = new WindowsOcrSettings(),
                AzureOcrSettings = new AzureOcrSettings(),
                //GoogleOcrSettings = new GoogleOcrSettings(),
                //TesseractOcrSettings = new TesseractOcrSettings(),
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

        [TestMethod]
        public async Task RunOnBuletine()
        {
            var imgExtenstions = new HashSet<string>(
                        new string[] { "jpg", "png" },
                        StringComparer.OrdinalIgnoreCase);
            var buletine = "../../data/buletine/ita";
            var files = Directory.GetFiles(buletine);
            var imgs = files.Where(f => imgExtenstions.Contains(f.Split('.').Last()));
            foreach (var img in imgs)
            {
                try
                {
                    var result = await multiOcrRunner.RunAllOcrEnginesOnImage(img, Language.Italian);
                    foreach (var kv in result)
                    {
                        var words = kv.Value.Detections.Select(
                            d => $"box({d.BoundingBox.Left}, {d.BoundingBox.Top}, {d.BoundingBox.Height}, {d.BoundingBox.Width}, {Esc(d.DetectedText)}).");
                        File.WriteAllText($"{img}-{kv.Key}.pl", string.Join("\r\n", words));
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            }
        }

        private object Esc(string detectedText)
        {
            return "'" + detectedText.Replace("'", "''") + "'";
        }
    }
}
