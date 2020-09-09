using Mobile.Models;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    [QueryProperty(nameof(AssignmentID), nameof(AssignmentID))]
    class AssignmentViewModel : BaseViewModel
    {
        private string assignmentID;
        private Assignment assignment;

        public AssignmentViewModel()
        {
            Title = "Assignment";
            Debug.WriteLine("We made it to the assignment page");
        }

        public Assignment Assignment
        {
            get => assignment;
        }

        public string AssignmentID
        {
            get {
                return assignmentID;
            }
            set
            {
                assignmentID = value;
                LoadAssignmentId(value);
            }
        }

        public async void LoadAssignmentId(string id)
        {
            try
            {
                assignment = await DataStore.GetById(Convert.ToInt64(id));
                Title = assignment.Title;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
