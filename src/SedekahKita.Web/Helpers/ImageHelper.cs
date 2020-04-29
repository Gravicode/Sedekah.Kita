
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SedekahKita.Web.Helpers
{
    public class ImageHelper
    {
        public static byte[] FixedSize(MemoryStream ms, int Width, int Height, bool needToFill)
        {

            Bitmap bmp;
            bmp = new Bitmap(ms);
            Image image = bmp;
            #region calculations
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);
            #endregion

            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.HighQuality;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                //Console.WriteLine("From: " + from.ToString());
                //Console.WriteLine("To: " + to.ToString());
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(bmPhoto, typeof(byte[]));
                //return bmPhoto;
            }
        }


    }
    public class ImageResizer
    {
        public void ResizeImage(string origFileLocation, string newFileLocation, string origFileName, string newFileName, int newWidth, int maxHeight, bool resizeIfWider)
        {
            System.Drawing.Image FullSizeImage = System.Drawing.Image.FromFile(origFileLocation + origFileName);
            // Ensure the generated thumbnail is not being used by rotating it 360 degrees
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (resizeIfWider)
            {
                if (FullSizeImage.Width <= newWidth)
                {
                    //newWidth = FullSizeImage.Width;
                }
            }

            int newHeight = FullSizeImage.Height * newWidth / FullSizeImage.Width;
            if (newHeight > maxHeight) // Height resize if necessary
            {
                //newWidth = FullSizeImage.Width * maxHeight / FullSizeImage.Height;
                newHeight = maxHeight;
            }
            newHeight = maxHeight;
            // Create the new image with the sizes we've calculated
            System.Drawing.Image NewImage = FullSizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
            FullSizeImage.Dispose();
            NewImage.Save(newFileLocation + newFileName);
        }
        public static byte[] ResizeImageAndRatio(MemoryStream ms, int Width, int Height, bool needToFill)//(string origFileLocation, string newFileLocation, string origFileName, string newFileName, int newWidth, int newHeight, bool resizeIfWider)
        {
            Bitmap bmp;
            bmp = new Bitmap(ms);
            Image initImage = bmp;

            //System.Drawing.Image initImage = System.Drawing.Image.FromFile(origFileLocation + origFileName);
            int templateWidth = Width;
            int templateHeight = Height;
            double templateRate = double.Parse(templateWidth.ToString()) / templateHeight;
            double initRate = double.Parse(initImage.Width.ToString()) / initImage.Height;
            if (templateRate == initRate)
            {

                System.Drawing.Image templateImage = new System.Drawing.Bitmap(templateWidth, templateHeight);
                System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                templateG.Clear(Color.White);
                templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, templateWidth, templateHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                //templateImage.Save(newFileLocation + newFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(templateImage, typeof(byte[]));
            }

            else
            {

                System.Drawing.Image pickedImage = null;
                System.Drawing.Graphics pickedG = null;


                Rectangle fromR = new Rectangle(0, 0, 0, 0);
                Rectangle toR = new Rectangle(0, 0, 0, 0);


                if (templateRate > initRate)
                {

                    pickedImage = new System.Drawing.Bitmap(initImage.Width, int.Parse(Math.Floor(initImage.Width / templateRate).ToString()));
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);


                    fromR.X = 0;
                    fromR.Y = int.Parse(Math.Floor((initImage.Height - initImage.Width / templateRate) / 2).ToString());
                    fromR.Width = initImage.Width;
                    fromR.Height = int.Parse(Math.Floor(initImage.Width / templateRate).ToString());


                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = initImage.Width;
                    toR.Height = int.Parse(Math.Floor(initImage.Width / templateRate).ToString());
                }

                else
                {
                    pickedImage = new System.Drawing.Bitmap(int.Parse(Math.Floor(initImage.Height * templateRate).ToString()), initImage.Height);
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                    fromR.X = int.Parse(Math.Floor((initImage.Width - initImage.Height * templateRate) / 2).ToString());
                    fromR.Y = 0;
                    fromR.Width = int.Parse(Math.Floor(initImage.Height * templateRate).ToString());
                    fromR.Height = initImage.Height;

                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = int.Parse(Math.Floor(initImage.Height * templateRate).ToString());
                    toR.Height = initImage.Height;
                }


                pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);


                System.Drawing.Image templateImage = new System.Drawing.Bitmap(templateWidth, templateHeight);
                System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                templateG.Clear(Color.White);
                templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, templateWidth, templateHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);
                //templateImage.Save(newFileLocation + newFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(templateImage, typeof(byte[]));

                templateG.Dispose();
                templateImage.Dispose();

                pickedG.Dispose();
                pickedImage.Dispose();
            }
            initImage.Dispose();
        }

    }
}
