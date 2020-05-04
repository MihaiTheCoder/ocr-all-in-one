using System.Collections.Generic;

namespace WindowsOcrWrapper.WindowsOcr
{
    public class OcrLine
    {
        public string Text { get; set; }

        public List<Word> Words { get; set; }

        public static OcrLine FromDynamic(dynamic ocrLine)
        {
            OcrLine line = new OcrLine();
            line.Text = ocrLine.Text;
            line.Words = new List<Word>();
            foreach (var word in ocrLine.Words)
            {
                line.Words.Add(Word.FromDynamic(word));
            }
            return line;
        }
    }
}
