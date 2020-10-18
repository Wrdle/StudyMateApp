using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class AddSkillPopup
    {
        Mobile.ViewModels.ProfileViewModel _viewModel;
        public AddSkillPopup()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ViewModels.ProfileViewModel();
        }

        //protected override void OnAppearing()
        //{
        //    _viewModel.LoadProfileSkillsCommand.Execute(null);   
        //}

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SkillListSearch.ItemsSource = UpdateSearch(e.NewTextValue);
            
        }

        private IEnumerable<Models.Skill> UpdateSearch(string searchText = null)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return _viewModel.ProfileSkills;
            }
            else
            {
                return _viewModel.ProfileSkills.Where(p => p.Name.ToLower().StartsWith(searchText));
            }
        }

        void OnTap (Object sender, ItemTappedEventArgs e)
        {
            Models.Skill tempTappedSkill;

            tempTappedSkill = (Models.Skill)e.Item;

            _viewModel.ProfileSkills.Add(tempTappedSkill);
            
        }


    }
}