using Mobile.ViewModels;
using Mobile.ViewModels.Profile;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class AddSkillPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private AddSkillPopupViewModel _viewModel;
        private ProfileViewModel _parentViewModel;

        string newSkill;

        public AddSkillPopup()
        {
        }

        public AddSkillPopup(ProfileViewModel parentViewModel)
        {
            CloseWhenBackgroundIsClicked = true;
            InitializeComponent();
            BindingContext = _viewModel = new AddSkillPopupViewModel();
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

        private async void OnClick_Create(object sender, System.EventArgs e)
        {
            newSkill =  ((AddSkillPopupViewModel)BindingContext).AddSkill();
            _parentViewModel.ExecuteAddNewSkill(newSkill);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}