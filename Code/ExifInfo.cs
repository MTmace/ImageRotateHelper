using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Linq;

namespace ImageRotateHelper.Code
{
    public class ExifMetaInfo
    {
        #region '   Declarations   '
        
        public BitmapMetadata _metaInfo;

        public enum EnumColorRepresentation
        {
            sRGB,
            Uncalibrated
        }
        public enum EnumFlashMode
        {
            FlashFired,
            FlashNotFire
        }

        public Dictionary<int, string> Orientations { get; set; }

        #endregion
        #region '   Constructors    '

        public ExifMetaInfo(Uri ImageUri)
        {
            BitmapFrame bFrame = BitmapFrame.Create(ImageUri, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            
            _metaInfo = (BitmapMetadata)bFrame.Metadata;

            this.Orientations = new Dictionary<int, string>();
            this.Orientations.Add(1,"Top / Left Side");
            this.Orientations.Add(2,"Top / Right Side");
            this.Orientations.Add(3,"Bottom / Right Side");
            this.Orientations.Add(4,"Bottom / Left Side");
            this.Orientations.Add(5,"Left Side / Top");
            this.Orientations.Add(6,"Right Side / Top");
            this.Orientations.Add(7,"Right Side / Bottom");
            this.Orientations.Add(8,"Left Side / Bottom");

            bFrame = null;
            
        }

        public ExifMetaInfo(string ImagePath) : this(new Uri(ImagePath))
        {

        }

        #endregion
        #region '   Properties  '

        public uint? Width
        {
            get
            {
                object obj = this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=40962}");
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    if (obj.GetType() == typeof(UInt32))
                        return (uint)obj;
                    else
                        return Convert.ToUInt32(obj);
                }
            }
        }

        public uint? Height
        {
            get
            {
                object obj = this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=40963}");
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    if (obj.GetType() == typeof(UInt32))
                        return (uint)obj;
                    else
                        return Convert.ToUInt32(obj);
                }
            }
        }

        public string EquipmentManufacturer
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/exif:{uint=271}");
                return (val != null ? (string)val : String.Empty);
            }
        }

        public string CameraModel
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/exif:{uint=272}");
                return (val != null ? (string)val : String.Empty);
            }
        }

        public string CreationSoftware
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/exif:{uint=305}");
                return (val != null ? (string)val : String.Empty);
            }
        }

        public KeyValuePair<int, string> Orientation
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/exif:{ushort=274}");
                if (val != null)
                    return this.Orientations.First(o => o.Key == int.Parse(val.ToString()));
                else
                    return new KeyValuePair<int,string>(0, string.Empty);
            }
        }

        public Int16? GPSLat
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/gps:{ushort=2}");
                if (val != null)
                    return Int16.Parse(val.ToString());
                else
                    return null;
            }
        }

        public EnumColorRepresentation ColorRepresentation
        {
            get
            {
                if ((ushort)this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=40961}") == 1)
                    return EnumColorRepresentation.sRGB;
                else
                    return EnumColorRepresentation.Uncalibrated;
            }
        }

        public decimal? ExposureTime
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=33434}");
                if (val != null)
                {
                    return ParseUnsigned((ulong)val);
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? LensAperture
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=33437}");
                if (val != null)
                {
                    return ParseUnsigned((ulong)val);
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? FocalLength
        {
            get
            {
                object val = this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=37386}");
                if (val != null)
                {
                    return ParseUnsigned((ulong)val);
                }
                else
                {
                    return null;
                }
            }
        }

        public ushort? IsoSpeed
        {
            get
            {
                return (ushort?)this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=34855}");
            }
        }

        public EnumFlashMode FlashMode
        {
            get
            {
                if ((ushort)this.GetMetaInfo("/app1/ifd/exif/subifd:{uint=37385}") % 2 == 1)
                    return EnumFlashMode.FlashFired;
                else
                    return EnumFlashMode.FlashNotFire;
            }
        }

        #endregion
        #region '   Methods '

        private decimal ParseUnsigned(ulong exifValue)
        {
            return (decimal)(exifValue & 0xFFFFFFFFL) / (decimal)((exifValue & 0xFFFFFFFF00000000L) >> 32);
        }

        private object GetMetaInfo(string infoQuery)
        {
            if (_metaInfo.ContainsQuery(infoQuery))
                return _metaInfo.GetQuery(infoQuery);
            else
                return null;
        }

        #endregion
    }
}
