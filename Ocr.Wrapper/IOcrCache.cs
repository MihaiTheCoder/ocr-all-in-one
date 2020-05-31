using System;
using System.Threading.Tasks;

namespace Ocr.Wrapper
{
    public interface IOcrCache
    {
        Task<Tuple<bool, T>> GetFromCache<T>(string filePath, string language, string ocrEngine);
        Task<bool> IsFileInCache(string inputFilePath, string language, string ocrEngineName);
        Task<bool> IsHashInCache(string encodedHash, string language, string ocrEngineName);
        Task<bool> SaveToCache<T>(string inputFilePath, string language, string ocrEngine, T result);
    }
}