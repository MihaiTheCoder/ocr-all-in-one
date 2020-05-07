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
    public class FileStorageOcrCache : IOcrCache
    {
        private readonly IFileStorage fileStorage;

        public FileStorageOcrCache(string rootDirectory): this(GetDefaultStorage(rootDirectory))
        {

        }

        public FileStorageOcrCache(IFileStorage fileStorage)
        {
            this.fileStorage = fileStorage;
        }

        public static IFileStorage GetDefaultStorage(string rootDirectory)
        {
            return new FolderFileStorage(new FolderFileStorageOptions { Folder = rootDirectory });
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
            return Path.Combine(ocrEngineName, encodedHash);
        }

        private string GetEncodedHashFromFile(string filePath)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hashResult = hashAlgorithm.ComputeHash(stream);
                    return ByteArrayToHexStringViaBitConverter(hashResult);                   
                }
            }
        }

        static string ByteArrayToHexStringViaBitConverter(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes);
            return hex.Replace("-", "");
        }

    }
}
