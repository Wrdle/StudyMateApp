using Mobile.Helpers;
using Mobile.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    public class AddAssignmentViewModel : Mobile.ViewModels.BaseViewModel
    {
        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }


        public string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }


        public DateTime MinDate
        {
            get => DateTime.Now;
        }


        public DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }


        ImageSource coverPhoto = null;
        public ImageSource CoverPhoto
        {
            get => coverPhoto;
            set
            {
                SetProperty(ref coverPhoto, value);
                if (CoverPhoto != null)
                {
                    SelectedColour = SMColours.DarkGray;
                    ShowRemoveImageButton = true;
                } else
                {
                    ShowRemoveImageButton = false;
                }
            }
        }


        public Command PickImageCommand { get; }
        /// <summary>
        /// Pick image from phones gallery
        /// </summary>
        private async void OnPickImageTapped()
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
                    CoverPhoto = null;

                    await CrossMedia.Current.Initialize();

                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile mediaFileCoverPhoto = await CrossMedia.Current.PickPhotoAsync();
                        CoverPhoto = ImageSource.FromStream(() =>
                        {
                            var stream = mediaFileCoverPhoto.GetStream();
                            return stream;
                        });
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


        private bool showRemoveImageButton;
        public bool ShowRemoveImageButton
        {
            get => showRemoveImageButton;
            set => SetProperty(ref showRemoveImageButton, value);
        }


        public Command RemoveImageCommand { get; }
        private async void OnRemoveImageTapped()
        {
            CoverPhoto = null;
        }


        public ObservableCollection<SMColour> ColourChoices { get; set; }


        private SMColour selectedColour = SMColours.LightGray; // Default Colour
        public SMColour SelectedColour
        {
            get => selectedColour;
            set => SetProperty(ref selectedColour, value);
        }


        public Command ColourTappedCommand { get; }
        /// <summary>
        /// Set the selected colour
        /// </summary>
        /// <param name="colourTapped"></param>
        private async void OnColourTapped(SMColour colourTapped)
        {
            SelectedColour = colourTapped;
        }




        public Command CancelCommand { get; }
        /// <summary>
        /// Cancel and return to previous page
        /// </summary>
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("../..");
        }


        public Command SaveCommand { get; }
        /// <summary>
        /// Save assignment and go back one screen
        /// </summary>
        private async void OnSave()
        {
            // Create new assignment with variables
            Assignment newAssignment = new Assignment()
            {
                Id = AssignmentDataStore.GenerateNewAssignmentID().Result,
                Title = Text,
                Description = Description,
                DateDue = SelectedDate,
                CoverPhoto = CoverPhoto != null ? CoverPhoto : null,
                CoverColour = SMColours.DarkGreen
            };

            await AssignmentDataStore.AddAssignmentAsync(newAssignment);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("../..");
        }

        /// <summary>
        /// Defult constructor
        /// </summary>
        public AddAssignmentViewModel()
        {
            ColourChoices = Helpers.Helpers.convertListToObservableCollection<SMColour>(SMColours.Colours);

            Title = "Add Assignment";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PickImageCommand = new Command(OnPickImageTapped);
            ColourTappedCommand = new Command<SMColour>(OnColourTapped);
            RemoveImageCommand = new Command(OnRemoveImageTapped);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Check all fields are valid
        /// </summary>
        /// <returns>True/False</returns>
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }
    }
}
