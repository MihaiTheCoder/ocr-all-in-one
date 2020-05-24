using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper
{
    public class NoOpOcrCache : IOcrCache
    {
        public Task<Tuple<bool, T>> GetFromCache<T>(string filePath, string language, string ocrEngine)
        {
            return Task.FromResult(new Tuple<bool, T>(false, default));
        }

        public Task<bool> IsFileInCache(string inputFilePath, string language, string ocrEngineName)
        {
            return Task.FromResult(false);
        }

        public Task<bool> IsHashInCache(string encodedHash, string language, string ocrEngineName)
        {
            return Task.FromResult(false);
        }

        public Task<bool> SaveToCache<T>(string inputFilePath, string language, string ocrEngine, T result)
        {
            return Task.FromResult(false);
        }
    }
}
