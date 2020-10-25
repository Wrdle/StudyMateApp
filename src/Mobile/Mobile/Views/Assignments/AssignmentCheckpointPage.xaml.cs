using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Mobile.ViewModels.Assignments;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentCheckpointPage : ContentPage
    {
        private CheckpointViewModel _viewModel;
        //private AddAssignUserViewModel bindingContext2 = new AddAssignUserViewModel();

        public AssignmentCheckpointPage()
        {
            InitializeComponent();
            // Binding the title dased on the checkpointID
            BindingContext = _viewModel = new CheckpointViewModel();
        }


        async void AddTaskPopup(object sender, EventArgs e)
        {
            //await PopupNavigation.Instance.PushAsync(new AddTask((CheckpointViewModel)BindingContext));
            await PopupNavigation.Instance.PushAsync(new AddTask((CheckpointViewModel)BindingContext));

        }

        // Assign checkpoin to member
        async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddAssignUser(_viewModel));

        }

        //async void AddMemberPopup(object sender, EventArgs e)
        //{
        //    await PopupNavigation.Instance.PushAsync(new AssignUser((CheckpointViewModel)BindingContext));
        //}
    }
}