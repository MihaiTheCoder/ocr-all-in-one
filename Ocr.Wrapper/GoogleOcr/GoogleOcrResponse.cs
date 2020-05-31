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
            return new GenericBoxDetection
            {
                DetectedText = a.Description,
                BoundingBox = new GenericBoundingBox { Height = 0, Left = 0, Top = 0, Width = 0 }
            };
        }
    }
}

