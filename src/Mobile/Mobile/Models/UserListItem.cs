using System.IO;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class UserListItem
    {
        public long Id { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name { get => $"{FirstName} {LastName}"; }

        public ImageSource ProfilePictureSource
        {
            get
            {
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    return ImageSource.FromStream(() => new MemoryStream(ProfilePicture));
                }

                return null;
            }
        }
    }
}
