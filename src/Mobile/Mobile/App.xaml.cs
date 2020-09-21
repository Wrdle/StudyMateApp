using Mobile.Data;
using Mobile.Services;
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
