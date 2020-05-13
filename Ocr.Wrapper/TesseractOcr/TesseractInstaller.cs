using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.TesseractOcr
{
    public class TesseractInstaller : ITesseractInstaller
    {
        const string webAddress = "https://github.com/MihaiTheCoder/TeseractBinariesZip/blob/master/Tesseract-OCR.zip?raw=true";
        public const string DefaultInstallDir = @"/Tesseract";
        public const string TesseractExeFileName = "tesseract.exe";
        public string TesseractExePath { get { return Path.Combine(TesseractInstallDir, TesseractExeFileName); } }

        public string TesseractInstallDir { get; }

        public TesseractInstaller(string tesseractInstallDir = DefaultInstallDir)
        {
            TesseractInstallDir = tesseractInstallDir;
        }

        public async Task Install()
        {
            var finalExePath = Path.Combine(TesseractInstallDir, TesseractExeFileName);

            if (File.Exists(finalExePath))
                return;

            if (Directory.Exists(TesseractInstallDir))
                Directory.Delete(TesseractInstallDir, true);

            var temporaryDirName = Guid.NewGuid().ToString();

            var temporaryDir = Path.Combine(Directory.GetParent(TesseractInstallDir).FullName, temporaryDirName);
            var temporaryZipFilePath = Path.Combine(temporaryDir, "tesseract-ocr.zip");
            var temporaryUnzipPath = Path.Combine(temporaryDir, "tesseract-ocr");

            Directory.CreateDirectory(temporaryDir);

            HttpClient client = new HttpClient();

            //send  request asynchronously
            HttpResponseMessage response = await client.GetAsync(webAddress);

            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();

            // Read response asynchronously and save asynchronously to file
            using (FileStream fileStream = new FileStream(temporaryZipFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //copy the content from response to filestream
                await response.Content.CopyToAsync(fileStream);
            }

            ZipFile.ExtractToDirectory(temporaryZipFilePath, temporaryDir);
            Directory.Move(temporaryUnzipPath, TesseractInstallDir);
            Directory.Delete(temporaryDir, true);
        }

        public void UninstallTesseract()
        {
            if (Directory.Exists(TesseractInstallDir))
                Directory.Delete(TesseractInstallDir, true);
        }
    }
}
