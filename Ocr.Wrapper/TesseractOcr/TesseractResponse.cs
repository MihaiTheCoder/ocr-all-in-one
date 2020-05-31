using System;
using System.Linq;
using System.Collections.Generic;

namespace Ocr.Wrapper.TesseractOcr
{
    public class TesseractResponse: IMappableToGenericResponse
    {
        public TesseractDetectionLine[] DetectionLines { get; set; }

        public string Language { get; set; }

        public string ImageFileName { get; set; }

        public string SoftwareName { get; set; }

        public GenericOcrResponse Map()
        {
            return Map(this);
        }

        public static GenericOcrResponse Map(TesseractResponse tesseractResponse)
        {
            return new GenericOcrResponse
            {
                SummaryText = String.Join(" ", tesseractResponse.DetectionLines.Select(dl => dl.Text)),
                Lines = tesseractResponse.DetectionLines.Select(dl => Map(dl)).ToList(),
                Language = tesseractResponse.Language,
                SoftwareName = tesseractResponse.SoftwareName,
                ImageFileName = tesseractResponse.ImageFileName
            };
        }

        public static GenericOcrLine Map(TesseractDetectionLine line)
        {
            return new GenericOcrLine()
            {
                Words = new List<GenericBoxDetection>()
                {
                    Get(line)
                }
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
