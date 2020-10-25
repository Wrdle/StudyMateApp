using Mobile.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    [QueryProperty(nameof(inputAssignmentID), nameof(AssignmentID))]
    class AssignmentSettingsViewModel : BaseViewModel
    {
        private Assignment placeholder = new Assignment() { Id = 0, Title = "", Description = "", Notes = "", Skills = null, DateDue = DateTime.Now, IsArchived = false, CoverPhoto = null, CoverColor = null };

        private long assignmentID;
        private Assignment assignment;

        public Command PickImageCommand { get; }
        public Command RemoveImageCommand { get; }
        public Command ColorTappedCommand { get; }
        public Command TapArchiveButtonCommand { get; }

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
            }
        }

        public Assignment Assignment
        {
            get
            {
                if (assignment == null)
                    return placeholder;
                return assignment;
            }
            set
            {
                if (value != assignment || value != placeholder)
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
            get
            {
                OnPropertyChanged(nameof(ShowCoverPhoto));
                return Assignment.CoverPhoto;
            }
            set
            {
                var assignmentEdits = Assignment;
                assignmentEdits.CoverPhoto = value;
                Assignment = assignmentEdits;
                OnPropertyChanged(nameof(AssignmentCoverPhoto));
                OnPropertyChanged(nameof(ShowCoverPhoto));
            }
        }

        public bool ShowCoverPhoto
        {
            get => Assignment.CoverPhoto != null;
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

        public bool AssignmentIsArchived
        {
            get => Assignment.IsArchived;
            set
            {
                var assignmentEdits = Assignment;
                assignmentEdits.IsArchived = value;
                Assignment = assignmentEdits;
                OnPropertyChanged(nameof(AssignmentCoverColor));
            }
        }


        public string ArchiveButtonText
        {
            get
            {
                if (AssignmentIsArchived != true)
                    return "Archive Assignment";
                return "Unarchive Assignment";
            }
        }

        public bool ShowRemoveImageButton
        {
            get
            {
                if (AssignmentCoverPhoto == null)
                    return false;
                return true;
            }
        }

        public AssignmentSettingsViewModel()
        {
            //Assignment = placeholder;

            PickImageCommand = new Command(OnPickImageCommand);
            RemoveImageCommand = new Command(OnRemoveImageCommand);
            ColorTappedCommand = new Command<CoverColor>(OnColorTappedCommand);
            TapArchiveButtonCommand = new Command(OnTapArchiveButton);
        }

        public void OnAppearing()
        {
            //Assignment = placeholder;
            LoadAssignmentId(AssignmentID);
        }


        private void OnTapArchiveButton()
        {
            AssignmentIsArchived = AssignmentIsArchived == true ? false : true;
            OnPropertyChanged(nameof(ArchiveButtonText));
        }

        private async void OnPickImageCommand()
        {
            var selectedPhoto = await RunImagePicker();
            if (selectedPhoto != null)
            {
                AssignmentCoverPhoto = selectedPhoto;
                OnPropertyChanged(nameof(ShowRemoveImageButton));
            }
        }

        private void OnRemoveImageCommand()
        {
            AssignmentCoverPhoto = null;
        }

        private void OnColorTappedCommand(CoverColor colorTapped)
        {
            AssignmentCoverColor = colorTapped;
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
                assignment = await AssignmentStore.GetById(id);
                var colorChoices = await CoverColorStore.GetAll();

                Title = "Edit Assignment";

                ColorChoices = Helpers.Helpers.ConvertListToObservableCollection<CoverColor>(colorChoices.ToList());

                OnPropertyChanged(nameof(AssignmentName));
                OnPropertyChanged(nameof(AssignmentDescription));
                OnPropertyChanged(nameof(AssignmentDueDate));
                OnPropertyChanged(nameof(AssignmentCoverPhoto));
                OnPropertyChanged(nameof(AssignmentCoverColor));
                OnPropertyChanged(nameof(ColorChoices));
                OnPropertyChanged(nameof(AssignmentIsArchived));

                OnPropertyChanged(nameof(ArchiveButtonText));
                OnPropertyChanged(nameof(ShowRemoveImageButton));
                OnPropertyChanged(nameof(ShowCoverPhoto));
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