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
        private Stream coverPhotoStream = null;

        public Command PickImageCommand { get; }
        public Command RemoveImageCommand { get; }
        public Command ColorTappedCommand { get; }

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

            PickImageCommand = new Command(OnPickImageCommand);
            RemoveImageCommand = new Command(OnRemoveImageCommand);
            ColorTappedCommand = new Command<CoverColor>(OnColorTappedCommand);
        }


        private async void OnPickImageCommand()
        {
            try
            {
                PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<MediaLibraryPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.MediaLibrary))
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Need media library", "Please grand media library access in order to add a coverphoto.", "OK");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<MediaLibraryPermission>();
                }

                if (status == PermissionStatus.Granted)
                {
                    AssignmentCoverPhoto = null;

                    await CrossMedia.Current.Initialize();

                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile mediaFileCoverPhoto = await CrossMedia.Current.PickPhotoAsync();

                        if (mediaFileCoverPhoto != null)
                        {
                            AssignmentCoverPhoto = ImageSource.FromStream(() =>
                            {
                                return mediaFileCoverPhoto.GetStream();
                            });
                        }
                    }
                }
                else if (status != PermissionStatus.Unknown)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please allow media access to add a coverphoto.", "Allow Permissions");
                }
            }
            catch
            {

                Acr.UserDialogs.UserDialogs.Instance.Alert("Something went wrong adding your cover photo", "Error");
            }
        }

        private async void OnRemoveImageCommand()
        {

        }

        private async void OnColorTappedCommand(CoverColor colorTapped)
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
                Assignment = await AssignmentStore.GetById(id);
                var colorChoices = await CoverColorStore.GetAll();

                Title = "Edit Assignment";

                ColorChoices = Helpers.Helpers.ConvertListToObservableCollection<CoverColor>(colorChoices.ToList());

                OnPropertyChanged(nameof(AssignmentName));
                OnPropertyChanged(nameof(AssignmentDescription));
                OnPropertyChanged(nameof(AssignmentDueDate));
                OnPropertyChanged(nameof(AssignmentCoverPhoto));
                OnPropertyChanged(nameof(AssignmentCoverColor));
                OnPropertyChanged(nameof(ColorChoices));
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