using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModels.Profile
{
    class AddSkillPopupViewModel : BaseViewModel
    {
        public string NewSkillName { get; set; }

        public AddSkillPopupViewModel()
        {

        }

        public void OnAppearing()
        {
            IsBusy = true;
            NewSkillName = "";
        }

        public string AddSkill()
        {
            if (!string.IsNullOrEmpty(NewSkillName))
            {
                Debug.WriteLine("new skill: " + NewSkillName);
                return NewSkillName;
            }
            else
            {
                return null;
            }
        }
    }
}
