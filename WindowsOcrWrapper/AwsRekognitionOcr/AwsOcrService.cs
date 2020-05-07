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
    public class AwsOcrService: GenericOcrRunner<AwsOcrResponse>
    {
        AmazonRekognitionClient rekognitionClient;
        public AwsOcrService(IOcrCache ocrCache, string accessKey, string secretKey): base(ocrCache)
        {
            rekognitionClient = new AmazonRekognitionClient(accessKey, secretKey);
        }

        public override string Name => nameof(AwsOcrService);

        public override async Task<AwsOcrResponse> GetOcrResultWithoutCacheAsync(string filePath, string language=null)
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
    }
}
