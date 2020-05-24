using Foundatio.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper
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

        public async Task<bool> IsFileInCache(string inputFilePath, string language, string ocrEngineName)
        {
            return await IsHashInCache(GetEncodedHashFromFile(inputFilePath), language, ocrEngineName);
        }

        public Task<bool> IsHashInCache(string encodedHash, string language, string ocrEngineName)
        {
            return fileStorage.ExistsAsync(GetFilePath(ocrEngineName, encodedHash, language));
        }

        public async Task<Tuple<bool, T>> GetFromCache<T>(string filePath, string language, string ocrEngine)
        {
            var encodedHash = GetEncodedHashFromFile(filePath);

            if (!(await IsHashInCache(encodedHash, language, ocrEngine)))
                return new Tuple<bool, T>(false, default);

            var objContent = await fileStorage.GetObjectAsync<T>(GetFilePath(ocrEngine, encodedHash, language));

            return new Tuple<bool, T>(true, objContent);
        }

        public async Task<bool> SaveToCache<T>(string inputFilePath, string language, string ocrEngine, T result)
        {
            var encodedHash = GetEncodedHashFromFile(inputFilePath);
            var cacheFilePath = GetFilePath(ocrEngine, encodedHash, language);
            return await fileStorage.SaveObjectAsync(cacheFilePath, result);
        }

        private string GetFilePath(string ocrEngineName, string encodedHash, string language)
        {
            if (language == null)
                language = "NoLang";
            var langAndHash = language + "- " + encodedHash;
            return Path.Combine(ocrEngineName, langAndHash);
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
