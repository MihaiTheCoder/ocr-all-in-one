using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper.TesseractOcr
{
    // <summary>
    /// Service to read texts from images through OCR Tesseract engine.
    /// http://diegogiacomelli.com.br/using-tesseract4-with-csharp/
    /// </summary>
    public class TesseractService : IGenericOcrRunner
    {
        private readonly string _tesseractExePath;

        public string Name => nameof(TesseractService);

        /// <summary>
        /// Initializes a new instance of the <see cref="TesseractService"/> class.
        /// </summary>
        /// <param name="tesseractDir">The path for the Tesseract4 installation folder (C:\Program Files\Tesseract-OCR).</param>        
        /// <param name="dataDir">The data with the trained models (tessdata). Download the models from https://github.com/tesseract-ocr/tessdata_fast</param>
        public TesseractService(string tesseractDir= @"C:\Program Files\Tesseract-OCR", string dataDir = null)
        {
            // Tesseract configs.
            _tesseractExePath = Path.Combine(tesseractDir, "tesseract.exe");            

            if (String.IsNullOrEmpty(dataDir))
                dataDir = Path.Combine(tesseractDir, "tessdata");

            Environment.SetEnvironmentVariable("TESSDATA_PREFIX", dataDir);
        }

        public async Task<TesseractResponse> GetOcrResultAsync(string inputFilePath, string language)
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
            return TesseractMapper.Get(output);
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

        public async Task<GenericOcrResponse> RunAsync(string inputImage, string inputLanguage = null)
        {
            return await GetOcrResultAsync(inputImage, inputLanguage);
        }
    }
}
