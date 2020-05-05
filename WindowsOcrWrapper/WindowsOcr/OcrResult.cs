using System.Collections.Generic;

namespace WindowsOcrWrapper.WindowsOcr
{
    public class WindowsOcrResult
    {
        public string Text { get; set; }

        public List<OcrLine> Lines { get; set; }

        public string Language { get; set; }

        public static WindowsOcrResult FromDynamic(dynamic ocrResult)
        {
            var result = new WindowsOcrResult();
            result.Lines = new List<OcrLine>();
            foreach (var line in ocrResult.Lines)
            {
                result.Lines.Add(OcrLine.FromDynamic(line));
            }
            result.Text = ocrResult.Text;
            
            return result;
        }
        
    }
}
