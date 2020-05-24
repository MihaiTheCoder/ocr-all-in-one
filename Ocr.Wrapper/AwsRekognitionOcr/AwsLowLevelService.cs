using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.AwsRekognitionOcr
{
    public class AwsLowLevelOcrService : ILowLevelOcrService<AwsOcrResponse>
    {
        AmazonRekognitionClient rekognitionClient;
        public AwsLowLevelOcrService(string accessKey, string secretKey, string region)
        {

            rekognitionClient = new AmazonRekognitionClient(
                                        accessKey, 
                                        secretKey, 
                                        region == null ? null : RegionEndpoint.GetBySystemName(region));
        }

        public string Name => nameof(AwsOcrService);

        public async Task<AwsOcrResponse> GetOcrResultWithoutCacheAsync(string filePath, string language = null, bool runAnywayWithBadLanguage = true)
        {
            MemoryStream ms = new MemoryStream();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                fileStream.CopyTo(ms);
            }
            var request = new DetectTextRequest { Image = new Image() { Bytes = ms } };
            DetectTextResponse result = await rekognitionClient.DetectTextAsync(request);
            return AwsResponseMapper.Get(result);
        }
    }
}
