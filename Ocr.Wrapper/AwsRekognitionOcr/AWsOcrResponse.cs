using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.AwsRekognitionOcr
{
    public class AwsOcrResponse: IMappableToGenericResponse
    {
        /// <summary>
        /// Gets and sets the property TextDetections.
        /// An array of text that was detected in the input image.
        /// </summary>
        public List<AwsTextDetection> TextDetections { get; set; }
        
        /// <summary>
        /// Gets and sets the property TextModelVersion.
        /// The model version used to detect text.
        /// </summary>
        public string TextModelVersion { get; set; }

        public GenericOcrResponse Map()
        {
            return Map(this);
        }

        public static GenericOcrResponse Map(AwsOcrResponse awsResponse)
        {
            var response = new GenericOcrResponse();
            response.SummaryText = string.Join(" ", awsResponse.TextDetections.Select(td => td.DetectedText));

            response.Lines = awsResponse.TextDetections.Select(td => Map(td)).ToList();
            return response;
        }

        public static GenericOcrLine Map(AwsTextDetection textDetection)
        {
            return new GenericOcrLine()
            {
                Words = new List<GenericBoxDetection>()
                {
                    Get(textDetection)
                }
            };
        }

        private static GenericBoxDetection Get(AwsTextDetection awsGeometry)
        {
            return new GenericBoxDetection
            {
                DetectedText = awsGeometry.DetectedText,
                Confidence = awsGeometry.Confidence,
                BoundingBox = new GenericBoundingBox
                {
                    Height = awsGeometry.Geometry.BoundingBox.Height,
                    Width = awsGeometry.Geometry.BoundingBox.Width,
                    Left = awsGeometry.Geometry.BoundingBox.Left,
                    Top = awsGeometry.Geometry.BoundingBox.Top
                }
            };
        }
    }
}
