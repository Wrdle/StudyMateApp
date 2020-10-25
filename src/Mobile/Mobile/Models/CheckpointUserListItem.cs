using System.IO;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class CheckpointUserListItem
    {
        public long Id { get; set; }
        public byte[] ProfilePictureBytes { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ImageSource ProfilePicture
        {
            get
            {
                if (ProfilePictureBytes != null)
                    return ImageSource.FromStream(() => new MemoryStream(ProfilePictureBytes));
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
                        ProfilePictureBytes = byteStream.ToArray();
                    }
                }
                else
                {
                    ProfilePictureBytes = new byte[] { };
                }
            }
        }
    }
}
