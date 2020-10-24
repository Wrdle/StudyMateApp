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
        private ObservableCollection<CheckpointUserListItem> assignedUsers;
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

        public ObservableCollection<CheckpointUserListItem> AssignedUsers
        {
            get => assignedUsers;
            set => SetProperty(ref assignedUsers, value);
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

        // Checkpoint Description/Notes
        private string checkpointNotes;

        public string CheckpointNotes
        {
            get => checkpointNotes;
            set
            {
                // Update Locally
                SetProperty(ref checkpointNotes, value);

                if (value != null)
                {
                    checkpoint.Notes = value;

                    // Update DB 
                    CheckpointStore.UpdateNotes(checkpoint.Id, value).Wait();
                }
            }
        }

        public async void LoadCheckpointID(string id)
        {
            //var checkpoint = CheckpointStore.GetCheckpointByID(Convert.ToInt64(id));
            Checkpoint = await CheckpointStore.GetById(Convert.ToInt64(id));

            // Grab the checkpoint id add to the title 
            Title = Checkpoint.Title;
            CheckpointNotes = Checkpoint.Notes;
        }
    }
}
