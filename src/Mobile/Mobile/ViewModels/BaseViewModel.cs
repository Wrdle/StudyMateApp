using Mobile.Services.Interfaces;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        public ICoverColorStore CoverColorStore { get; set; }
        public IAssignmentStore DataStore { get; }
        public IUserStore UserStore { get; }
        public IGroupStore GroupStore { get; }
        public IAssignmentStore AssignmentStore { get; }
        public ICheckpointStore CheckpointStore { get; }
        public ISkillStore SkillStore { get; }

        public Models.User LoggedInUser { get; private set; }
        //------------------------------
        //          Constructor
        //------------------------------

        public BaseViewModel()
        {
            CoverColorStore = DependencyService.Get<ICoverColorStore>();
            DataStore = DependencyService.Get<IAssignmentStore>();
            UserStore = DependencyService.Get<IUserStore>();
            GroupStore = DependencyService.Get<IGroupStore>();
            AssignmentStore = DependencyService.Get<IAssignmentStore>();
            CheckpointStore = DependencyService.Get<ICheckpointStore>();
            SkillStore = DependencyService.Get<ISkillStore>();

            // Temp
            UserStore.Login("test-user@studymate.com", "fake-password");
            SetLoggedInUser(UserStore.GetProfile().Result);
        }

        //------------------------------
        //          Methods
        //------------------------------

        public void SetLoggedInUser(Models.User user)
        {
            LoggedInUser = user;
        }
    }
}