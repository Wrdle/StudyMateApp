using System;
using System.IO;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class GroupListItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] CoverPhotoBytes { get; set; }
        public CoverColor CoverColor { get; set; }

        public string SemesterAndYear
        {
            get
            {
                var semester = DateCreated.Month > 6 ? 2 : 1;
                var year = DateCreated.Year;
                return $"Semester {semester} | {year}";
            }
        }

        public ImageSource CoverPhoto
        {
            get
            {
                if (CoverPhotoBytes != null)
                    return ImageSource.FromStream(() => new MemoryStream(CoverPhotoBytes));
                return null;
            }
            set
            {
                if (value != null)
                {
                    var cancellationToken = System.Threading.CancellationToken.None;
                    using (var imageStream = ((StreamImageSource)value).Stream(cancellationToken).Result)
                    using (var byteStream = new MemoryStream())
                    {
                        imageStream.CopyTo(byteStream);
                        CoverPhotoBytes = byteStream.ToArray();
                    }
                }
                else
                {
                    CoverPhotoBytes = new byte[] { };
                }
            }
        }
    }
}
