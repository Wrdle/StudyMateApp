using System;
using System.Collections.Generic;
using Mobile.ViewModels;
using Mobile.Views;
using Mobile.Views.Assignments;
using Xamarin.Forms;

namespace Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Add Assignment Page Routes
            Routing.RegisterRoute("assignments/addAssignment", typeof(AddAssignmentPage));
            Routing.RegisterRoute("assignments/assignmentAddCheckpoint", typeof(AssignmentAddCheckpointPage));
            Routing.RegisterRoute("assignments/assignmentCheckpoint", typeof(AssignmentCheckpointPage));
            Routing.RegisterRoute("assignments/assignment", typeof(AssignmentPage));
            Routing.RegisterRoute("assignments/assignmentSettings", typeof(AssignmentSettingsPage));
        }

    }
}
