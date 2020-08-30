using System.Collections.Generic;
using System.Linq;

namespace Ocr.Wrapper.WindowsOcr
{
    public class WindowsOcrResult: IMappableToGenericResponse
    {
        public string Text { get; set; }

        public List<OcrLine> Lines { get; set; }

        public string Language { get; set; }

        public string InputImage { get; set; }

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

        public GenericOcrResponse Map()
        {
            return Map(this);
        }

        public static GenericOcrResponse Map(WindowsOcrResult ocrResult)
        {
            return new GenericOcrResponse
            {
                SummaryText = ocrResult.Text,
                Language = ocrResult.Language,
                Detections = ocrResult.Lines.SelectMany(l => l.Words).Select(w => Get(w)).ToList()
            };
        }

        private static GenericBoxDetection Get(Word w)
        {
            return new GenericBoxDetection
            {
                DetectedText = w.Text,
                BoundingBox = new GenericBoundingBox
                {
                    Height = (float)w.BoundingRect.Height,
                    Left = (float)w.BoundingRect.Left,
                    Top = (float)w.BoundingRect.Top,
                    Width = (float)w.BoundingRect.Width
                }
            };
        }
    }
}
