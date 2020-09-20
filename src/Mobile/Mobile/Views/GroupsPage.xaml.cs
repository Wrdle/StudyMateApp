
using Mobile.Views.Groups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsPage : ContentPage
    {
        public GroupsPage()
        {
            InitializeComponent();
        }

        private async void OnClick_AddGroup(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new AddGroupPage(), false);
        }
    }
}