using Mobile.Data;
using Mobile.Services;
using Mobile.Services.Interfaces;
using MvvmHelpers;
using Xamarin.Forms;

namespace Mobile
{
    public partial class App : Application
    {

        public App()
        {
            // Initialize app
            InitializeComponent();

            // Seed database
            new AppDbSeeder().Seed().SafeFireAndForget();

            // Register services
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IUserStore, UserStore>();
            DependencyService.Register<IGroupStore, GroupStore>();
            DependencyService.Register<IAssignmentStore, AssignmentStore>();
            DependencyService.Register<ICheckpointstore, CheckpointStore>();

            {
                var userStore = DependencyService.Get<IUserStore>();
                userStore.Login(AppDbSeeder.TestUser.Email, "").SafeFireAndForget();
            }

            // Set main page
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
