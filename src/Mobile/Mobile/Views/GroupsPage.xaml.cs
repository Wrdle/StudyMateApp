using Mobile.ViewModels;
using Mobile.Views.Groups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsPage : ContentPage
    {
        GroupsViewModel _viewModel;
        public GroupsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new GroupsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void OnClick_AddGroup(object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddGroupPage((GroupsViewModel)BindingContext));
        }
    }
}