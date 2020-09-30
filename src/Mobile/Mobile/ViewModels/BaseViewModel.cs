using Mobile.Models;
using Mobile.Services;
using Xamarin.Forms;
using Mobile.Services.Interfaces;

namespace Mobile.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        public IAssignmentStore<Assignment> AssignmentDataStore;
        public ISkillsStore<Skill> SkillDataStore;
        public ICheckpointStore<Checkpoint> CheckpointDataStore;

        public BaseViewModel()
        {
            AssignmentDataStore = DependencyService.Get<IAssignmentStore<Assignment>>();
            SkillDataStore = DependencyService.Get<ISkillsStore<Skill>>();
            CheckpointDataStore = DependencyService.Get<ICheckpointStore<Checkpoint>>();
        }
    }
} 
