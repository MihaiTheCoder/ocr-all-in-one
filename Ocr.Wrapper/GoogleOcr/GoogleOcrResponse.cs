using System.Collections.Generic;
using System.Linq;

namespace Ocr.Wrapper.GoogleOcr
{
    public class GoogleOcrResponse: IMappableToGenericResponse
    {
        public List<GoogleOcrSingleResponse> Responses { get; set; }
        public string ImageFileName { get; set; }
        public string SoftwareName { get; set; }


        public GenericOcrResponse Map()
        {
            return Map(this);
        }

        public static GenericOcrResponse Map(GoogleOcrResponse ocrResponse)
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
                Lines = firstResponse.Annotations.Select(a => Map(a)).ToList(),
                SummaryText = summaryText,
                ImageFileName = ocrResponse.ImageFileName,
                SoftwareName = ocrResponse.SoftwareName
            };
        }


        public static GenericOcrLine Map(GoogleTextAnnotation a)
        {
            return new GenericOcrLine()
            {
                Words = new List<GenericBoxDetection>()
                {
                    Get(a)
                }
            };
        }

        private static GenericBoxDetection Get(GoogleTextAnnotation a)
        {
            var maxX = a.BoundingPoly[2].X;
            var maxY = a.BoundingPoly[2].Y;
            var minX = a.BoundingPoly[0].X;
            var minY = a.BoundingPoly[0].Y;
            return new GenericBoxDetection
            {
                DetectedText = a.Description,
                BoundingBox = new GenericBoundingBox {
                    Height = maxY - minY, 
                    Left = 0, //distance from the upper-left corner of the bounding box, to the left border of the image
                    Top = 0, //distance from the upper-left corner of the bounding box, to the top border of the image
                    Width = maxX - minX
                }
            };
        }
    }
}

