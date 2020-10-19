using System;
using System.Collections.Generic;
using Mobile.ViewModels.Assignments;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTask : Rg.Plugins.Popup.Pages.PopupPage
    {
        private AddTaskViewModel _viewModel;
        private CheckpointViewModel _parentViewModel;

        public AddTask(CheckpointViewModel parentViewModel)
        {
            CloseWhenBackgroundIsClicked = true;
            InitializeComponent();
            BindingContext = _viewModel = new AddTaskViewModel();
            _parentViewModel = parentViewModel;
        }


    }
}
