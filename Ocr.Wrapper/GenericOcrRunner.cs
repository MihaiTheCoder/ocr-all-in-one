using Ocr.Wrapper.ImageManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper
{
    public abstract class GenericOcrRunner<T> : IGenericOcrRunner<T> where T : IMappableToGenericResponse
    {
        private readonly IOcrCache ocrCache;
        private readonly IImageCompressor imageCompressor;

        public GenericOcrRunner(IOcrCache ocrCache, IImageCompressor imageCompressor)
        {
            this.ocrCache = ocrCache;
            this.imageCompressor = imageCompressor;
        }
        public abstract string Name { get; }

        public abstract Task<T> GetOcrResultWithoutCacheAsync(string inputImage, string inputLanguage = null, bool runAnywayWithBadLanguage = true);
       
        public async Task<T> GetOcrResultAsync(string inputImage, string inputLanguage = null, bool runAnywayWithBadLanguage = true)
        {
            if (imageCompressor != null)
                inputImage = imageCompressor.CompressInPlace(inputImage);

            var cacheResult = await ocrCache.GetFromCache<T>(inputImage, Name);
            
            if (cacheResult.Item1)
                return cacheResult.Item2;

            var result = await GetOcrResultWithoutCacheAsync(inputImage, inputLanguage);

            await ocrCache.SaveToCache(inputImage, Name, result);

            return result;
        }

        public async Task<GenericOcrResponse> GetGenericOcrResultAsync(string inputImage, string inputLanguage = null, bool runAnywayWithBadLanguage = true)
        {
            var result = await GetOcrResultAsync(inputImage, inputLanguage);
            return result.Map();
        }
    }
}
