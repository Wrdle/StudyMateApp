using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.ViewModels.Assignments;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentCheckpointPage : ContentPage
    {
        public AssignmentCheckpointPage()
        {
            InitializeComponent();
            BindingContext = new CheckpointViewModel();
        }
    }
}