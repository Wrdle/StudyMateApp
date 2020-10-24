using Xamarin.Forms;

namespace Mobile.Models
{
    public class CheckpointUserListItem
    {
        public long Id { get; set; }
        public ImageSource ProfilePicture { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
