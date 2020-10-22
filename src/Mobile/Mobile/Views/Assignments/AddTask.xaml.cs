using Mobile.ViewModels.Assignments;
using Rg.Plugins.Popup.Services;
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
            var checklist = _parentViewModel.Checklist;
            checklist.Add(newTask);
            _parentViewModel.Checklist = checklist;
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
