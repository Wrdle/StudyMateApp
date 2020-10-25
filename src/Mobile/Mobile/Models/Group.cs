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
        public ICollection<UserListItem> Members { get; private set; }
        public ICollection<AssignmentListItem> Assignments { get; private set; }

        public Group()
        {
            Members = new List<UserListItem>();
            Assignments = new List<AssignmentListItem>();
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