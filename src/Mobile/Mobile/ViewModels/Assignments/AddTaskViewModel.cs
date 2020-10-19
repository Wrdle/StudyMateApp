using System;
using System.Threading.Tasks;

namespace Mobile.ViewModels.Assignments
{
    public class AddTaskViewModel : BaseViewModel
    {
        public string TaskName { get; set; }

        public AddTaskViewModel()
        {

        }
        public async Task AddTask(long checkpointID)
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                await CheckpointStore.AddTaskToCheckpoint(checkpointID, TaskName);
            }
        }

    }

}
