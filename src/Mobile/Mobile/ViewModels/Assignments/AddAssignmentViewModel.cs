using Mobile.Models;
using MvvmHelpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels.Assignments
{
    public class AddAssignmentViewModel : Mobile.ViewModels.BaseViewModel
    {
        private string name = "";
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
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
                    ShowRemoveImageButton = true;
                }
                else
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
            var selectedPhoto = await RunImagePicker();
            if (selectedPhoto != null)
                CoverPhoto = selectedPhoto;
        }
           

        private bool showRemoveImageButton;
        public bool ShowRemoveImageButton
        {
            get => showRemoveImageButton;
            set => SetProperty(ref showRemoveImageButton, value);
        }


        public Command RemoveImageCommand { get; }
        private void OnRemoveImageTapped()
        {
            CoverPhoto = null;
        }


        public ObservableCollection<CoverColor> ColorChoices { get; set; }


        private CoverColor selectedColor; // Default Colour
        public CoverColor SelectedColor
        {
            get => selectedColor;
            set => SetProperty(ref selectedColor, value);
        }


        public Command ColorTappedCommand { get; }
        /// <summary>
        /// Set the selected colour
        /// </summary>
        /// <param name="colourTapped"></param>
        private void OnColorTapped(CoverColor colourTapped)
        {
            SelectedColor = colourTapped;
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
                Id = AssignmentStore.GenerateNewAssignmentID().Result,
                Title = Name,
                Description = Description,
                DateDue = SelectedDate,
                CoverPhoto = CoverPhoto != null ? CoverPhoto : null,
                CoverColor = selectedColor
            };

            await AssignmentStore.Create(newAssignment);

            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("");

            await Shell.Current.Navigation.PopAsync();
        }


        /// <summary>
        /// Defult constructor
        /// </summary>
        public AddAssignmentViewModel()
        {
            LoadColors().SafeFireAndForget();
            Title = "Add Assignment";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PickImageCommand = new Command(OnPickImageTapped);
            ColorTappedCommand = new Command<CoverColor>(OnColorTapped);
            RemoveImageCommand = new Command(OnRemoveImageTapped);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public async Task LoadColors()
        {
            var coverColors = await CoverColorStore.GetAll();
            SelectedColor = coverColors.SingleOrDefault(cc => cc.Id == 1);
            ColorChoices = new ObservableCollection<CoverColor>();
            foreach (var cc in coverColors)
            {
                ColorChoices.Add(cc);
            }
        }

        /// <summary>
        /// Check all fields are valid
        /// </summary>
        /// <returns>True/False</returns>
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(description);
        }
    }
}
