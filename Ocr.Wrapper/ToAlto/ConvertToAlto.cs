using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.ToAlto
{
    public class ConvertToAlto
    {
        public AltoType Convert(GenericOcrResponse genericOcrResponse)
        {
            var alto = new AltoType();
            alto.description = new DescriptionType()
            {
                measurementUnit = MeasurementUnitType.pixel,
                sourceImageInformation = new SourceImageInformationType()
                {
                    fileName = genericOcrResponse.ImageFileName
                }
            };

            alto.description.ocrProcessing = new List<DescriptionType.OCRProcessing>()
            {
                new DescriptionType.OCRProcessing()
                {
                     ocrProcessingStep = new ProcessingStepType()
                     {
                         processingSoftware = new ProcessingSoftwareType()
                         {
                             softwareName = genericOcrResponse.SoftwareName
                         }
                    }
                }
            };

            alto.layout = new LayoutType()
            {
                page = new List<PageType>()
                {
                    new PageType()
                    {
                        printSpace = new PageSpaceType()
                        {
                            textBlockOrIllustrationOrGraphicalElement = new List<TextBlockType>()
                            {
                                new TextBlockType()
                                {
                                    height = genericOcrResponse.Lines[0].Words[0].BoundingBox.Height,
                                    width = genericOcrResponse.Lines[0].Words[0].BoundingBox.Width,
                                    language = genericOcrResponse.Language,
                                    lang = genericOcrResponse.Language,
                                    textLine = genericOcrResponse.Lines.Select(Map).ToList()
                                }
                            }
                        }
                    }
                }
            };

            return alto;
        }

        public static TextBlockType.TextLine Map(GenericOcrLine line)
        {
            return new TextBlockType.TextLine()
            {
                stringAndSP = line.Words.Select(Get).Select(x=>(object)x).ToList()
            };
        }

        private static stringType Get(GenericBoxDetection word)
        {
            return new stringType
            {
                content = word.DetectedText,
                width = word.BoundingBox.Width,
                height = word.BoundingBox.Height
            };
        }


    }
}

