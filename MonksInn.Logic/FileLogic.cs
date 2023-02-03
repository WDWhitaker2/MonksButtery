using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace MonksInn.Logic
{
    public class FileLogic : BaseLogic
    {
        public FileLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public Domain.File AddFile(Domain.File file)
        {

            file.DateCreated = DateTime.Now;
            return Uow.DbContext.Files.Add(file);
        }


        public IQueryable<Domain.File> GetAllFiles(params string[] includes)
        {
            return Uow.DbContext.Files.AsQueryable(true, includes);
        }

        public Domain.File GetFile(Guid id)
        {
            return Uow.DbContext.Files.AsQueryable(false).FirstOrDefault(a => a.Id == id);
        }


        

        public void CompressBeerImage(Guid beerId)
        {
            var beer = Uow.DbContext.Beers.AsQueryable(false).FirstOrDefault(a => a.Id == beerId);
            if (beer != null && beer.ImageId != null)
            {
                var beerThumb = Uow.DbContext.Files.AsQueryable().FirstOrDefault(a => a.Id == beer.ImageId);
                CompressImage(beerThumb, 500);
            }
        }

        public void CompressImage(Domain.File file, int w = 0)
        {
            if (w > 0)
            {

                using (MemoryStream memoryStream = new MemoryStream(file.FileData))
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(memoryStream))
                {
                    NormalizeOrientation(img);

                    if(img.Width > w)
                    {
                        using (System.Drawing.Image imgResized = ScaleImage(img, w, 0))
                        using (MemoryStream memoryStreamResized = new MemoryStream())
                        {
                            EncoderParameter encoderParameter1 = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);

                            EncoderParameters encoderParameter2 = new EncoderParameters(1);
                            encoderParameter2.Param[0] = encoderParameter1;

                            ImageCodecInfo encoderInfo;
                            if (img.PixelFormat == PixelFormat.Format1bppIndexed | img.PixelFormat == PixelFormat.Format4bppIndexed | img.PixelFormat == PixelFormat.Format8bppIndexed | img.PixelFormat == PixelFormat.Undefined | img.PixelFormat == PixelFormat.DontCare | img.PixelFormat == PixelFormat.Format16bppArgb1555 | img.PixelFormat == PixelFormat.Format16bppGrayScale)
                            {
                                encoderInfo = GetEncoderInfo("image/png");
                            }
                            else
                            {
                                encoderInfo = GetEncoderInfo(file.MimeType);
                            }

                            imgResized.Save(memoryStreamResized, encoderInfo, encoderParameter2);

                            file.MimeType = encoderInfo.MimeType;
                            file.FileData = memoryStreamResized.ToArray();
                        }
                    }

                   
                }
            }

        }

        protected Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var newImage = new Bitmap(image);
            var newWidth = 0;
            var newHeight = 0;

            if (maxWidth > 0 && maxHeight > 0)
            {
                var ratioX = (double)maxWidth / (double)image.Width;
                var ratioY = (double)maxHeight / (double)image.Height;
                var ratio = Math.Min(ratioX, ratioY);

                newWidth = (int)(image.Width * ratio);
                newHeight = (int)(image.Height * ratio);

                newImage = new Bitmap(newWidth, newHeight);

                using (var graphics = Graphics.FromImage(newImage))
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            else if (maxWidth > 0 && maxHeight == 0)
            {
                var ratioX = (double)maxWidth / (double)image.Width;

                newWidth = (int)(image.Width * ratioX);
                newHeight = (int)(image.Height * ratioX);

                newImage = new Bitmap(newWidth, newHeight);

                using (var graphics = Graphics.FromImage(newImage))
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            else
            {
                var ratioY = (double)maxHeight / (double)image.Height;

                newWidth = (int)(image.Width * ratioY);
                newHeight = (int)(image.Height * ratioY);

                newImage = new Bitmap(newWidth, newHeight);

                using (var graphics = Graphics.FromImage(newImage))
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
        protected ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        protected void NormalizeOrientation(Image image)
        {
            int ExifOrientationTagId = 0x112; //274

            if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
            {
                int orientation;

                orientation = image.GetPropertyItem(ExifOrientationTagId).Value[0];

                if (orientation >= 1 && orientation <= 8)
                {
                    switch (orientation)
                    {
                        case 2:
                            image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 3:
                            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 4:
                            image.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 5:
                            image.RotateFlip(RotateFlipType.Rotate90FlipX);
                            break;
                        case 6:
                            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 7:
                            image.RotateFlip(RotateFlipType.Rotate270FlipX);
                            break;
                        case 8:
                            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }

                    image.RemovePropertyItem(ExifOrientationTagId);
                }
            }
        }
    }
}
