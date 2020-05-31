using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.GoogleOcr
{
    public class GoogleLowLevelOcrService : ILowLevelOcrService<GoogleOcrResponse>
    {
        private readonly string apiToken;

        private const string ocrPostUrl = "https://vision.googleapis.com/v1/images:annotate";

        public string Name => nameof(GoogleLowLevelOcrService);

        public GoogleLowLevelOcrService(string apiToken)
        {
            this.apiToken = apiToken;
        }

        public string GetBody(string base64Image)
        {
            var request = new Dictionary<string, dynamic>();
            request.Add("image", new Dictionary<string, string> { { "content", base64Image } });
            request.Add("features", new List<dynamic> { new { type = "TEXT_DETECTION" } });
            var body = new Dictionary<string, List<dynamic>>();
            body["requests"] = new List<dynamic>();
            body["requests"].Add(request);
            string json = JsonConvert.SerializeObject(body, Formatting.Indented);

            return json;
        }

        public async Task<GoogleOcrResponse> GetOcrResultWithoutCacheAsync(string filePath, string language = null, bool runAnywayWithBadLanguage = true)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            string base64Data = Convert.ToBase64String(bytes);
            var postBody = GetBody(base64Data);

            HttpClient client = new HttpClient();

            var content = new StringContent(postBody, Encoding.UTF8, "application/json");
            string url = $"{ocrPostUrl}?key={apiToken}";
            var response = await client.PostAsync(url, content);
            string contentString = await response.Content.ReadAsStringAsync();
            return GoogleOcrResponseMapper.FromDynamic(JToken.Parse(contentString) as dynamic, filePath, Name);
        }
    }
}
