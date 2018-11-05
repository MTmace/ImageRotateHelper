using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ImageRotateHelper.Code
{
    public class RotateImage
    {
        #region '   Declarations    '

        public delegate void RotatingImage(string imagePath);
        public event RotatingImage OnRotatingImage;

        public delegate void RotateComplete(string imagePath);
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

        public void ProcessImage(string FullPath, ExifMetaInfo EMI)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(FullPath);

            // EXIT IF IMAGE IS IN CORRECT ORIENTATION
            if (EMI.Orientation.Key == 1)
                return;

            switch (EMI.Orientation.Key)
            {
                case 1:
                    // IS IN CORRECT ORIENTATION
                    break;
                case 2:
                    if (this.OnRotatingImage != null)
                        this.OnRotatingImage(FullPath);

                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    if (this.OnRotatingImage != null)
                        this.OnRotatingImage(FullPath);

                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

                    break;
                case 7:
                    break;
                case 8:
                    if (this.OnRotatingImage != null)
                        this.OnRotatingImage(FullPath);

                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);

                    break;
                default:
                    break;
            }

            try
            {
                // TODO: SAVE AS CORRECT IMAGE TYPE
                image.Save(FullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                //throw;
            }

            if (this.OnRotateComplete != null)
                this.OnRotateComplete(FullPath);

            image.Dispose();
        }

        #endregion
    }
}
