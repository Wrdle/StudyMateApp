using Mobile.ViewModels;
using Mobile.ViewModels.Groups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Groups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //Rg.Plugins.Popup.Pages.PopupPage
    public partial class AddGroupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        //------------------------------
        //          Fields
        //------------------------------

        private AddGroupViewModel _viewModel;
        private GroupsViewModel _parentViewModel;

        //------------------------------
        //          Constructors
        //------------------------------

        public AddGroupPage(GroupsViewModel parentViewModel)
        {
            CloseWhenBackgroundIsClicked = true;
            InitializeComponent();
            BindingContext = _viewModel = new AddGroupViewModel();
            _parentViewModel = parentViewModel;
        }

        //------------------------------
        //          Methods
        //------------------------------

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void OnClick_Cancel(object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnClick_Create(object sender, System.EventArgs e)
        {
            await ((AddGroupViewModel)BindingContext).CreateGroup();
            await _parentViewModel.ExecuteLoadGroupsCommand();
            await PopupNavigation.Instance.PopAsync();
        }

    }
}