namespace Ocr.Wrapper.AwsRekognitionOcr
{
    public class AwsTextDetection
    {
        /// <summary>
        /// The confidence that Amazon Rekognition has in the accuracy of the detected text
        /// and the accuracy of the geometry points around the detected text.
        /// Possible values 0-100
        /// </summary>
        public float Confidence { get; set; }
        
        /// <summary>
        /// Gets and sets the property DetectedText.
        /// The word or line of text recognized by Amazon Rekognition.
        /// </summary>
        public string DetectedText { get; set; }
       
        /// <summary>
        /// Gets and sets the property Geometry.
        /// The location of the detected text on the image. Includes an axis aligned coarse
        /// bounding box surrounding the text and a finer grain polygon for more accurate
        /// spatial information.
        /// </summary>
        public AwsGeometry Geometry { get; set; }

        /// <summary>
        /// Gets and sets the property Type.
        /// The type of text that was detected.
        /// </summary>
        public string Type { get; set; }
    }
}