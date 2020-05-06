using Foundatio.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper
{
    public class OcrCache
    {
        private readonly IFileStorage fileStorage;
        private readonly string rootDirectory;

        public OcrCache(IFileStorage fileStorage, string rootDirectory)
        {
            this.fileStorage = fileStorage;
            this.rootDirectory = rootDirectory;
        }

        public async Task<bool> IsFileInCache(string inputFilePath, string ocrEngineName)
        {
            return await IsHashInCache(GetEncodedHashFromFile(inputFilePath), ocrEngineName);
        }

        public Task<bool> IsHashInCache(string encodedHash, string ocrEngineName)
        {
            return fileStorage.ExistsAsync(GetFilePath(ocrEngineName, encodedHash));
        }

        public async Task<Tuple<bool, T>> GetFromCache<T>(string filePath, string ocrEngine)
        {
            var encodedHash = GetEncodedHashFromFile(filePath);

            if (!(await IsHashInCache(encodedHash, ocrEngine)))
                return new Tuple<bool, T>(false, default);

            var objContent = await fileStorage.GetObjectAsync<T>(GetFilePath(ocrEngine, encodedHash));

            return new Tuple<bool, T>(true, objContent);
        }

        public async Task<bool> SaveToCache<T>(string inputFilePath, string ocrEngine, T result)
        {
            var encodedHash = GetEncodedHashFromFile(inputFilePath);
            var cacheFilePath = GetFilePath(ocrEngine, encodedHash);
            return await fileStorage.SaveObjectAsync(cacheFilePath, result);
        }

        private string GetFilePath(string ocrEngineName, string encodedHash)
        {
            return Path.Combine(rootDirectory, ocrEngineName, encodedHash);
        }

        private string GetEncodedHashFromFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

    }
}
