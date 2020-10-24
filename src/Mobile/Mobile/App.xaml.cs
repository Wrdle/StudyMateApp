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
            DependencyService.Register<ImageConverter>();
            DependencyService.Register<ICoverColorStore, CoverColorStore>();
            DependencyService.Register<ISkillStore, SkillStore>();
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
