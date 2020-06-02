using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocr.Wrapper.AwsRekognitionOcr;
using Ocr.Wrapper.AzureOcr;
using Ocr.Wrapper.GoogleOcr;
using Ocr.Wrapper.TesseractOcr;
using Ocr.Wrapper.WindowsOcr;

namespace Ocr.Wrapper
{
    public class GenericOcrResponse
    {
        public string SummaryText { get; set; }

        public string Language { get; set; }

        public string ImageFileName { get; set; }

        public string SoftwareName { get; set; }

        public List<GenericOcrLine> Lines { get; set; }
    }

    public class GenericOcrLine
    {
        public List<GenericBoxDetection> Words { get; set; }
    }

    public class GenericBoxDetection
    {
        public string DetectedText { get; set; }

        public float? Confidence { get; set; }

        public GenericBoundingBox BoundingBox { get; set; }

    }

    public class GenericBoundingBox
    {
        public float Height { get; set; }

        public float Left { get; set; }

        public float Top { get; set; }

        public float Width { get; set; }
    }
}
