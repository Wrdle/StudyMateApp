using Mobile.Models;
using Mobile.ViewModels.Assignments;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class AssignmentsViewModel : BaseViewModel
    {
        private Assignment _selectedItem;

        public ObservableCollection<Assignment> Assignments { get; }
        public Command LoadAssignmentsCommand { get; }
        //public Command AddItemCommand { get; }
        public Command<Assignment> ItemTapped { get; }

        public AssignmentsViewModel()
        {
            Title = "Assignments";
            Assignments = new ObservableCollection<Assignment>();
            LoadAssignmentsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Assignment>(OnItemSelected);

            //AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Assignments.Clear();
                var assignments = await AssignmentStore.GetByUserIdAsync(1, true);
                foreach (var assignment in assignments)
                {
                    Assignments.Add(assignment);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Assignment SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        /*private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }*/

        async void OnItemSelected(Assignment assignment)
        {
            if (assignment == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"//assignments/assignment?{nameof(AssignmentViewModel.AssignmentID)}={assignment.Id}");
        }
    }
}