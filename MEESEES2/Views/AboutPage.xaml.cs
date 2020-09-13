using MEESEES2.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MEESEES2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            //ca-app-pub-6838059012127071/5429112077
            //ca-app-pub-3940256099942544/6300978111
            Globals.BannerAdId = "ca-app-pub-6838059012127071/5429112077";
            InitializeComponent();
        }
    }
}