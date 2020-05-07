using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsOcrWrapper;
using WindowsOcrWrapper.WindowsOcr;

namespace WindowsOcrWrapperTests
{
    [TestClass]
    public class WindowsOcrExecutorTests
    {
        private string path = System.IO.Path.GetFullPath(@"data\TLCShot.png");

        WindowsOcrExecutor windowsOcrExecutor = new WindowsOcrExecutor(new NoOpOcrCache());

        [TestMethod]
        public void works_with_good_lang()
        {
            var res = windowsOcrExecutor.GetOcrResult(path, "en-US", true);

            Assert.IsNotNull(res);
            var isOutputEmpty = string.IsNullOrEmpty(windowsOcrExecutor.debugPsOutput.ToString());
            Assert.IsTrue(isOutputEmpty);
        }

        [TestMethod]
        public void works_with_bad_lang()
        {
            WindowsOcrExecutor windowsOcrExecutor = new WindowsOcrExecutor(new NoOpOcrCache());
            var res = windowsOcrExecutor.GetOcrResult(path, "_BAD_LANGUAGE_", true);

            Assert.IsNotNull(res);
            var isOutputEmpty = string.IsNullOrEmpty(windowsOcrExecutor.debugPsOutput.ToString());
            Assert.IsTrue(isOutputEmpty);
        }

        [TestMethod]
        public void fails_with_bad_lang()
        {
            try
            {
                var res = windowsOcrExecutor.GetOcrResult(path, "_BAD_LANGUAGE_", false);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e)
            {
                var isOutputEmpty = string.IsNullOrEmpty(windowsOcrExecutor.debugPsOutput.ToString());
                Assert.IsFalse(isOutputEmpty);
            }
        }
    }
}
