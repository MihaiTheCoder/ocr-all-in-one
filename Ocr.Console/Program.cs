﻿using System;
using System.IO;
using System.Threading.Tasks;
using Ocr.Wrapper;
using System.Linq;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using Ocr.Wrapper.WindowsOcr;

namespace OcrFromConsole
{
    /// <summary>
    /// Add dependency injection, remove test client ...
    /// index.html is just a test html page to check if upload is working
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            var png = @"Data/abc.JPG";
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                Console.WriteLine("Starting invoke...");
                var response = UploadImage(baseAddress + "api/values", File.ReadAllBytes(png)).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }

        static async public Task<HttpResponseMessage> UploadImage(string url, byte[] ImageData)
        {
            HttpClient client = new HttpClient();
            var requestContent = new MultipartFormDataContent();
            //    here you can specify boundary if you need---^
            var imageContent = new ByteArrayContent(ImageData);
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("image/jpeg");

            requestContent.Add(imageContent, "image", "image.jpg");

            return await client.PostAsync(url, requestContent);
        }
    }
}
