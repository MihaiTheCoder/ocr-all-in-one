namespace Ocr.Wrapper.WindowsOcr
{
    public class WindowsOcrService : HighLevelOcrService<WindowsOcrResult, WindowsLowLevelOcrService>
    {
        public WindowsOcrService(HighLevelOcrServiceParams ocrParams=null) : base(new WindowsLowLevelOcrService(), ocrParams)
        {
        }

        public string DebugPsOutput { get { return base.lowLevelOcrService.debugPsOutput.ToString(); } }

        
    }
}
