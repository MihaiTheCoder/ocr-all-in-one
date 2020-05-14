using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocr.Wrapper.ImageManipulation;
using System.IO;

namespace Ocr.Wrapper.Tests
{
    [TestClass()]
    public class ImageMagickCompressorTests
    {

        //[TestMethod]
        public void MyTestMethod()
        {
            var filePath = "data/TLCShot.png";

            var fileInfo = new FileInfo(Path.GetFullPath(filePath));

            ImageMagickCompressor compressor = new ImageMagickCompressor(true);
            compressor.TestAllCompressions(filePath);
            Assert.IsTrue(true);
        }
        
        [TestMethod()]
        public void CompressInPlaceLoslessTest()
        {
            var filePath = "data/TLCShot.png";

            var fileInfo = new FileInfo(filePath);

            var oldFileSize = fileInfo.Length;
            ImageMagickCompressor compressor = new ImageMagickCompressor(true);
            filePath = compressor.CompressInPlace(fileInfo.FullName);
            fileInfo = new FileInfo(filePath);
            var newFileSize = fileInfo.Length;
            Assert.IsTrue(newFileSize < oldFileSize);
        }

        [TestMethod()]
        public void CompressInPlaceLossyTest()
        {
            var filePath = "data/TLCShot.png";

            var fileInfo = new FileInfo(filePath);

            var oldFileSize = fileInfo.Length;
            ImageMagickCompressor compressor = new ImageMagickCompressor(false);
            var compressedFile = compressor.CompressInPlace(fileInfo.FullName);
            fileInfo = new FileInfo(compressedFile);            
            var newFileSize = fileInfo.Length;
            Assert.IsTrue(newFileSize < oldFileSize);
        }
    }
}