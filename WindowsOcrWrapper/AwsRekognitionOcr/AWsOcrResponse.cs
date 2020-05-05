using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper.AwsRekognitionOcr
{
    public class AwsOcrResponse
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
    }
}
