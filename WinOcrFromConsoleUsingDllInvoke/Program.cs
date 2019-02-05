using System;
using System.IO;
using System.Threading.Tasks;
using WindowsOcrWrapper;
using System.Linq;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WinOcrFromConsoleUsingDllInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = @"C:\Users\mihai.petrutiu\Downloads\clocks\clocks\";
            string[] files = Directory.GetFiles(dir).Where(f => f.EndsWith("png")).ToArray();
            var png = @"C:\Users\mihai.petrutiu\Downloads\clocks\clocks\AAA_BXSP001_060.mxf_clock.png";
            /*
            OcrExecutor ocrExecutor = new OcrExecutor();
            

            Parallel.For(0, files.Length, async (i) => {
                var result = await ocrExecutor.GetOcrResultAsync(files[i]);
                Console.WriteLine(result.Text);
                Console.WriteLine(i);
            });
            Console.ReadKey();*/

            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/values").Result;
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
