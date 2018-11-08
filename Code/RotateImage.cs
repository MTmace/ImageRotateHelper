using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ImageRotateHelper.Code
{
    public class RotateImage
    {
        #region '   Declarations    '

        private const int exifOrientationID = 0x112; //274

        public delegate void RotatingImage(string imagePath, int degreesToRotated);
        public event RotatingImage OnRotatingImage;

        public delegate void RotateComplete(string imagePath, int degreesRotated);
        public event RotateComplete OnRotateComplete;

        #endregion
        #region '   Constructors    '

        public RotateImage()
        {
        }

        #endregion
        #region '   Properties  '

        #endregion
        #region '   Methods    '

        public void ProcessImage(string FullPath)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(FullPath);
            int degreesRotated = 0;

            if (!image.PropertyIdList.Contains(exifOrientationID))
                return;

            PropertyItem prop = image.GetPropertyItem(exifOrientationID);
            int val = BitConverter.ToUInt16(prop.Value, 0);
            RotateFlipType rot = RotateFlipType.RotateNoneFlipNone;

            switch (val)
            {
                case 1:
                    // IS IN CORRECT ORIENTATION
                    break;
                case 2:
                    break;
                case 3:
                    rot = RotateFlipType.Rotate180FlipNone;
                    break;
                case 4:
                    rot = RotateFlipType.Rotate180FlipNone;
                    break;
                case 5:
                    rot = RotateFlipType.Rotate90FlipNone;
                    break;
                case 6:
                    rot = RotateFlipType.Rotate90FlipNone;
                    break;
                case 7:
                    rot = RotateFlipType.Rotate270FlipNone;
                    break;
                case 8:
                    rot = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    break;
            }

            if (val == 2 || val == 4 || val == 5 || val == 7)
                rot |= RotateFlipType.RotateNoneFlipX;

            if (rot == RotateFlipType.RotateNoneFlipNone)
                return;

            image.RotateFlip(rot);

            try
            {
                EncoderParameters ep = new EncoderParameters();
                ImageCodecInfo ici = this.GetCodec();

                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)99);

                // TODO: SAVE AS CORRECT IMAGE TYPE
                image.Save(FullPath, ici, ep);
            }
            catch (Exception)
            {
                //throw;
            }

            if (this.OnRotateComplete != null)
                this.OnRotateComplete(FullPath, degreesRotated);

            image.Dispose();
        }

        //public void ProcessImage2(string FullPath, ExifMetaInfo EMI)
        //{
        //    System.Drawing.Bitmap image = new System.Drawing.Bitmap(FullPath);
        //    int degreesRotated = 0;


        //    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //    foreach (PropertyItem item in image.PropertyItems)
        //    {
        //        this.OnRotateComplete(string.Concat(item.Id, " :: ", encoding.GetString(item.Value)), degreesRotated);

        //    }

        //    if (!image.PropertyIdList.Contains(exifOrientationID))
        //        return;

        //    // EXIT IF IMAGE IS IN CORRECT ORIENTATION
        //    if (EMI.Orientation.Key == 1)
        //        return;

        //    switch (EMI.Orientation.Key)
        //    {
        //        case 1:
        //            // IS IN CORRECT ORIENTATION
        //            break;
        //        case 2:
        //            degreesRotated = 270;

        //            if (this.OnRotatingImage != null)
        //                this.OnRotatingImage(FullPath, degreesRotated);

        //            image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
        //            break;
        //        case 3:
        //            break;
        //        case 4:
        //            break;
        //        case 5:
        //            break;
        //        case 6:
        //            degreesRotated = 90;

        //            if (this.OnRotatingImage != null)
        //                this.OnRotatingImage(FullPath, 90);

        //            image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

        //            break;
        //        case 7:
        //            break;
        //        case 8:
        //            degreesRotated = 270;
        //            if (this.OnRotatingImage != null)
        //                this.OnRotatingImage(FullPath, degreesRotated);

        //            image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);

        //            break;
        //        default:
        //            break;
        //    }

        //    try
        //    {

        //        EncoderParameters ep = new EncoderParameters();
        //        ImageCodecInfo ici = this.GetCodec();

        //        ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);

        //        // TODO: SAVE AS CORRECT IMAGE TYPE
        //        image.Save(FullPath, ici, ep);
        //    }
        //    catch (Exception)
        //    {
        //        //throw;
        //    }

        //    if (this.OnRotateComplete != null)
        //        this.OnRotateComplete(FullPath, degreesRotated);

        //    image.Dispose();
        //}

        private ImageCodecInfo GetCodec()
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == "image/jpeg")
                    return codec;
            }

            throw new Exception("Unknown codec");
        }
        #endregion
    }
}
