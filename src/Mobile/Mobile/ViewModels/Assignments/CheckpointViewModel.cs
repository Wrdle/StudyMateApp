using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Mobile.ViewModels.Assignments;


namespace Mobile.ViewModels.Assignments
{

    [QueryProperty(nameof(CheckpointID), nameof(CheckpointID))]
    class CheckpointViewModel : BaseViewModel
    {
        string checkpointID;

        public string CheckpointID
        {
            get => checkpointID;
            set
            {
                SetProperty(ref checkpointID, value);
                LoadCheckpointID(value);
            }
        }

        /// <summary>
        /// Load the checkpoint ID and due date
        /// </summary>
        /// <param name="id"></param>
        private void LoadCheckpointID(string id)
        {
            // Grab the checkpoint id add to the title 
            Title = "Checkpoint" + " " + checkpointID;
        }

        string dueDay;
        public string DueDay
        {
            get; set;
        }

        private void LoadDueDay()
        {

        }
    }
}