﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Mobile.ViewModels.Assignments;
using System.Diagnostics;
using Mobile.Models;
using System.Threading.Tasks;

namespace Mobile.ViewModels.Assignments
{

    [QueryProperty(nameof(CheckpointID), nameof(CheckpointID))]
    public class CheckpointViewModel : BaseViewModel
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

        public async void LoadCheckpointID(string id)
        {
            //var checkpoint = CheckpointStore.GetCheckpointByID(Convert.ToInt64(id));
            Checkpoint = await CheckpointStore.GetById(Convert.ToInt64(id));

            // Grab the checkpoint id add to the title 
            Title = Checkpoint.Title;
        }
    }
}
