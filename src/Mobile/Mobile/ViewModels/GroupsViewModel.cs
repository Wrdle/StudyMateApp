using Mobile.Models;
using Mobile.Services.Interfaces;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    class GroupsViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        // Services
        private IGroupStore _groupStore;

        // Data
        private ICollection<GroupListItem> _groups;

        // Public
        public string Error { get; set; }
        public ICollection<GroupListItem> Groups
        {
            get => _groups;

            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        //------------------------------
        //          Constructors
        //------------------------------

        public GroupsViewModel()
        {
            Title = "Groups";
            Error = null;

            _groupStore = DependencyService.Get<IGroupStore>();

            _groups = new List<GroupListItem>();
            LoadGroups().SafeFireAndForget();
        }

        //------------------------------
        //          Methods
        //------------------------------

        public async Task LoadGroups()
        {
            try
            {
                _groups = await _groupStore.MyGroups();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

    }
}
