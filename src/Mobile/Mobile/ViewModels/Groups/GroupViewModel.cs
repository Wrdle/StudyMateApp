﻿using Mobile.Models;
using Mobile.ViewModels.Assignments;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Mobile.ViewModels.Groups
{
    [QueryProperty(nameof(inputGroupID), nameof(GroupID))]
    public class GroupViewModel : BaseViewModel
    {
        public Command<Assignment> AssignmentTapped { get; }
        public Command AddAssignmentCommand { get; }
        public Command InfoTappedCommand { get; }

        public GroupViewModel()
        {
            Title = "Group";
            Debug.WriteLine("We made it to the group page");
            AssignmentTapped = new Command<Assignment>(OnAssignmentSelected);
            AddAssignmentCommand = new Command(OnAddAssignmentTapped);
            InfoTappedCommand = new Command(OnInfoTappedCommand);
        }

        // =============================
        //       ASSIGNMENT DATA
        // =============================

        private Group group;

        public Group Group
        {
            get => group;
            set => SetProperty(ref group, value);
        }

        // GROUP ID
        public string inputGroupID
        {
            set => GroupID = Convert.ToInt64(value);
        }

        private long groupID;

        public long GroupID
        {
            get => groupID;
            set
            {
                SetProperty(ref groupID, Convert.ToInt64(value));
                LoadGroupId(value);
            }
        }


        // SHOW COVER PHOTO
        private bool showCoverPhoto;

        public bool ShowCoverPhoto
        {
            get => showCoverPhoto;
            set => SetProperty(ref showCoverPhoto, value);
        }

        /// <summary>
        /// Loads a group from the datastore given an group id
        /// </summary>
        /// <param name="id">Group Id</param>
        public async void LoadGroupId(long id)
        {
            try
            {
                // Get Group
                Group = await GroupStore.GetById(id);

                // Extract and store data
                Title = group.Name;
                ShowCoverPhoto = CheckCoverPhoto();

                var coverColors = await CoverColorStore.GetAll();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Check if assignment has a coverphoto
        /// </summary>
        /// <returns>True/False</returns>
        public bool CheckCoverPhoto()
        {
            if (group.CoverPhoto != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Runs when assignment selected, navigating to assignment page of given assignment
        /// </summary>
        /// <param name="assignment">Assignment to navigate to</param>
        async void OnAssignmentSelected(Assignment assignment)
        {
            if (assignment == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"assignments/assignment?{nameof(AssignmentViewModel.AssignmentID)}={assignment.Id}");
        }

        async void OnAddAssignmentTapped()
        {
            await Shell.Current.GoToAsync($"assignments/addAssignment?{nameof(AddAssignmentViewModel.GroupID)}={Group.Id}");
        }

        async void OnInfoTappedCommand()
        {
            await Shell.Current.GoToAsync($"groups/groupInfo?{nameof(GroupInfoViewModel.GroupId)}={Group.Id}");
        }
    }
}