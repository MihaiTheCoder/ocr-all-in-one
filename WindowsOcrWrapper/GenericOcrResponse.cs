using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsOcrWrapper.AwsRekognitionOcr;
using WindowsOcrWrapper.AzureOcr;
using WindowsOcrWrapper.GoogleOcr;
using WindowsOcrWrapper.TesseractOcr;
using WindowsOcrWrapper.WindowsOcr;

namespace WindowsOcrWrapper
{
    public class GenericOcrResponse
    {
        public string SummaryText { get; set; }

        public string Language { get; set; }

        public List<GenericBoxDetection> Detections { get; set; }

        public static implicit operator GenericOcrResponse(AwsOcrResponse awsResponse)
        {
            var response = new GenericOcrResponse();
            response.SummaryText = string.Join(" ", awsResponse.TextDetections.Select(td => td.DetectedText));

            response.Detections = awsResponse.TextDetections.Select(td => Get(td)).ToList();
            return response;
        }

        private static GenericBoxDetection Get(AwsTextDetection awsGeometry)
        {
            return new GenericBoxDetection {
                DetectedText = awsGeometry.DetectedText,
                Confidence = awsGeometry.Confidence,
                BoundingBox = new GenericBoundingBox {
                    Height = awsGeometry.Geometry.BoundingBox.Height,
                    Width = awsGeometry.Geometry.BoundingBox.Width,
                    Left = awsGeometry.Geometry.BoundingBox.Left,
                    Top = awsGeometry.Geometry.BoundingBox.Top
                } };
        }

        public static implicit operator GenericOcrResponse(AzureOcrResults azureOcrResults)
        {
            return new GenericOcrResponse
            {
                Detections = azureOcrResults.Regions.SelectMany(r => r.Lines).SelectMany(l => l.Words).Select(w => Get(w)).ToList(),
                Language = azureOcrResults.Language,
                SummaryText = string.Join(Environment.NewLine, azureOcrResults.Regions.Select(r => GetText(r)))
            };
        }

        private static string GetText(AzureOcrRegion r)
        {
            return string.Join(Environment.NewLine, r.Lines.Select(l => string.Join(" ", l.Words.Select(w => w.Text))));
        }

        private static GenericBoxDetection Get(AzureOcrWord word)
        {
            return new GenericBoxDetection
            {
                DetectedText = word.Text,
                BoundingBox = new GenericBoundingBox {
                    Height = word.BoundingBox[0],
                    Width = word.BoundingBox[1],
                    Left = word.BoundingBox[2],
                    Top = word.BoundingBox[3],
                }
            };
        }

        public static implicit operator GenericOcrResponse(TesseractResponse tesseractResponse)
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


        public static implicit operator GenericOcrResponse(WindowsOcrResult ocrResult)
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


        public static implicit operator GenericOcrResponse(GoogleOcrResponse ocrResponse)
        {
            var firstResponse = ocrResponse.Responses.FirstOrDefault();
            var firstAnnotation = firstResponse?.Annotations?.FirstOrDefault();
            if (firstAnnotation == null)
                return new GenericOcrResponse();
            var language = firstAnnotation.Locale;
            var summaryText = firstAnnotation.Description;

            return new GenericOcrResponse
            {
                Language = language,
                Detections = firstResponse.Annotations.Select(a => Get(a)).ToList(),
                SummaryText = summaryText
            };
        }

        private static GenericBoxDetection Get(GoogleTextAnnotation a)
        {            
            return new GenericBoxDetection
            {
                DetectedText = a.Description,
                BoundingBox = new GenericBoundingBox { Height = 0, Left = 0, Top = 0, Width = 0 }
            };
        }
    }

    public class GenericBoxDetection
    {
        public string DetectedText { get; set; }

        public float? Confidence { get; set; }

        public GenericBoundingBox BoundingBox { get; set; }

    }

    public class GenericBoundingBox
    {
        public float Height { get; set; }

        public float Left { get; set; }

        public float Top { get; set; }

        public float Width { get; set; }
    }
}
