using Xamarin.Forms;

namespace Mobile.Models
{
    public class GroupListItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ImageSource CoverPhoto { get; set; }
        public int? CoverColorId { get; set; }
    }
}
