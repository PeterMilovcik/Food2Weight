using Syncfusion.Licensing;
using Xamarin.Forms;

namespace Food2Weight
{
    public partial class App : Application
    {
        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("##SyncfusionLicense##");
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
