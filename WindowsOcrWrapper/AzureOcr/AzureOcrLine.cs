using System;
using System.Collections.Generic;

namespace WindowsOcrWrapper.AzureOcr
{
    public class AzureOcrLine
    {
        public string BoundingBox { get; set; }

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