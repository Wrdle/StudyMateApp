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
    public partial class AddSubjectPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private AddSubjectPopupViewModel _viewModel;
        private ProfileViewModel _parentViewModel;

        string newSubject;
    
        public AddSubjectPopup()
        {

        }

        public AddSubjectPopup(ProfileViewModel parentViewModel)
        {
            CloseWhenBackgroundIsClicked = true;
            InitializeComponent();
            BindingContext = _viewModel = new AddSubjectPopupViewModel();
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

        private async void OnClick_CreateCurrent(object sender, System.EventArgs e)
        {
            newSubject = ((AddSubjectPopupViewModel)BindingContext).AddSubject();
            _parentViewModel.ExecuteAddCurrentSubject(newSubject);
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnClick_CreatePrevious(object sender, System.EventArgs e)
        {
            newSubject = ((AddSubjectPopupViewModel)BindingContext).AddSubject();
            _parentViewModel.ExecuteAddPreviousSubject(newSubject);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}