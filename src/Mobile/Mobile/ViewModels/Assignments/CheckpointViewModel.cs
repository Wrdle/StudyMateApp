using Mobile.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{

    [QueryProperty(nameof(CheckpointID), nameof(CheckpointID))]
    public class CheckpointViewModel : BaseViewModel
    {

        private Checkpoint checkpoint;
        private ObservableCollection<ChecklistItem> checklist;

        public Checkpoint Checkpoint
        {
            get => checkpoint;
            set
            {
                SetProperty(ref checkpoint, value);
                var newChecklist = new ObservableCollection<ChecklistItem>();
                foreach (var item in value.ChecklistItems)
                {
                    newChecklist.Add(item);
                }
                Checklist = newChecklist;
            }
        }

        // Constructing for checklist
        public ObservableCollection<ChecklistItem> Checklist
        {
            get => checklist;
            set => SetProperty(ref checklist, value);
        }

        private string checkpointID;

        public string CheckpointID
        {
            get => checkpointID;
            set
            {
                SetProperty(ref checkpointID, value);
                LoadCheckpointID(value);
            }
        }

        public async void LoadCheckpointID(string id)
        {
            //var checkpoint = CheckpointStore.GetCheckpointByID(Convert.ToInt64(id));
            Checkpoint = await CheckpointStore.GetById(Convert.ToInt64(id));

            // Grab the checkpoint id add to the title 
            Title = Checkpoint.Title;
        }
    }
}
