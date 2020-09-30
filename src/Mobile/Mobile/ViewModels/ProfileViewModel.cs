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
        public ProfileViewModel()
        {
            Title = "Profile";

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
    }  
}
