using System;
using System.Collections.Generic;

namespace WindowsOcrWrapper.AzureOcr
{
    public class AzureOcrRegion
    {
        public string BoundingBox { get; set; }

        public List<AzureOcrLine> Lines { get; set; }
        public static AzureOcrRegion FromDynamic(dynamic region)
        {
            AzureOcrRegion azureOcrRegion = new AzureOcrRegion
            {
                BoundingBox = region.boundingBox,
                Lines = new List<AzureOcrLine>()
            };
            foreach (dynamic line in region.lines)
            {
                azureOcrRegion.Lines.Add(AzureOcrLine.FromDynamic(line));
            }
            return azureOcrRegion;
        }
    }
}