using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ocr.Wrapper.WebAPI.Services
{
    public static class AspNetFileProcessorWrapper
    {
        public static async Task<List<T>> ProcessFilesAsync<T>(Func<string,Task<T>> processFile,params IFormFile[] files)
        {
            if (files == null || files.Length == 0)
                return new List<T>();

            long size = files.Sum(f => f.Length);
            var processedFileResults = new List<T>(files.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    processedFileResults.Add(await processFile(filePath));
                    File.Delete(filePath);
                }
            }
            return processedFileResults;
        }
    }
}
