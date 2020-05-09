using System;
using System.Collections.Generic;
using System.Linq;

namespace Ocr.Wrapper.AzureOcr
{
    public class AzureOcrRegion
    {
        public string BoundingBox { get; set; }

        public int[] ParsedBoundingBox
        {
            get
            {
                if (string.IsNullOrWhiteSpace(BoundingBox))
                    return new int[0];
                return BoundingBox.Split(',').Select(b => int.Parse(b)).ToArray();
            }
        }

        public List<AzureOcrLine> Lines { get; set; }
        public static AzureOcrRegion FromDynamic(dynamic region)
        {
            AzureOcrRegion azureOcrRegion = new AzureOcrRegion
            {
                BoundingBox = region.boundingBox,
                Lines = new List<AzureOcrLine>()
            };

            var ceapa = azureOcrRegion.BoundingBox.Split(',');
            foreach (dynamic line in region.lines)
            {
                azureOcrRegion.Lines.Add(AzureOcrLine.FromDynamic(line));
            }
            return azureOcrRegion;
        }
    }
}