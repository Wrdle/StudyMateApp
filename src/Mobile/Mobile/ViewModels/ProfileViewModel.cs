using Mobile.Data.Entites;
using Mobile.Models;
using MvvmHelpers.Commands;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile.Views.Profile;
using static Mobile.Helpers.Helpers;

// Helpers
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Query;
using Acr.UserDialogs;
using Mobile.Data;

namespace Mobile.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ObservableCollection<string> UsersCurrentSkills { get; set; }
        public ObservableCollection<string> UsersCurrentSubjects { get; set; }
        public ObservableCollection<string> UsersPreviousSubjects { get; set; }
        public Command LoadUserProfileCommand { get; }

        public ProfileViewModel()
        {
            Title = "Profile";
            UsersCurrentSkills = new ObservableCollection<string>();
            UsersCurrentSubjects = new ObservableCollection<string>();
            UsersPreviousSubjects = new ObservableCollection<string>();
            LoadUserProfileCommand = new Command(() => ExecuteLoadUserProfile());
        }

        string firstName;
        public string FirstName
        {
            get => firstName;
        }

        string lastName;
        public string LastName
        {
            get => lastName;
        }

        string institution;
        public string Institution
        {
            get => institution;
        }

        string major;
        public string Major
        {
            get => major;
        }

        string email;
        public string Email
        {
            get => email;
        }

        public string DisplayUsername => $"Name: {FirstName}" + " " + $"{LastName}";
        public string DisplayInstitution => $"Institution : {Institution}";
        public string DisplayMajor => $"Major: {Major}";
        public string DisplayEmail => $"Email: {Email}";

        public void OnAppearing()
        {
            IsBusy = true;           
        }

        void ExecuteLoadUserProfile()
        {
            IsBusy = true;
            try
            {
                var userFirstName = LoggedInUser.FirstName;
                var userLastName = LoggedInUser.LastName;
                var userInstitution = LoggedInUser.Institution;
                var userMajor = LoggedInUser.Major;
                var userEmail = LoggedInUser.Email;

                SetProperty(ref firstName, userFirstName);
                SetProperty(ref lastName, userLastName);
                SetProperty(ref institution, userInstitution);
                SetProperty(ref major, userMajor);
                SetProperty(ref email, userEmail);

                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(Institution));
                OnPropertyChanged(nameof(Major));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(DisplayInstitution));
                OnPropertyChanged(nameof(DisplayUsername));
                OnPropertyChanged(nameof(DisplayEmail));
                OnPropertyChanged(nameof(DisplayMajor));

                UsersCurrentSkills.Clear();
                var skills = LoggedInUser.Skills;
                foreach (var skill in skills)
                {
                    UsersCurrentSkills.Add(skill);
                    Debug.WriteLine(skill);
                }
                OnPropertyChanged(nameof(UsersCurrentSkills));

                UsersCurrentSubjects.Clear();
                UsersPreviousSubjects.Clear();
                var cSubjects = LoggedInUser.CurrentSubjects;//ConvertListToObservableCollection(LoggedInUser.CurrentSubjects);
                var pSubjects = LoggedInUser.PreviousSubjects;//ConvertListToObservableCollection(LoggedInUser.PreviousSubjects);
                foreach (var subject in cSubjects)
                {
                    UsersCurrentSubjects.Add(subject);
                    Debug.WriteLine(subject);
                }
                foreach (var subject in pSubjects)
                {
                    UsersPreviousSubjects.Add(subject);
                    Debug.WriteLine(subject);
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

        public void ExecuteAddNewSkill(string newSkill)
        {
            
            LoggedInUser.Skills.Add(newSkill);
            IsBusy = true;
            try
            {
                UsersCurrentSkills.Clear();
                var skills = LoggedInUser.Skills;
                foreach (var skill in skills)
                {
                    UsersCurrentSkills.Add(skill);
                    Debug.WriteLine(skill);
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
    }
}