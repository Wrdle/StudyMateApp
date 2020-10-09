using Mobile.Data.Entites;
using Mobile.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Skill> ProfileSkills { get; }

        public ObservableCollection<Models.Subject> CurrentSubjects { get; }
        public ObservableCollection<Models.Subject> PastSubjects { get; }

        public Command LoadProfileSkillsCommand { get; }

        public Command LoadSubjectsCommand { get; }

      

        public ProfileViewModel()
        {
            Title = "Profile";

            ProfileSkills = new ObservableCollection<Models.Skill>();
            CurrentSubjects = new ObservableCollection<Models.Subject>();
            PastSubjects = new ObservableCollection<Models.Subject>();

            LoadProfileSkillsCommand = new Command(async () => await ExecuteLoadProfileSkillsCommand());
            LoadSubjectsCommand = new Command(async () => await ExecuteLoadSubjectsCommand());
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

        async Task ExecuteLoadSubjectsCommand()
        {
            IsBusy = true;

            try
            {
                CurrentSubjects.Clear();
                var subjects = await SubjectDataStore.GetAllSubjectsByUserAsync(1);
                foreach (var subject in subjects)
                {
                    if (subject.Current)
                    {
                        CurrentSubjects.Add(subject);
                    }
                    else
                    {
                        PastSubjects.Add(subject);
                    }

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
