using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocr.Wrapper.WindowsOcr;
using Ocr.Wrapper.WebAPI.Services;
using Ocr.Wrapper;
using Ocr.Wrapper.TesseractOcr;
using Ocr.Wrapper.AzureOcr;
using Ocr.Wrapper.AwsRekognitionOcr;
using Ocr.Wrapper.GoogleOcr;

namespace OcrFromConsole
{
    [ApiController]
    [Route("[controller]")]
    public class OcrController : ControllerBase
    {
        private readonly MultiOcrRunner multiOcrRunner;

        public OcrController(MultiOcrRunner multiOcrRunner)
        {
            this.multiOcrRunner = multiOcrRunner;
        }

        [HttpPost("Windows")]
        public async Task<IActionResult> GetWindowsOcrResultsAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => multiOcrRunner.GetOcrByType< WindowsOcrService>().GetOcrResultAsync(filePath), 
                file);
           
            return Ok(results[0]);
        }

        [HttpPost("Tesseract")]
        public async Task<IActionResult> GetTesseractOcrResultAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => multiOcrRunner.GetOcrByType<TesseractOcrService>().GetOcrResultAsync(filePath),
                file);

            return Ok(results[0]);
        }

        [HttpPost("Azure")]
        public async Task<IActionResult> GetAzureOcrResultAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => multiOcrRunner.GetOcrByType<AzureOcrService>().GetOcrResultAsync(filePath),
                file);

            return Ok(results[0]);
        }

        [HttpPost("Aws")]
        public async Task<IActionResult> GetAwsOcrResultAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => multiOcrRunner.GetOcrByType<AwsOcrService>().GetOcrResultAsync(filePath),
                file);

            return Ok(results[0]);
        }

        [HttpPost("Google")]
        public async Task<IActionResult> GetGoogleOcrResultAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => multiOcrRunner.GetOcrByType<GoogleOcrService>().GetOcrResultAsync(filePath),
                file);

            return Ok(results[0]);
        }

        [HttpPost("All")]
        public async Task<IActionResult> GetAllOcrResultAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => multiOcrRunner.RunAllOcrEnginesOnImage(filePath),
                file);

            return Ok(results[0]);
        }

        [HttpPost("google_high")]
        public async Task<IActionResult> GetGoogleHighLevelResultAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => multiOcrRunner.RunSingleOcrEngineOnImage<GoogleOcrService>(filePath),
                file);

            return Ok(results[0]);
        }
    }
}
