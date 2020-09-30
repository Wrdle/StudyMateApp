using Mobile.Views;
using Mobile.Views.Assignments;
using Mobile.Views.Groups;
using Xamarin.Forms;

namespace Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        void RegisterRoutes()
        {
            // Add Main Page Routes
            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("profile", typeof(ProfilePage));
            Routing.RegisterRoute("groups", typeof(GroupsPage));
            Routing.RegisterRoute("assignments", typeof(AssignmentsPage));

            // Add Misc. Page Routes
            Routing.RegisterRoute("notifications", typeof(NotificationsPage));

            // Add Assignment Page Routes
            Routing.RegisterRoute("assignments/addAssignment", typeof(AddAssignmentPage));
            Routing.RegisterRoute("assignments/assignment", typeof(AssignmentPage));
            Routing.RegisterRoute("assignments/assignmentAddCheckpoint", typeof(AssignmentAddCheckpointPage));
            Routing.RegisterRoute("assignments/assignmentCheckpoint", typeof(AssignmentCheckpointPage));
            Routing.RegisterRoute("assignments/assignmentSettings", typeof(AssignmentSettingsPage));

            // Add Group Page Routes
            Routing.RegisterRoute("groups/groupInfo", typeof(GroupInfoPage));
            Routing.RegisterRoute("groups/group", typeof(GroupPage));
        }
    }
}
