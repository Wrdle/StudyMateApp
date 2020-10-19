﻿using Mobile.Views.Profile;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        Mobile.ViewModels.ProfileViewModel _viewModel;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ViewModels.ProfileViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            _viewModel.LoadProfileSkillsCommand.Execute(null);
            _viewModel.LoadSubjectsCommand.Execute(null);
        }

        protected void AddSkillPopupCommand(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AddSkillPopup());//PopupNavigation.PushAsync(new AddSkillPopup());
        }
    }
}