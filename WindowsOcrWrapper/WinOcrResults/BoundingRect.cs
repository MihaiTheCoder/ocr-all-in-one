using System;

namespace WindowsOcrWrapper.WinOcrResults
{
    public class BoundingRect
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }

        public bool IsEmpty { get; set; }

        public static BoundingRect FromDynamic(dynamic boundingRect)
        {
            return new BoundingRect
            {
                X = boundingRect.X,
                Y = boundingRect.Y,
                Width = boundingRect.Width,
                Height = boundingRect.Height,
                Left = boundingRect.Left,
                Top = boundingRect.Top,
                Right = boundingRect.Right,
                Bottom = boundingRect.Bottom,
                IsEmpty = boundingRect.IsEmpty
            };
        }
    }
}
