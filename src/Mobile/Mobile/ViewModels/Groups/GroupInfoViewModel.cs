using Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModels.Groups
{
    [QueryProperty(nameof(GroupIdParam), nameof(GroupId))]
    public class GroupInfoViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        // Query Params
        public string GroupIdParam { set => GroupId = Convert.ToInt64(value); }

        // Private
        private Group _placeHolder = new Group { CoverColor = new CoverColor() };
        private long _groupId;
        private Group _group;
        private ObservableCollection<UserListItem> _groupMembers;

        // Accessors
        public long GroupId { get => _groupId; set => SetProperty(ref _groupId, value); }
        public Group Group
        {
            get
            {
                if (_group == null) { return _placeHolder; }
                return _group;
            }
            set
            {
                if (value != _group || value != _placeHolder)
                {
                    SetProperty(ref _group, value);
                    UpdateGroup();
                }
            }
        }
        public bool HasCoverPhoto { get => Group.CoverPhoto != null && Group.CoverPhotoBytes.Length > 0; }
        public ImageSource CoverPhoto
        {
            get => Group.CoverPhoto;
            set
            {
                var group = Group;
                group.CoverPhoto = value;
                Group = group;
                OnPropertyChanged(nameof(HasCoverPhoto));
                OnPropertyChanged(nameof(CoverPhoto));
            }
        }
        public CoverColor CoverColor
        {
            get => Group.CoverColor;
            set
            {
                var group = Group;
                group.CoverColor = value;
                Group = group;
                OnPropertyChanged(nameof(CoverColor));
            }
        }
        public string GroupName
        {
            get => Group.Name;
            set
            {
                var group = Group;
                group.Name = value;
                Group = group;
                OnPropertyChanged(nameof(GroupName));
            }
        }
        public ObservableCollection<UserListItem> GroupMembers { get => _groupMembers; set => SetProperty(ref _groupMembers, value); }
        public ObservableCollection<CoverColor> ColorChoices { get; set; }

        // Commands
        public ICommand LoadGroupCommand { get; }
        public ICommand PickImageCommand { get; }
        public ICommand RemoveImageCommand { get; }
        public ICommand SelectColorCommand { get; }
        public ICommand ArchiveGroupCommand { get; }

        //------------------------------
        //          Constructors
        //------------------------------

        public GroupInfoViewModel()
        {
            LoadGroupCommand = new Command(LoadGroup);
            PickImageCommand = new Command(ExecutePickImageCommand);
            RemoveImageCommand = new Command(ExecuteRemoveImageCommand);
            SelectColorCommand = new Command<CoverColor>(ExecuteSelectColorCommand);
        }

        //------------------------------
        //          Methods
        //------------------------------

        public async void LoadGroup()
        {
            if (ColorChoices == null)
            {
                var colorChoices = await CoverColorStore.GetAll();
                ColorChoices = Helpers.Helpers.ConvertListToObservableCollection(colorChoices.ToList());
            }

            Group = await GroupStore.GetById(GroupId);
            var groupMembers = new ObservableCollection<UserListItem>();
            foreach (var item in Group.Members)
            {
                if (item.Id != UserStore.CurrentUserId)
                {
                    groupMembers.Add(item);
                }
            }
            GroupMembers = groupMembers;
            OnPropertyChanged(nameof(HasCoverPhoto));
            OnPropertyChanged(nameof(CoverPhoto));
            OnPropertyChanged(nameof(CoverColor));
            OnPropertyChanged(nameof(GroupName));
            OnPropertyChanged(nameof(ColorChoices));
            Title = $"{Group.Name} Details";
        }

        public async void UpdateGroup()
        {
            await GroupStore.Update(Group);
        }

        //------------------------------
        //          Command Methods
        //------------------------------

        private async void ExecutePickImageCommand()
        {
            var selectedPhoto = await RunImagePicker();
            if (selectedPhoto != null)
            {
                CoverPhoto = selectedPhoto;
            }
        }

        private void ExecuteRemoveImageCommand()
        {
            CoverPhoto = null;
        }

        private void ExecuteSelectColorCommand(CoverColor selectedColor)
        {
            CoverColor = selectedColor;
        }

    }
}
