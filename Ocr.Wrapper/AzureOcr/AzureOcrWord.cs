using System.Linq;

namespace Ocr.Wrapper.AzureOcr
{
    public class AzureOcrWord
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