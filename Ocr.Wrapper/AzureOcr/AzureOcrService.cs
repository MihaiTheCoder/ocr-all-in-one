using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.AzureOcr
{
    public class AzureOcrService: HighLevelOcrService<AzureOcrResults, AzureLowLevelOcrService>
    {
        public AzureOcrService(string subscriptionKey, string endpoint, HighLevelOcrServiceParams ocrParams = null) : 
            base(new AzureLowLevelOcrService(subscriptionKey, endpoint), ocrParams)
        {

        }
    }
}
