using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.TesseractOcr
{
    public class TesseractLowLevelOcrService: ILowLevelOcrService<TesseractResponse>
    {
        private readonly string _tesseractExePath;

        public string Name => nameof(TesseractLowLevelOcrService);


        /// <summary>
        /// Initializes a new instance of the <see cref="TesseractService"/> class.
        /// </summary>
        /// <param name="tesseractDir">The path for the Tesseract4 installation folder using our own installer, or using the standard installer (C:\Program Files\Tesseract-OCR).</param>        
        /// <param name="dataDir">The data with the trained models (tessdata). Download the models from https://github.com/tesseract-ocr/tessdata_fast</param>
        public TesseractLowLevelOcrService(string tesseractDir = TesseractInstaller.DefaultInstallDir, string dataDir = null)
        {
            var winTesseractDir = @"C:\Program Files (x86)\Tesseract-OCR";
            if (!Directory.Exists(winTesseractDir))
                _tesseractExePath = "tesseract";

            if (Directory.Exists(winTesseractDir))
            {
                _tesseractExePath = Path.Combine(winTesseractDir, "tesseract.exe");
            }

            if (String.IsNullOrEmpty(dataDir))
                dataDir = Path.Combine(winTesseractDir, "tessdata");

            Environment.SetEnvironmentVariable("TESSDATA_PREFIX", dataDir);
        }

        public async Task<TesseractResponse> GetOcrResultWithoutCacheAsync(string inputFilePath, string language, bool runAnywayWithBadLanguage = true)
        {
            if (language == null)
                language = "eng";

            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            try
            {
                Directory.CreateDirectory(tempPath);
                return await RunTesseract(tempPath, inputFilePath, language);
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        private async Task<TesseractResponse> RunTesseract(string tempPath, string inputFile, string language)
        {
            var tempOutputFile = NewTempFileName(tempPath);

            var info = new ProcessStartInfo
            {
                FileName = _tesseractExePath,
                Arguments = $"{inputFile} {tempOutputFile} -l {language} tsv",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            await RunProcessAsync(info);

            string output = File.ReadAllText(tempOutputFile + ".tsv");
            return TesseractMapper.Get(output, inputFile, Name);
        }

        static Task<int> RunProcessAsync(ProcessStartInfo startInfo)
        {
            var tcs = new TaskCompletionSource<int>();

            var process = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            process.Exited += (sender, args) =>
            {
                if (process.ExitCode == 0)
                    tcs.SetResult(process.ExitCode);
                else
                {
                    var stderr = process.StandardError.ReadToEnd();
                    tcs.SetException(new InvalidOperationException(stderr));
                }
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }

        private static string NewTempFileName(string tempPath)
        {
            return Path.Combine(tempPath, Guid.NewGuid().ToString());
        }
    }
}
