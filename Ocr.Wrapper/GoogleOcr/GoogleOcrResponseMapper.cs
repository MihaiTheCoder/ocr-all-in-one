using System.Collections.Generic;

namespace Ocr.Wrapper.GoogleOcr
{
    public class GoogleOcrResponseMapper
    {
        internal static GoogleOcrResponse FromDynamic(dynamic googleResult, string inputFileName, string ocrName)
        {
            GoogleOcrResponse googleOcrResponse = new GoogleOcrResponse();
            googleOcrResponse.Responses = new List<GoogleOcrSingleResponse>();
            foreach (var response in googleResult.responses)
            {
                var singleOcrResponse = new GoogleOcrSingleResponse();
                singleOcrResponse.Annotations = new List<GoogleTextAnnotation>();
                foreach (var textAnnotation in response.textAnnotations)
                {
                    var googleTextAnnotation = new GoogleTextAnnotation();
                    googleTextAnnotation.Description = textAnnotation.description;
                    googleTextAnnotation.Locale = textAnnotation.locale;
                    googleTextAnnotation.BoundingPoly = new GoogleBoundingPoly();
                    foreach (var boundingPoly in textAnnotation.boundingPoly)
                    {
                        foreach (var vertice in boundingPoly.Value)
                        {
                            googleTextAnnotation.BoundingPoly.Add(new GoogleVertice { X = vertice.x ?? -1, Y = vertice.y ?? -1 });
                        }                        
                    }
                    singleOcrResponse.Annotations.Add(googleTextAnnotation);
                }
                googleOcrResponse.Responses.Add(singleOcrResponse);
            }
            googleOcrResponse.ImageFileName = inputFileName;
            googleOcrResponse.SoftwareName = ocrName;
            return googleOcrResponse;
        }
    }
}

