using Mobile.Models;
using Mobile.ViewModels.Groups;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    {
        private GroupListItem _selectedGroup;

        public ObservableCollection<GroupListItem> Groups { get; }
        public Command LoadGroupsCommand { get; }
        public Command<GroupListItem> GroupTapped { get; }

        public GroupsViewModel()
        {
            Title = "Groups";
            Groups = new ObservableCollection<GroupListItem>();
            LoadGroupsCommand = new Command(async () => await ExecuteLoadGroupsCommand());

            GroupTapped = new Command<GroupListItem>(OnGroupSelected);
        }

        /// <summary>
        /// Load groups from datastore
        /// </summary>
        /// <returns>Task type</returns>
        public async Task ExecuteLoadGroupsCommand()
        {
            IsBusy = true;

            try
            {
                Groups.Clear();
                var groups = await GroupStore.MyGroups();
                foreach (var group in groups)
                {
                    Groups.Add(group);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\n===================================================");
                Debug.WriteLine(ex);
                Debug.WriteLine("===================================================\n");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Runs before page appears, resetting variables and sets IsBusy to true
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedGroup = null;
        }

        public GroupListItem SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                SetProperty(ref _selectedGroup, value);
                OnGroupSelected(value);
            }
        }

        /// <summary>
        /// Runs when group selected, navigating to group page of given group
        /// </summary>
        /// <param name="group">Grou to navigate to</param>
        async void OnGroupSelected(GroupListItem group)
        {
            if (group == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"groups/group?{nameof(GroupViewModel.GroupID)}={group.Id}");
        }

    }
}