using System;
using System.Collections.Generic;
using Mobile.ViewModels.Assignments;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace Mobile.Views.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssignUser : Rg.Plugins.Popup.Pages.PopupPage
    {
        private AddAssignUserViewModel _viewModel;

        public AddAssignUser(CheckpointViewModel parentViewModel)
        {
            BindingContext = _viewModel = new AddAssignUserViewModel(parentViewModel);
            CloseWhenBackgroundIsClicked = true;
            InitializeComponent();
        }

        public AddAssignUser(AddAssignUserViewModel bindingContext2)
        {
            BindingContext = bindingContext2;
        }
    }
}
