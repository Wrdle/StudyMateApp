using System;
using System.Threading.Tasks;
using Mobile.Models;

namespace Mobile.ViewModels.Assignments
{
    public class AddTaskViewModel : BaseViewModel
    {
        private string taskName;

        public string TaskName
        {
            get => taskName;
            set => SetProperty(ref taskName, value);
        }

        public AddTaskViewModel()
        {

        }
        public async Task<ChecklistItem> AddTask(long checkpointID)
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                return await CheckpointStore.AddTaskToCheckpoint(checkpointID, TaskName);
            }

            return null;
        }

    }

}
