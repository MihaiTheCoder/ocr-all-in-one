using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocr.Wrapper.ToAlto;
using System.Collections.Generic;
using System.Linq;

namespace Ocr.Wrapper.Tests.ToAlto
{
    [TestClass]
    public class GenericResponseToAltoTests
    {
        GenericOcrResponse genericOcrResponse;

        [TestInitialize()]
        public void GenericResponseToAltoTestInitialize()
        {
            genericOcrResponse = new GenericOcrResponse();
            genericOcrResponse.SummaryText = "";
            genericOcrResponse.Language = "";
            genericOcrResponse.SoftwareName = "";
            genericOcrResponse.ImageFileName = "";
            genericOcrResponse.Lines = new List<GenericOcrLine>()
            {
                new GenericOcrLine()
                {
                     Words = new List<GenericBoxDetection>()
                     {
                         new GenericBoxDetection()
                         {
                             DetectedText = "",
                             Confidence = 0,
                             BoundingBox = new GenericBoundingBox()
                             {
                                 Height=0,
                                 Left = 0,
                                 Top = 0,
                                 Width = 0
                             }
                         }
                     }
                }
            };
        }


        [TestMethod]
        public void SoftwareName()
        {
            var expected = "Test Software Name";
            genericOcrResponse.SoftwareName = expected;

            var c = new ConvertToAlto();
            var alto = c.Convert(genericOcrResponse);

            Assert.AreEqual(
                expected,
                alto.description.ocrProcessing[0].ocrProcessingStep.processingSoftware.softwareName);
        }

        [TestMethod]
        public void ImageFileName()
        {
            var expected = "Test Image File Name";
            genericOcrResponse.ImageFileName = expected;

            var c = new ConvertToAlto();
            var alto = c.Convert(genericOcrResponse);

            Assert.AreEqual(expected, alto.description.sourceImageInformation.fileName);
        }

        [TestMethod]
        public void Language()
        {
            var expected = "Test Language";
            genericOcrResponse.Language = expected;

            var c = new ConvertToAlto();
            var alto = c.Convert(genericOcrResponse);

            Assert.AreEqual(
                expected,
                alto.layout.page[0].printSpace.textBlockOrIllustrationOrGraphicalElement[0].language,
                alto.layout.page[0].printSpace.textBlockOrIllustrationOrGraphicalElement[0].lang);
        }


        [TestMethod]
        public void Height()
        {
            var expected = 100;
            genericOcrResponse.Lines[0].Words[0].BoundingBox.Height = expected;

            var c = new ConvertToAlto();
            var alto = c.Convert(genericOcrResponse);

            Assert.AreEqual(expected, alto.layout.page[0].printSpace.textBlockOrIllustrationOrGraphicalElement[0].height);
        }

        [TestMethod]
        public void Width()
        {
            var expected = 100;
            genericOcrResponse.Lines[0].Words[0].BoundingBox.Width = expected;

            var c = new ConvertToAlto();
            var alto = c.Convert(genericOcrResponse);

            Assert.AreEqual(expected, alto.layout.page[0].printSpace.textBlockOrIllustrationOrGraphicalElement[0].width);
        }


        [TestMethod]
        public void FirstLine()
        {
            var expected = "Detected text on first line";
            genericOcrResponse.Lines[0].Words[0].DetectedText = expected;

            var c = new ConvertToAlto();
            var alto = c.Convert(genericOcrResponse);

            var str = (stringType)alto.layout.page[0].printSpace.textBlockOrIllustrationOrGraphicalElement[0].textLine[0].stringAndSP[0];

            Assert.AreEqual(expected, str.content);
        }

    }
}
 