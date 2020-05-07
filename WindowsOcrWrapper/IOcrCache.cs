using System;
using System.Threading.Tasks;

namespace WindowsOcrWrapper
{
    public interface IOcrCache
    {
        Task<Tuple<bool, T>> GetFromCache<T>(string filePath, string ocrEngine);
        Task<bool> IsFileInCache(string inputFilePath, string ocrEngineName);
        Task<bool> IsHashInCache(string encodedHash, string ocrEngineName);
        Task<bool> SaveToCache<T>(string inputFilePath, string ocrEngine, T result);
    }
}