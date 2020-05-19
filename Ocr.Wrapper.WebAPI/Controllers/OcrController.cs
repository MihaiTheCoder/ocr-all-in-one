using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocr.Wrapper.WindowsOcr;
using Ocr.Wrapper.WebAPI.Services;
using Ocr.Wrapper;

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
    }
}
