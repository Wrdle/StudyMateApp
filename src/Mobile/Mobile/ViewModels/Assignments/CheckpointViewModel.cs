using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Mobile.ViewModels.Assignments;


// Issues: Back button nevg to Assignments page instead of Assignment page

namespace Mobile.ViewModels.Assignments
{
    // Pass to the view model which checkpoint been clicked
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

            // This needs to be converted to use the new CheckpointStore
            //var checkpoint = CheckpointDataStore.GetCheckpointByID(Convert.ToInt64(id));
        }

        //string checkpointDueDay;
        //public string DueDay
        //{
        //    get => checkpointDueDay;
        //    set
        //    {
        //        SetProperty(ref checkpointDueDay, value);
        //        LoadCpDueDay();
        //    }
        //}

        //private void LoadCpDueDay()
        //{
        //    //DueDate = checkpointDueDay;
        //}
    }
}