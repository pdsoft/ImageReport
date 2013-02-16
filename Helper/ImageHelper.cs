using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using MigraDoc.DocumentObjectModel;

namespace ImageReport.Helper
{
    public class ImageHelper
    {

        //------------------------------------------------------------
        //------------------------------------------------------------
        public static void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;

            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            // Save resized picture
            NewImage.Save(NewFile);
        }

        //----------------------------------------------------------------------
        // by Fred, 1-19-2013
        //----------------------------------------------------------------------
        public static Orientation ResizeImage(HttpPostedFile postedFile, string filePath)
        {
            int maxWidth = GetConfigValue.MaxWidth;
            int maxHeight = GetConfigValue.MaxHeight;

            Image sourceImage = Image.FromStream(postedFile.InputStream);
            Image source = new Bitmap(sourceImage);

            int width = sourceImage.Width;
            int height = sourceImage.Height;

            //----------------------- resize if width exceed limitation
            if (width > maxWidth)
            {
                height = (height * maxWidth) / width;
                width = maxWidth;
            }

            //----------------------- resize if height exceed limitation
            if (height > maxHeight)
            {
                width = (width * maxHeight) / height;
                height = maxHeight;
            }

            if ((width != sourceImage.Width) || (height != sourceImage.Height))
            {
                source = new Bitmap(source, width, height);
            }

            source.Save(filePath, ImageFormat.Jpeg);

            //-------------------------------------------------
            Orientation ImgOrientation = Orientation.Landscape;
            if (height > width) { ImgOrientation = Orientation.Portrait; }

            return ImgOrientation;
        }

        //------------------------------------------------------------
        //------------------------------------------------------------
        public static byte[] GetThumbnail(HttpPostedFileBase imgFile)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(imgFile.InputStream);

            System.Drawing.Image thumbnailImage =
                image.GetThumbnailImage(100, Convert.ToInt32((image.Height / (image.Width / 100))), null, IntPtr.Zero);

            return imageToByteArray(thumbnailImage);
        }

        //------------------------------------------------------------
        //------------------------------------------------------------
        private static byte[] imageToByteArray(System.Drawing.Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }
    }
}