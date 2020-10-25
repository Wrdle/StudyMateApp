using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }
        public byte[] ProfilePictureBytes { get; set; }
        public List<string> CurrentSubjects { get; set; }
        public List<string> PreviousSubjects { get; set; }
        public List<string> Skills { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Group> Groups { get; set; }

        public User()
        {
            CurrentSubjects = new List<string>();
            PreviousSubjects = new List<string>();
            Skills = new List<string>();
            Assignments = new List<Assignment>();
            Groups = new List<Group>();
        }


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
