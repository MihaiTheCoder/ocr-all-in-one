namespace Ocr.Wrapper.TesseractOcr
{
    public class TesseractOcrService : HighLevelOcrService<TesseractResponse, TesseractLowLevelOcrService>
    {
        public TesseractOcrService(string tesseractInstalledPath = WindowsTesseractInstaller.DefaultInstalledPath, string dataDir = null, HighLevelOcrServiceParams ocrParams=null) : 
            base(new TesseractLowLevelOcrService(tesseractInstalledPath, dataDir), ocrParams)
        {
        }
    }
}
