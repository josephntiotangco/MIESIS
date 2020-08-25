using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MEESEES2.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://jntdevz.blogspot.com/"));
            OpenIconsLinkCommand = new Command(async () => await Browser.OpenAsync("https://icons8.com/"));
            OpenSyncFusionCommand = new Command(async () => await Browser.OpenAsync("https://www.syncfusion.com/"));
        }

        public ICommand OpenIconsLinkCommand { get; private set; }
        public ICommand OpenWebCommand { get; private set; }
        public ICommand OpenSyncFusionCommand { get; private set; }
    }
}
