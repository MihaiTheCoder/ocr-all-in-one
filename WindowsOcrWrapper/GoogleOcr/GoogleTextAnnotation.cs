using System.Collections.Generic;

namespace WindowsOcrWrapper.GoogleOcr
{
    public class GoogleTextAnnotation
    {
        public string Locale { get; set; }

        public string Description { get; set; }

        public GoogleBoundingPoly BoundingPoly { get; set; }

    }
}

