using Ocr.Wrapper.ImageManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper
{
    public class HighLevelOcrService<TResponse, TLowLevelService> : IHighLevelOcrService<TResponse>
        where TResponse : IMappableToGenericResponse
        where TLowLevelService: ILowLevelOcrService<TResponse>
    {
        protected readonly TLowLevelService lowLevelOcrService;
        private readonly HighLevelOcrServiceParams ocrParams;

        public string Name => lowLevelOcrService.Name;

        public HighLevelOcrService(TLowLevelService lowLevelOcrService, HighLevelOcrServiceParams ocrParams)
        {
            this.lowLevelOcrService = lowLevelOcrService;

            if (ocrParams == null)
                ocrParams = new HighLevelOcrServiceParams();

            this.ocrParams = ocrParams;
        }

        public TResponse GetOcrResult(string inputImage, string inputLanguage = null, bool runAnywayWithBadLanguage = true)
        {
            return GetOcrResultAsync(inputImage, inputLanguage, runAnywayWithBadLanguage).Result;
        }

        public async Task<TResponse> GetOcrResultAsync(string inputImage, string inputLanguage = null, bool runAnywayWithBadLanguage = true)
        {
            if (ocrParams.ImageCompressor != null)
                inputImage = ocrParams.ImageCompressor.CompressInPlace(inputImage);

            if (ocrParams.OcrCache != null)
            {
                var cacheResult = await ocrParams.OcrCache.GetFromCache<TResponse>(inputImage, lowLevelOcrService.Name);

                if (cacheResult.Item1)
                    return cacheResult.Item2;
            }

            var result = await lowLevelOcrService.GetOcrResultWithoutCacheAsync(inputImage, inputLanguage, runAnywayWithBadLanguage);

            if (ocrParams.OcrCache != null)
                await ocrParams.OcrCache.SaveToCache(inputImage, lowLevelOcrService.Name, result);

            return result;
        }

        public async Task<GenericOcrResponse> GetGenericOcrResultAsync(string inputImage, string inputLanguage = null, bool runAnywayWithBadLanguage = true)
        {
            var result = await GetOcrResultAsync(inputImage, inputLanguage, runAnywayWithBadLanguage);
            return result.Map();
        }

        public Task<TResponse> GetOcrResultWithoutCacheAsync(string filePath, string language = null, bool runAnywayWithBadLanguage = true)
        {
            return lowLevelOcrService.GetOcrResultWithoutCacheAsync(filePath, language, runAnywayWithBadLanguage);
        }
    }

    public class HighLevelOcrServiceParams
    {
        public IOcrCache OcrCache { get; set; }

        public IImageCompressor ImageCompressor { get; set; }
    }
}
