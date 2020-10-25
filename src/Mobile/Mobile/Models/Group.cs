using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] CoverPhotoBytes { get; set; }
        public CoverColor CoverColor { get; set; }
        public ICollection<UserListItem> Members { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

        public Group()
        {
            Members = new List<UserListItem>();
            Assignments = new List<Assignment>();
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

        public string SemesterAndYear
        {
            get
            {
                var semester = DateCreated.Month > 6 ? 2 : 1;
                var year = DateCreated.Year;
                return $"Semester {semester} | {year}";
            }
        }

    }
}