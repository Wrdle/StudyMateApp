using Mobile.ViewModels;
using Mobile.Views.Profile;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        Mobile.ViewModels.ProfileViewModel _viewModel;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ViewModels.ProfileViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            _viewModel.ExecuteLoadUserProfile();
        }

        protected async void AddSkillPopupCommandAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddSkillPopup((ProfileViewModel)BindingContext));
        }

        protected async void AddSubjectPopupCommandAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddSubjectPopup((ProfileViewModel)BindingContext));
        }

        protected async void EditProfilePopupCommandAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new EditProfilePopup((ProfileViewModel)BindingContext));
        }
    }
}