using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocr.Wrapper.WindowsOcr;
using System.Linq;
using Ocr.Wrapper.WebAPI.Services;

namespace OcrFromConsole
{
    [ApiController]
    [Route("[controller]")]
    public class OcrController : ControllerBase
    {
        private readonly WindowsOcrService windowsOcrExecutor;

        public OcrController(WindowsOcrService windowsOcrExecutor)
        {
            this.windowsOcrExecutor = windowsOcrExecutor;
        }

        [HttpPost("Windows")]
        public async Task<IActionResult> GetWindowsOcrResultsAsync(IFormFile file)
        {
            var results = await AspNetFileProcessorWrapper.ProcessFilesAsync(
                filePath => windowsOcrExecutor.GetOcrResultAsync(filePath), 
                file);
           
            return Ok(results[0]);
        }        
    }
}
