using System.Collections.Generic;

namespace WindowsOcrWrapper.WinOcrResults
{
    public class OcrResult
    {
        public string Text { get; set; }

        public List<OcrLine> Lines { get; set; }

        public static OcrResult FromDynamic(dynamic ocrResult)
        {
            var result = new OcrResult();
            result.Text = ocrResult.Text;
            result.Lines = new List<OcrLine>();
            foreach (var line in ocrResult.Lines)
            {
                result.Lines.Add(OcrLine.FromDynamic(line));
            }
            return result;
        }
        
    }
}
