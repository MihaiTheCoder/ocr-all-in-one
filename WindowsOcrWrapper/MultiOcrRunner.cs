using System.Collections.Generic;
using System.Threading.Tasks;

namespace WindowsOcrWrapper
{
    public class MultiOcrRunner
    {
        public IGenericOcrRunner[] OcrRunners { get; private set; }

        public MultiOcrRunner(params IGenericOcrRunner[] ocrRunners)
        {
            OcrRunners = ocrRunners;
        }

        public async Task<Dictionary<string, GenericOcrResponse>> RunAllOcrEnginesOnImage(string inputImagePath, string language = null)
        {
            return await RunAllOcrEnginesOnImage(OcrRunners, inputImagePath, language);
        }

        public static async Task<Dictionary<string, GenericOcrResponse>> RunAllOcrEnginesOnImage(
            IEnumerable<IGenericOcrRunner> ocrEngines,
            string inputImagePath, string language = null)
        {
            var responses = new Dictionary<string, GenericOcrResponse>();
            foreach (var ocr in ocrEngines)
            {
                responses[ocr.Name] = await ocr.GetGenericOcrResultAsync(inputImagePath, language);
            }
            return responses;
        }
    }

    


}
