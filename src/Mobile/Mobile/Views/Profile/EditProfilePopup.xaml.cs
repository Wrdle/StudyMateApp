using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.ViewModels;
using Mobile.ViewModels.Profile;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private EditProfileViewModel _viewModel;
        private ProfileViewModel _parentViewModel;

        string newFirstName;
        string newLastName;
        string newInstitution;
        string newMajor;

        public EditProfilePopup()
        {

        }

        public EditProfilePopup(ProfileViewModel parentViewModel)
        {
            CloseWhenBackgroundIsClicked = true;
            InitializeComponent();
            BindingContext = _viewModel = new EditProfileViewModel();
            _parentViewModel = parentViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void OnClick_Cancel(object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnClick_UpdateProfileInformation(object sender, System.EventArgs e)
        {
            newFirstName = ((EditProfileViewModel)BindingContext).UpdateFirstName();
            _parentViewModel.ExecuteUpdateFirstName(newFirstName);

            newLastName = ((EditProfileViewModel)BindingContext).UpdateLastName();
            _parentViewModel.ExecuteUpdateLastName(newLastName);

            newInstitution = ((EditProfileViewModel)BindingContext).UpdateInstitution();
            _parentViewModel.ExecuteUpdateInstitution(newInstitution);

            newMajor = ((EditProfileViewModel)BindingContext).UpdateMajor();
            _parentViewModel.ExecuteUpdateMajor(newMajor);

            await PopupNavigation.Instance.PopAsync();
        }
    }
}