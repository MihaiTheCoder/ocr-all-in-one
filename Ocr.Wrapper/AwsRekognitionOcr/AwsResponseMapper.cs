using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.AwsRekognitionOcr
{
    public class AwsResponseMapper
    {
        public static AwsOcrResponse Get(DetectTextResponse detectTextResponse, string imagePath)
        {
            AwsOcrResponse ocrResponse = new AwsOcrResponse();
            ocrResponse.TextModelVersion = detectTextResponse.TextModelVersion;
            ocrResponse.TextDetections = detectTextResponse.TextDetections.Select(t => MapDetection(t)).ToList();
            return ocrResponse;
        }

        private static AwsTextDetection MapDetection(TextDetection t)
        {
            AwsTextDetection awsTextDetection = new AwsTextDetection();
            awsTextDetection.Confidence = t.Confidence;
            awsTextDetection.DetectedText = t.DetectedText;
            awsTextDetection.Geometry = GetGeometry(t.Geometry);
            awsTextDetection.Type = t.Type.Value;
            return awsTextDetection;
        }

        private static AwsGeometry GetGeometry(Geometry geometry)
        {
            AwsGeometry awsGeometry = new AwsGeometry();
            awsGeometry.BoundingBox = GetBoundingBox(geometry.BoundingBox);
            awsGeometry.Polygon = geometry.Polygon.Select(p => new AwsPoint { X = p.X, Y = p.Y }).ToList();
            return awsGeometry;
        }

        private static AwsBoundingBox GetBoundingBox(BoundingBox boundingBox)
        {
            AwsBoundingBox awsBoundingBox = new AwsBoundingBox
            {
                Height = boundingBox.Height,
                Left = boundingBox.Left,
                Top = boundingBox.Top,
                Width = boundingBox.Width
            };
            return awsBoundingBox;
        }
    }
}
