using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper.TesseractOcr
{
    public class TesseractMapper
    {
        public static TesseractResponse Get(string tsvResult)
        {
            string[] lines = tsvResult.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();
            var header = lines[0];
            TesseractResponse response = new TesseractResponse();
            response.DetectionLines = new TesseractDetectionLine[lines.Length];

            for (int i = 1; i < lines.Length; i++)
            {
                var splitted = lines[i].Split('\t');

                response.DetectionLines[i - 1] = new TesseractDetectionLine
                {
                    Level = int.Parse(splitted[0]),
                    PageNumber = int.Parse(splitted[1]),
                    BlockNumber = int.Parse(splitted[2]),
                    PartNumber = int.Parse(splitted[3]),
                    LineNumber = int.Parse(splitted[4]),
                    WordNumber = int.Parse(splitted[5]),
                    Left = int.Parse(splitted[6]),
                    Top = int.Parse(splitted[7]),
                    Width = int.Parse(splitted[8]),
                    Height = int.Parse(splitted[9]),
                    Confidence = int.Parse(splitted[10]),
                    Text = splitted[11]
                };
            }
            response.DetectionLines = response.DetectionLines
                .Where(d => !string.IsNullOrWhiteSpace(d?.Text) && d.Confidence > 0)
                .ToArray();
            return response;
        }
    }
}
