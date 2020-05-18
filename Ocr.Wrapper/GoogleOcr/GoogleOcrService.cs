namespace Ocr.Wrapper.GoogleOcr
{
    public class GoogleOcrService: HighLevelOcrService<GoogleOcrResponse, GoogleLowLevelOcrService>
    {
        public GoogleOcrService(string apiToken, HighLevelOcrServiceParams ocrParams=null): base(new GoogleLowLevelOcrService(apiToken), ocrParams)
        {

        }

    }
}
