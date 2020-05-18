namespace Ocr.Wrapper.TesseractOcr
{
    public class TesseractOcrService : HighLevelOcrService<TesseractResponse, TesseractLowLevelOcrService>
    {
        public TesseractOcrService(string tesseractDir = TesseractInstaller.DefaultInstallDir, string dataDir = null, HighLevelOcrServiceParams ocrParams=null) : 
            base(new TesseractLowLevelOcrService(tesseractDir, dataDir), ocrParams)
        {
        }
    }
}
