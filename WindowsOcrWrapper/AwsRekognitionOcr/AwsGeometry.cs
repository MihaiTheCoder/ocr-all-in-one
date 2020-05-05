using Amazon.Rekognition.Model;
using System.Collections.Generic;

namespace WindowsOcrWrapper.AwsRekognitionOcr
{
    public class AwsGeometry
    {
        /// <summary>
        /// Gets and sets the property BoundingBox.
        /// An axis-aligned coarse representation of the detected item's location on the image.
        /// </summary>
        public AwsBoundingBox BoundingBox { get; set; }
        
        /// <summary>
        /// Gets and sets the property Polygon.
        /// Within the bounding box, a fine-grained polygon around the detected item.
        /// </summary>
        public List<AwsPoint> Polygon { get; set; }
    }
}