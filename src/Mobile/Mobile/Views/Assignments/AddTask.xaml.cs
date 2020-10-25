using System;
using System.Collections.Generic;
using Mobile.ViewModels.Assignments;
using Rg.Plugins.Popup.Services;
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

        public async void OnClick_addTask(object sender, System.EventArgs e)
        {
            var newTask = await ((AddTaskViewModel)BindingContext).AddTask(_parentViewModel.Checkpoint.Id);
            var checkpoint = _parentViewModel.Checkpoint;
            checkpoint.ChecklistItems.Add(newTask);
            _parentViewModel.Checkpoint = checkpoint;
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
