using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.ViewModels.Assignments;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentCheckpointPage : ContentPage
    {
        private CheckpointViewModel bindingContext = new CheckpointViewModel();
        public AssignmentCheckpointPage()
        {
            InitializeComponent();
            // Binding the title dased on the checkpointID
            BindingContext = bindingContext;
        }

        async void AddTaskPopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddTask((CheckpointViewModel)BindingContext));
        }
    }
}