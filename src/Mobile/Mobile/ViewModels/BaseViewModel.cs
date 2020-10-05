using Mobile.Services.Interfaces;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        public IAssignmentStore DataStore { get; }
        public IUserStore UserStore { get; }
        public IGroupStore GroupStore { get; }
        public IAssignmentStore AssignmentStore { get; }
        public ICheckpointStore CheckpointStore { get; }

        //------------------------------
        //          Constructor
        //------------------------------

        public BaseViewModel()
        {
            DataStore = DependencyService.Get<IAssignmentStore>();
            UserStore = DependencyService.Get<IUserStore>();
            GroupStore = DependencyService.Get<IGroupStore>();
            AssignmentStore = DependencyService.Get<IAssignmentStore>();
            CheckpointStore = DependencyService.Get<ICheckpointStore>();
        }

        //------------------------------
        //          Methods
        //------------------------------

    }
} 
