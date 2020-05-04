using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper.AzureOcr
{
    public class AzureOcrResults
    {
        public string Language { get; set; }

        public float TextAngle { get; set; }

        public string Orientation { get; set; }

        public List<AzureOcrRegion> Regions { get; set; }

        public static AzureOcrResults FromDynamic(dynamic ocrResult)
        {
            AzureOcrResults azureOcrResults = new AzureOcrResults();
            azureOcrResults.Language = ocrResult.language;
            azureOcrResults.TextAngle = ocrResult.textAngle;
            azureOcrResults.Orientation = ocrResult.orientation;
            azureOcrResults.Regions = new List<AzureOcrRegion>();
            foreach (dynamic region in ocrResult.regions)
            {
                azureOcrResults.Regions.Add(AzureOcrRegion.FromDynamic(region));
            }
            
            return azureOcrResults;
        }
    }
}
