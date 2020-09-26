using Mobile.Services.Interfaces;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        public IAssignmentStore DataStore;

        public BaseViewModel()
        {
            DataStore = DependencyService.Get<IAssignmentStore>();
        }
    }
} 
