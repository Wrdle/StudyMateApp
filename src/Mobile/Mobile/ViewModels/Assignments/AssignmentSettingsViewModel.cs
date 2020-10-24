using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    [QueryProperty(nameof(inputAssignmentID), nameof(AssignmentID))]
    class AssignmentSettingsViewModel : BaseViewModel
    {
        private Assignment placeholder = new Assignment() { Id = 0, Title = "", Description="", Notes="", Skills = null, DateDue=DateTime.Now, IsArchived=false, CoverPhoto=null, CoverColor=null};

        private long assignmentID;
        private Assignment assignment;

        public Command PickImageCommand;
        public Command RemoveImageCommand;

        public ObservableCollection<CoverColor> ColorChoices { get; set; }

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
                assignmentEdits.Title = value;
                Assignment = assignmentEdits;
                OnPropertyChanged(nameof(AssignmentName));
            }
        }

        public String AssignmentDescription
        {
            get => Assignment.Description;
            set
            {
                var assignmentEdits = Assignment;
                assignmentEdits.Description = value;
                Assignment = assignmentEdits;
                OnPropertyChanged(nameof(AssignmentDescription));
            }
        }

        public DateTime AssignmentDueDate 
        {
            get => Assignment.DateDue;
            set
            {
                var assignmentEdits = Assignment;
                assignmentEdits.DateDue = value;
                Assignment = assignmentEdits;
                OnPropertyChanged(nameof(AssignmentDueDate));
            }
        }

        public ImageSource AssignmentCoverPhoto
        {
            get => Assignment.CoverPhoto;
            set
            {
                var assignmentEdits = Assignment;
                assignmentEdits.CoverPhoto = value;
                Assignment = assignmentEdits;
                OnPropertyChanged(nameof(AssignmentCoverPhoto));
            }
        }

        public CoverColor AssignmentCoverColor
        {
            get => Assignment.CoverColor;
            set
            {
                var assignmentEdits = Assignment;
                assignmentEdits.CoverColor = value;
                Assignment = assignmentEdits;
                OnPropertyChanged(nameof(AssignmentCoverColor));
            }
        }

        public AssignmentSettingsViewModel()
        {
            Assignment = placeholder;
            System.Diagnostics.Debug.WriteLine("Testing");
        }


        private async void OnPickImageCommand()
        {

        }

        private async void OnRemoveImageCommand()
        {

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
                var colorChoices = await CoverColorStore.GetAll();

                PickImageCommand = new Command(OnPickImageCommand);
                RemoveImageCommand = new Command(OnRemoveImageCommand);

                Title = "Edit Assignment";

                ColorChoices = Helpers.Helpers.ConvertListToObservableCollection<CoverColor>(colorChoices.ToList());

                OnPropertyChanged(nameof(AssignmentName));
                OnPropertyChanged(nameof(AssignmentDescription));
                OnPropertyChanged(nameof(AssignmentDueDate));
                OnPropertyChanged(nameof(AssignmentCoverPhoto));
                OnPropertyChanged(nameof(AssignmentCoverColor));
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
