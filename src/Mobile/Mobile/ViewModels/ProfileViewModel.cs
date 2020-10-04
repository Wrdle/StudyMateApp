using Mobile.Data.Entites;
using Mobile.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;


namespace Mobile.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Skill> ProfileSkills { get; }

        public Command LoadProfileSkillsCommand { get; }

        public ProfileViewModel()
        {
            Title = "Profile";
            ProfileSkills = new ObservableCollection<Models.Skill>();
            LoadProfileSkillsCommand = new Command(async () => await ExecuteLoadProfileSkillsCommand());
        }

        string name = "Felix";

        public String Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName => $"Name: {Name}";

        string university = "QUT";

        public String University
        {
            get => university;
            set
            {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(University));
                OnPropertyChanged(nameof(DisplayUniversity));
            }
        }

        public string DisplayUniversity => $"University: {University}";

        string skillsTitle = "Skills";

        public string SkillsTitle => $"{skillsTitle}";

        async Task ExecuteLoadProfileSkillsCommand()
        {
            IsBusy = true;

            try
            {
                ProfileSkills.Clear();
                var skills = await SkillDataStore.GetAllSkillsByUserAsync(1);
                foreach (var skill in skills)
                {
                    
                    ProfileSkills.Add(skill);
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
            
        }
    }  
}
