using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocr.Wrapper.WindowsOcr;

namespace Ocr.Wrapper.Tests
{
    [TestClass]
    public class WindowsOcrExecutorTests
    {
        private string abc = System.IO.Path.GetFullPath(@"data\abc.jpg");
        private string path = System.IO.Path.GetFullPath(@"data\TLCShot.png");

        

        [TestMethod]
        public void works_with_good_lang()
        {
            WindowsOcrService windowsOcrExecutor = new WindowsOcrService();
            var res = windowsOcrExecutor.GetOcrResult(path, "en-US", false);

            Assert.IsNotNull(res);
            var isOutputEmpty = string.IsNullOrEmpty(windowsOcrExecutor.DebugPsOutput);
            Assert.IsTrue(isOutputEmpty);
        }

        [TestMethod]
        public void works_with_bad_lang()
        {
            WindowsOcrService windowsOcrExecutor = new WindowsOcrService();
            var res = windowsOcrExecutor.GetOcrResult(path, "_BAD_LANGUAGE_", true);

            Assert.IsNotNull(res);
            var isOutputEmpty = string.IsNullOrEmpty(windowsOcrExecutor.DebugPsOutput);
            Assert.IsTrue(isOutputEmpty);
        }

        [TestMethod]
        public void fails_with_bad_lang()
        {
            WindowsOcrService windowsOcrExecutor = new WindowsOcrService();
            try
            {
                var res = windowsOcrExecutor.GetOcrResult(path, "_BAD_LANGUAGE_", false);
                Assert.Fail();
            }
            catch (Exception e)
            {
                var isOutputEmpty = string.IsNullOrEmpty(windowsOcrExecutor.DebugPsOutput);
                Assert.IsFalse(isOutputEmpty);
            }
        }

        [TestMethod]
        public void reads_abc()
        {
            WindowsOcrService windowsOcrExecutor = new WindowsOcrService();
            var res = windowsOcrExecutor.GetOcrResult(abc, "ro", true);
            var text = string.Join("\r\n", res.Lines.Select(l => l.Text));
            var expected =
@"abc
DEF
Total
3";
            Assert.AreEqual(expected, text);
        }
    }
}
