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


            // Set main page
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
