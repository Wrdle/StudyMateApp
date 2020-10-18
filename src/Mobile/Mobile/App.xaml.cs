using Mobile.Data;
using Mobile.Services;
using Mobile.Services.Interfaces;
using Mobile.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IUserStore, UserStore>();
            DependencyService.Register<IGroupStore, GroupStore>();
            DependencyService.Register<IAssignmentStore, AssignmentStore>();
            DependencyService.Register<ICheckpointStore, CheckpointStore>();

            // Seed database
            new AppDbSeeder().Seed().SafeFireAndForget();

            {
                var userStore = DependencyService.Get<IUserStore>();
                userStore.Login(AppDbSeeder.TestUser.Email, "").SafeFireAndForget();
            }

            // Load the login page on app startup
            MainPage = new NavigationPage(new LoginPage());
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            AppShell.Current.GoToAsync("notifications");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
