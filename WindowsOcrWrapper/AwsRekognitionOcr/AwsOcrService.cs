using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper.AwsRekognitionOcr
{
    public class AwsOcrService: IGenericOcrRunner
    {
        AmazonRekognitionClient rekognitionClient;
        public AwsOcrService(string accessKey, string secretKey)
        {
            rekognitionClient = new AmazonRekognitionClient(accessKey, secretKey);
        }

        public string Name => nameof(AwsOcrService);

        public async Task<AwsOcrResponse> GetOcrResultAsync(string filePath, string language=null)
        {
            MemoryStream ms = new MemoryStream();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                fileStream.CopyTo(ms);                
            }
            var request = new DetectTextRequest { Image = new Image() { Bytes = ms }};
            DetectTextResponse result = await rekognitionClient.DetectTextAsync(request);
            return AwsResponseMapper.Get(result);
        }

        public async Task<GenericOcrResponse> RunAsync(string inputImage, string inputLanguage = null)
        {
            return await GetOcrResultAsync(inputImage, inputLanguage);
        }
    }
}
