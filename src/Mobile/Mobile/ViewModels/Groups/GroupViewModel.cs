using Mobile.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Mobile.ViewModels.Groups
{
    [QueryProperty(nameof(inputGroupID), nameof(GroupID))]
    public class GroupViewModel : BaseViewModel
    {
        public GroupViewModel()
        {
            Title = "Group";
            Debug.WriteLine("We made it to the group page");
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
    }
}