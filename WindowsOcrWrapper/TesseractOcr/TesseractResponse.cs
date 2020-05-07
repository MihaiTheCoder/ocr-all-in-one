using System;
using System.Linq;

namespace WindowsOcrWrapper.TesseractOcr
{
    public class TesseractResponse: IMappableToGenericResponse
    {
        public TesseractDetectionLine[] DetectionLines { get; set; }

        public string Language { get; set; }

        public GenericOcrResponse Map()
        {
            return Map(this);
        }

        public static GenericOcrResponse Map(TesseractResponse tesseractResponse)
        {
            return new GenericOcrResponse
            {
                SummaryText = String.Join(" ", tesseractResponse.DetectionLines.Select(dl => dl.Text)),
                Detections = tesseractResponse.DetectionLines.Select(dl => Get(dl)).ToList(),
                Language = tesseractResponse.Language
            };
        }

        private static GenericBoxDetection Get(TesseractDetectionLine dl)
        {
            return new GenericBoxDetection
            {
                Confidence = dl.Confidence,
                DetectedText = dl.Text,
                BoundingBox = new GenericBoundingBox
                {
                    Height = dl.Height,
                    Left = dl.Left,
                    Top = dl.Top,
                    Width = dl.Width
                }
            };
        }
    }
}
