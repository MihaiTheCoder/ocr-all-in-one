using System.Threading.Tasks;
using WindowsOcrWrapper.WindowsOcr;

namespace WindowsOcrWrapper
{
    public interface IOcrExecutor
    {
        Task<OcrResult> GetOcrResultAsync(string imagePath);
    }
}