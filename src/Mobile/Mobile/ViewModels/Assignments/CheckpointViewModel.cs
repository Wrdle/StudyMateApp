using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Mobile.ViewModels.Assignments;
using System.Diagnostics;
using Mobile.Models;

namespace Mobile.ViewModels.Assignments
{
    // Pass to the view model which checkpoint been clicked
    [QueryProperty(nameof(CheckpointID), nameof(CheckpointID))]
    class CheckpointViewModel : BaseViewModel
    {

        private Checkpoint checkpoint;

        public Checkpoint Checkpoint
        {
            get => checkpoint;
            set => SetProperty(ref checkpoint, value);
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

        /// <summary>
        /// Load the checkpoint ID and due date
        /// </summary>
        /// <param name="id"></param>
        private void LoadCheckpointID(string id)
        {
            //var checkpoint = CheckpointStore.GetCheckpointByID(Convert.ToInt64(id));
            Checkpoint = CheckpointStore.GetById(Convert.ToInt64(id)).Result;


            // Grab the checkpoint id add to the title 
            Title = Checkpoint.Title;
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