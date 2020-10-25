using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.ViewModels.Profile
{
    class AddSubjectPopupViewModel : BaseViewModel
    {
        public string NewSubject { get; set; }

        public AddSubjectPopupViewModel()
        {

        }

        public void OnAppearing()
        {
            IsBusy = true;
            NewSubject = "";
        }

        public string AddSubject()
        {
            if (!string.IsNullOrEmpty(NewSubject))
            {
                return NewSubject;
            }
            else
            {
                return null;
            }
        }
    }
}
