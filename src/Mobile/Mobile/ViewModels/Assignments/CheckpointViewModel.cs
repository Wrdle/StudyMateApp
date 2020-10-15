using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

        private void LoadCheckpointID(string id)
        {
            // Your loading code here
        }
    }
}
