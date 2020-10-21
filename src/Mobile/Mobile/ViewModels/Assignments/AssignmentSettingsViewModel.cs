using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    [QueryProperty(nameof(inputAssignmentID), nameof(AssignmentID))]
    class AssignmentSettingsViewModel : BaseViewModel
    {
        private long assignmentID;
        private Assignment assignment;


        // ASSIGNMENT ID
        public string inputAssignmentID
        {
            set => AssignmentID = Convert.ToInt64(value);
        }

        public long AssignmentID
        {
            get => assignmentID;
            set
            {
                SetProperty(ref assignmentID, Convert.ToInt64(value));
                LoadAssignmentId(value);
            }
        }

        public Assignment Assignment
        {
            get => assignment;
            set
            {
                SetProperty(ref assignment, value);
                AssignmentStore.Update(value);
            }
        }

        public String AssignmentName
        {
            get => Assignment.Title;
            set
            {
                var assignmentEdits = Assignment;
            }
        }


        /// <summary>
        /// Loads a assignment from the datastore given an assignment id
        /// </summary>
        /// <param name="id">Assignment Id</param>
        public async void LoadAssignmentId(long id)
        {
            try
            {
                // Get Assignment
                Assignment = await AssignmentStore.GetById(id);

                Title = "Edit Assignment";

                // Extract and store data
                AssignmentName = assignment.Title;
                //AssignmentNotes = assignment.Notes;
                //ShowCoverPhoto = CheckCoverPhoto();

                var coverColors = await CoverColorStore.GetAll();

                // Load the assignments checkpoints
                //LoadCheckpoints(Assignment.Id);
            }
            catch (Exception)
            {
                //Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
