﻿using Mobile.ViewModels.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentPage : ContentPage
    {
        private AssignmentViewModel _viewModel;

        public AssignmentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AssignmentViewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.LoadAssignmentId(_viewModel.AssignmentID);
            base.OnAppearing();
        }
    }
}