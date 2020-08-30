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

        public List<AzureOcrRegion> Regions { get; set; }

        public string InputImage { get; set; }

        public static AzureOcrResults FromDynamic(dynamic ocrResult)
        {
            AzureOcrResults azureOcrResults = new AzureOcrResults();
            azureOcrResults.Language = ocrResult.language;
            azureOcrResults.TextAngle = ocrResult.textAngle;
            azureOcrResults.Orientation = ocrResult.orientation;
            azureOcrResults.Regions = new List<AzureOcrRegion>();
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

        public static GenericOcrResponse Map(AzureOcrResults azureOcrResults)
        {
            return new GenericOcrResponse
            {
                Detections = azureOcrResults.Regions.SelectMany(r => r.Lines).SelectMany(l => l.Words).Select(w => Get(w)).ToList(),
                Language = azureOcrResults.Language,
                SummaryText = string.Join(Environment.NewLine, azureOcrResults.Regions.Select(r => GetText(r)))
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
                    Left = word.ParsedBoundingBox[0],
                    Top = word.ParsedBoundingBox[1],
                    Width = word.ParsedBoundingBox[2],
                    Height = word.ParsedBoundingBox[3],
                }
            };
        }
    }
}
