
using Mobile.ViewModels.Groups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Groups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        private readonly GroupViewModel _viewModel;

        public GroupPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new GroupViewModel();
        }
    }
}
