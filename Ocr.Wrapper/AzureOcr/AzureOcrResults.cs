using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.AzureOcr
{
    public class AzureOcrResults : IMappableToGenericResponse
    {
        public string Language { get; set; }

        public float TextAngle { get; set; }

        public string Orientation { get; set; }

        public string ImageFileName { get; set; }

        public string SoftwareName { get; set; }

        public List<AzureOcrRegion> Regions { get; set; }

        public static AzureOcrResults FromDynamic(dynamic ocrResult, string imageFileName,string ocrName)
        {
            AzureOcrResults azureOcrResults = new AzureOcrResults();
            azureOcrResults.Language = ocrResult.language;
            azureOcrResults.TextAngle = ocrResult.textAngle;
            azureOcrResults.Orientation = ocrResult.orientation;
            azureOcrResults.Regions = new List<AzureOcrRegion>();
            azureOcrResults.ImageFileName = imageFileName;
            azureOcrResults.SoftwareName = ocrName;
            foreach (dynamic region in ocrResult.regions)
            {
                azureOcrResults.Regions.Add(AzureOcrRegion.FromDynamic(region));
            }
            
            return azureOcrResults;
        }

        public GenericOcrResponse Map()
        {
            return Map(this);
        }

        public static GenericOcrLine Map(AzureOcrLine line)
        {
            return new GenericOcrLine()
            {
                Words = line.Words.Select(Get).ToList()
            };
        }

        public static GenericOcrResponse Map(AzureOcrResults azureOcrResults)
        {
            return new GenericOcrResponse
            {
                Lines = azureOcrResults.Regions.SelectMany(r => r.Lines).Select(Map).ToList(),
                Language = azureOcrResults.Language,
                SummaryText = string.Join(Environment.NewLine, azureOcrResults.Regions.Select(r => GetText(r))),
                SoftwareName = azureOcrResults.SoftwareName,
                ImageFileName = azureOcrResults.ImageFileName
            };
        }

        private static string GetText(AzureOcrRegion r)
        {
            return string.Join(Environment.NewLine, r.Lines.Select(l => string.Join(" ", l.Words.Select(w => w.Text))));
        }

        private static GenericBoxDetection Get(AzureOcrWord word)
        {
            return new GenericBoxDetection
            {
                DetectedText = word.Text,
                BoundingBox = new GenericBoundingBox
                {
                    Height = word.BoundingBox[0],
                    Width = word.BoundingBox[1],
                    Left = word.BoundingBox[2],
                    Top = word.BoundingBox[3],
                }
            };
        }
    }
}
