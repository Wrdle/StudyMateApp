using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.ViewModels.Profile
{
    class EditProfileViewModel : BaseViewModel
    {
        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string NewInstitution { get; set; }
        public string NewMajor { get; set; }

        public EditProfileViewModel()
        {

        }

        public void OnAppearing()
        {
            IsBusy = true;
            NewFirstName = "";
            NewLastName = "";
            NewInstitution = "";
            NewMajor = "";
        }

        public string UpdateFirstName()
        {
            if (!string.IsNullOrEmpty(NewFirstName))
            {
                return NewFirstName;
            }
            else
            {
                return LoggedInUser.FirstName;
            }
        }

        public string UpdateLastName()
        {
            if (!string.IsNullOrEmpty(NewLastName))
            {
                return NewLastName;
            }
            else
            {
                return LoggedInUser.LastName;
            }
        }

        public string UpdateInstitution()
        {
            if (!string.IsNullOrEmpty(NewInstitution))
            {
                return NewInstitution;
            }
            else
            {
                return LoggedInUser.Institution;
            }
        }

        public string UpdateMajor()
        {
            if (!string.IsNullOrEmpty(NewMajor))
            {
                return NewMajor;
            }
            else
            {
                return LoggedInUser.Major;
            }
        }
    }
}
