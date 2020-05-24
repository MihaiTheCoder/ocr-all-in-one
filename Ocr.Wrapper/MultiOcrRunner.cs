using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocr.Wrapper
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

        public IGenericOcrRunner GetOcrByName(string ocrName)
        {
            return OcrRunners.FirstOrDefault(o => o.Name == ocrName);
        }

        public T GetOcrByType<T>() where T: class, IGenericOcrRunner
        {
            return OcrRunners.FirstOrDefault(ocr => ocr is T) as T;
        }
    }

    


}
