using System.Collections.Generic;
using System.Linq;

namespace WindowsOcrWrapper.GoogleOcr
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
            return new GenericBoxDetection
            {
                DetectedText = a.Description,
                BoundingBox = new GenericBoundingBox { Height = 0, Left = 0, Top = 0, Width = 0 }
            };
        }
    }
}

