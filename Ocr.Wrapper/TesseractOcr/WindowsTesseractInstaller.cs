using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ocr.Wrapper.TesseractOcr
{
    public class WindowsTesseractInstaller : ITesseractInstaller
    {
        const string webAddress = "https://github.com/MihaiTheCoder/TeseractBinariesZip/blob/master/Tesseract-OCR.zip?raw=true";        
        public const string TesseractExeFileName = "tesseract.exe";
        public const string DefaultInstalledPath = @"/Tesseract/" + TesseractExeFileName;        

        public string TesseractInstallDir { get; }

        public string TesseractExePath { get; }

        public WindowsTesseractInstaller(string tesseractExecutable = DefaultInstalledPath)
        {
            if (tesseractExecutable == null)
                tesseractExecutable = @"C:\Program Files (x86)\Tesseract-OCR\" + TesseractExeFileName;

            if (!File.Exists(tesseractExecutable))
                tesseractExecutable = DefaultInstalledPath;                


            TesseractInstallDir = Directory.GetParent(tesseractExecutable).FullName;
            TesseractExePath = tesseractExecutable;
        }

        public async Task Install()
        {
            if (File.Exists(TesseractExePath))
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
