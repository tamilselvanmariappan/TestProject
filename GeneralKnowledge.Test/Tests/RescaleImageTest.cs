using ImageProcessor;
using System.IO;
using System.Net;
using System.Drawing;
using ImageProcessor.Imaging.Formats;
using System;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Image rescaling
    /// </summary>
    public class RescaleImageTest : ITest
    {
        public void Run()
        {
            // TODO
            // Grab an image from a public URL and write a function that rescales the image to a desired format
            // The use of 3rd party plugins is permitted
            // For example: 100x80 (thumbnail) and 1200x1600 (preview)
            string imageUrl = "https://i.picsum.photos/id/1029/4887/2759.jpg";
            Console.WriteLine($"\n\nRescaleImageTest : Image from ${imageUrl} in progress...");
            Size thumbnailSize = new Size(100, 80);
            ISupportedImageFormat thumbnailFormat = new PngFormat { Quality = 70 };
            RescaleImage(imageUrl, thumbnailSize, thumbnailFormat, "thumbnail.png");

            Size previewSize = new Size(1200, 1600);
            ISupportedImageFormat previewFormat = new JpegFormat { Quality = 90 };
            RescaleImage(imageUrl, previewSize, previewFormat, "preview.jpg");
        }

        public void RescaleImage(string imageUrl, Size size, ISupportedImageFormat format, string fileName)
        {
            using (WebClient webClient = new WebClient())
            {
                using (Stream inStream = webClient.OpenRead(imageUrl))
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                        imageFactory.Load(inStream)
                        .Resize(size)
                        .Format(format)
                        .Save(filePath);
                        Console.WriteLine($"\n{ fileName} resized and saved in : { filePath}");
                    }
                }
            }
        }
    }
}
