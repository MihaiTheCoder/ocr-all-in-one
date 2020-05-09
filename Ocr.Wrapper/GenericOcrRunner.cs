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

        public GenericOcrRunner(IOcrCache ocrCache)
        {
            this.ocrCache = ocrCache;
        }
        public abstract string Name { get; }

        public abstract Task<T> GetOcrResultWithoutCacheAsync(string inputImage, string inputLanguage = null);
       
        public async Task<T> GetOcrResultAsync(string inputImage, string inputLanguage = null)
        {
            var cacheResult = await ocrCache.GetFromCache<T>(inputImage, Name);
            
            if (cacheResult.Item1)
                return cacheResult.Item2;

            var result = await GetOcrResultWithoutCacheAsync(inputImage, inputLanguage);

            await ocrCache.SaveToCache(inputImage, Name, result);

            return result;
        }

        public async Task<GenericOcrResponse> GetGenericOcrResultAsync(string inputImage, string inputLanguage = null)
        {
            var result = await GetOcrResultAsync(inputImage, inputLanguage);
            return result.Map();
        }
    }
}
