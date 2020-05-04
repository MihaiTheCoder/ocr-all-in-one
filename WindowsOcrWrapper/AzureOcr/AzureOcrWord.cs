using System;

namespace WindowsOcrWrapper.AzureOcr
{
    public class AzureOcrWord
    {
        public string BoundingBox { get; set; }

        public string Text { get; set; }

        internal static AzureOcrWord FromDynamic(dynamic word)
        {
            AzureOcrWord azureOcrWord = new AzureOcrWord();
            azureOcrWord.BoundingBox = word.boundingBox;
            azureOcrWord.Text = word.text;
            return azureOcrWord;
        }
    }
}