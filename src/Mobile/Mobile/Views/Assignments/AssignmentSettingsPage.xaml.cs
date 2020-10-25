using Mobile.ViewModels.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentSettingsPage : ContentPage
    {
        AssignmentSettingsViewModel _viewModel;
        public AssignmentSettingsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AssignmentSettingsViewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}