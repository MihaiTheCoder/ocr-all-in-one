using System;

namespace WindowsOcrWrapper.WinOcrResults
{
    public class Word
    {
        public BoundingRect BoundingRect { get; set; }

        public string Text { get; set; }

        internal static Word FromDynamic(dynamic ocrWord)
        {
            Word word = new Word();
            word.Text = ocrWord.Text;
            word.BoundingRect = BoundingRect.FromDynamic(ocrWord.BoundingRect);
            return word;
        }
    }
}
