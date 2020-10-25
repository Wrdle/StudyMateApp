
using Mobile.ViewModels.Groups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Groups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupInfoPage : ContentPage
    {
        //------------------------------
        //          Fields
        //------------------------------

        private GroupInfoViewModel _viewModel;

        //------------------------------
        //          Constructors
        //------------------------------

        public GroupInfoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new GroupInfoViewModel();
        }

        //------------------------------
        //          Methods
        //------------------------------

        protected override void OnAppearing()
        {
            _viewModel.LoadGroup();
            base.OnAppearing();
        }

    }
}