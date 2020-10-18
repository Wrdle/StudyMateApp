using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace Mobile.ViewModels.Groups
{
    public class AddGroupViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        public string GroupName { get; set; }

        // Commands
        public Command MyProperty { get; set; }

        //------------------------------
        //          Constructors
        //------------------------------

        public AddGroupViewModel()
        {
        }

        //------------------------------
        //          Methods
        //------------------------------

        /// <summary>
        /// Runs before page appears, resetting variables and sets IsBusy to true
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
            GroupName = "";
        }

        public async Task CreateGroup()
        {
            if (!string.IsNullOrEmpty(GroupName))
            {
                await GroupStore.Create(GroupName);
            }
        }

    }
}
