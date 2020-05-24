using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper.ImageManipulation
{
    public class ImageMagickCompressor : IImageCompressor
    {
        private readonly bool losless;

        public ImageMagickCompressor(bool losless)
        {
            this.losless = losless;
        }
        public string CompressInPlace(string imagePath)
        {
            MagickFormat format = MagickFormat.Jpeg;
            string targetPath = $"{imagePath}.{format}";
            var fileInfo = new FileInfo(imagePath);
            var optimizer = new ImageOptimizer();

            ChangeFileFormat(imagePath, targetPath, format);
            if (losless)
                optimizer.LosslessCompress(targetPath);
            else
                optimizer.Compress(fileInfo);
            return targetPath;

        }

        public List<Tuple<string, long>> TestAllCompressions(string imagePath)
        {
            var values = Enum.GetValues(typeof(MagickFormat)).Cast<MagickFormat>();
            var lengthsForDifferentFileFormats = new List<Tuple<string, long>>();
            var destinationPath = "xxx";
            foreach (var value in values)
            {
                try {
                    ChangeFileFormat(imagePath, destinationPath, value);
                    FileInfo fileInfo = new FileInfo(destinationPath);
                    lengthsForDifferentFileFormats.Add(Tuple.Create(value.ToString(), fileInfo.Length));
                } catch (Exception e) { }
                
            }
            var xx = lengthsForDifferentFileFormats.OrderBy(c => c.Item2).ToList();
            return xx;
            
        }

        private static void ChangeFileFormat(string imagePath, string destinationPath, MagickFormat value)
        {
            using (MagickImage img = new MagickImage(imagePath))
            {
                img.Format = value;

                img.Write(destinationPath);
            }
        }
    }
}
