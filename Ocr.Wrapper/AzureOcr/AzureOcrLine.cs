using System;
using System.Collections.Generic;
using System.Linq;

namespace Ocr.Wrapper.AzureOcr
{
    public class AzureOcrLine
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

        public List<AzureOcrWord> Words { get; set; }

        public static AzureOcrLine FromDynamic(dynamic line)
        {
            AzureOcrLine azureOcrLine = new AzureOcrLine();
            azureOcrLine.BoundingBox =  line.boundingBox;
            azureOcrLine.Words = new List<AzureOcrWord>();
            foreach (var word in line.words)
            {
                azureOcrLine.Words.Add(AzureOcrWord.FromDynamic(word));
            }
            return azureOcrLine;
        }
    }
}