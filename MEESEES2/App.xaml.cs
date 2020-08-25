using MEESEES2.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MEESEES2
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new[] { "Brush_Experimental" });
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzAzNzMzQDMxMzgyZTMyMmUzMGNXSytxRlNmcStTYm9XUGg4dDVNK0pMRzlxbTZYaUlwakszN09WNCtMaUU9");
            InitializeComponent();
            MainPage = new AppShell();
            //MainPage = new LoginPage();
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
