using Mobile.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        public ICoverColorStore CoverColorStore { get; set; }
        public IAssignmentStore DataStore { get; }
        public IUserStore UserStore { get; }
        public IGroupStore GroupStore { get; }
        public IAssignmentStore AssignmentStore { get; }
        public ICheckpointStore CheckpointStore { get; }
        public ISkillStore SkillStore { get; }

        public Models.User LoggedInUser { get; private set; }
        //------------------------------
        //          Constructor
        //------------------------------

        public BaseViewModel()
        {
            CoverColorStore = DependencyService.Get<ICoverColorStore>();
            DataStore = DependencyService.Get<IAssignmentStore>();
            UserStore = DependencyService.Get<IUserStore>();
            GroupStore = DependencyService.Get<IGroupStore>();
            AssignmentStore = DependencyService.Get<IAssignmentStore>();
            CheckpointStore = DependencyService.Get<ICheckpointStore>();
            SkillStore = DependencyService.Get<ISkillStore>();

            // Temp
            UserStore.Login("test-user@studymate.com", "fake-password");
            SetLoggedInUser(UserStore.GetProfile().Result);
        }

        //------------------------------
        //          Methods
        //------------------------------

        public void SetLoggedInUser(Models.User user)
        {
            LoggedInUser = user;
        }

        public async Task<ImageSource> RunImagePicker()
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
                    ImageSource CoverPhoto = null;

                    await CrossMedia.Current.Initialize();

                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile mediaFileCoverPhoto = await CrossMedia.Current.PickPhotoAsync();

                        if (mediaFileCoverPhoto != null)
                        {
                            return ImageSource.FromStream(() =>
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
            return null;
        }
    }
}