using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.AwsRekognitionOcr
{
    public class AwsOcrResponse: IMappableToGenericResponse
    {
        private int width;
        private int height;
        
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
        public string InputImage { get; set ; }

        public GenericOcrResponse Map()
        {
            return Map(this);
        }

        public GenericOcrResponse Map(AwsOcrResponse awsResponse)
        {
            using (MagickImage img = new MagickImage(awsResponse.InputImage))
            {
                this.width = img.Width;
                this.height = img.Height;
            }

            var response = new GenericOcrResponse();
            response.SummaryText = string.Join(" ", awsResponse.TextDetections.Select(td => td.DetectedText));

            response.Detections = awsResponse.TextDetections.Select(td => Get(td)).ToList();
            return response;
        }

        private GenericBoxDetection Get(AwsTextDetection awsGeometry)
        {
            return new GenericBoxDetection
            {
                DetectedText = awsGeometry.DetectedText,
                Confidence = awsGeometry.Confidence,
                BoundingBox = new GenericBoundingBox
                {
                    Left = (int)(awsGeometry.Geometry.BoundingBox.Left * width),
                    Top = (int)(awsGeometry.Geometry.BoundingBox.Top* height),
                    Width = (int)(awsGeometry.Geometry.BoundingBox.Width * width),
                    Height = (int)(awsGeometry.Geometry.BoundingBox.Height * height)                                      
                }
            };
        }
    }
}
/*
Left coordinate = BoundingBox.Left(0.3922065) * image width(608) = 238

Top coordinate = BoundingBox.Top(0.15567766) * image height(588) = 91

Face width = BoundingBox.Width(0.284666) * image width(608) = 173

Face height = BoundingBox.Height(0.2930403) * image height(588) = 172*/