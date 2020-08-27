using System.Collections.Generic;
using System.Linq;

namespace Ocr.Wrapper.GoogleOcr
{
    public class GoogleOcrResponse: IMappableToGenericResponse
    {
        public List<GoogleOcrSingleResponse> Responses { get; set; }

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
                Detections = firstResponse.Annotations.Select(a => Get(a)).ToList(),
                SummaryText = summaryText
            };
        }

        private static GenericBoxDetection Get(GoogleTextAnnotation a)
        {
            var leftest = a.BoundingPoly.OrderBy(e => e.X).ToList();
            var upperLeftCorner = leftest[1];
            var height = leftest[0].Y - upperLeftCorner.Y;
            if (leftest[0].Y < leftest[1].Y) {
                upperLeftCorner = leftest[0];
                height = leftest[1].Y - upperLeftCorner.Y;
            }

            var rightMostCorner = leftest.Last();
            var widht = rightMostCorner.X - upperLeftCorner.X;
            return new GenericBoxDetection
            {
                DetectedText = a.Description,
                BoundingBox = new GenericBoundingBox 
                {
                    Left = upperLeftCorner.X,
                    Top = upperLeftCorner.Y,
                    Height = height,
                    Width = widht
                }
            };
        }
    }
}

