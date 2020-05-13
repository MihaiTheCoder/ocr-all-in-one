using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocr.Wrapper.TesseractOcr;

namespace Ocr.Wrapper.Tests
{
    [TestClass]
    public class TesseractInstallerTests
    {
        TesseractInstaller installer;

        [TestInitialize()]
        public void Setup()
        {
            installer = new TesseractInstaller();
        }

        [TestMethod]
        public async Task Install_Uninstall_Tesseract()
        {
            await installer.Install();
            Assert.IsTrue(File.Exists(installer.TesseractExePath));
            await installer.Install(); //It should work even if the program is already installed
            Assert.IsTrue(File.Exists(installer.TesseractExePath));

            installer.UninstallTesseract();
            Assert.IsFalse(Directory.Exists(installer.TesseractInstallDir));
            installer.UninstallTesseract();//It should work even if the program is already uninstalled
            Assert.IsFalse(Directory.Exists(installer.TesseractInstallDir));
        }
    }
}
