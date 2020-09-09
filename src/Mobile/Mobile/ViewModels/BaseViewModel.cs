using Mobile.Models;
using Mobile.Services;
using Xamarin.Forms;
using Mobile.Services.Interfaces;

namespace Mobile.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        public IAssignmentStore<Assignment> DataStore;

        public BaseViewModel()
        {
            DataStore = DependencyService.Get<IAssignmentStore<Assignment>>();
        }
    }
} 
