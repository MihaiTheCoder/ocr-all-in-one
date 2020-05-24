using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocr.Wrapper.ImageManipulation;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.AwsRekognitionOcr
{
    public class AwsOcrService: HighLevelOcrService<AwsOcrResponse, AwsLowLevelOcrService>
    {
        public AwsOcrService(string accessKey, string secretKey, string awsRegion=null, HighLevelOcrServiceParams ocrParams=null): base(new AwsLowLevelOcrService(accessKey, secretKey, awsRegion), ocrParams)
        {
            
        }
    }
}
