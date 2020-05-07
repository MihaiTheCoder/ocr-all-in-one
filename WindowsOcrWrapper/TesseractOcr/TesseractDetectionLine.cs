namespace WindowsOcrWrapper.TesseractOcr
{
    public class TesseractDetectionLine
    {
        public int Level { get; set; }

        public int PageNumber { get; set; }

        public int BlockNumber { get; set; }

        public int PartNumber { get; set; }

        public int LineNumber { get; set; }

        public int WordNumber { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Confidence { get; set; }

        public string Text { get; set; }
    }
}
