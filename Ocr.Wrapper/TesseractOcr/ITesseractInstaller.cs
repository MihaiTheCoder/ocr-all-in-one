using System.Threading.Tasks;

namespace Ocr.Wrapper.TesseractOcr
{
    public interface ITesseractInstaller
    {
        Task Install();
        void UninstallTesseract();
    }
}