using MEESEES2.Commons;
using MEESEES2.ViewModels;
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
    public partial class EntryPage : ContentPage
    {
        public EntryPage()
        {
            //ca-app-pub-6838059012127071/4824098100
            //ca-app-pub-3940256099942544/6300978111
            Globals.BannerAdId = "ca-app-pub-6838059012127071/4824098100";
            SQLiteGeneral sqlDb = new SQLiteGeneral(DependencyService.Get<ISQLiteDb>(), "");
            PageService pageService = new PageService();
            dataModel = new DataInputViewModel(sqlDb, pageService);
            InitializeComponent();

            dpkTransDate.SetValue(DatePicker.MinimumDateProperty, DateTime.Today.AddMonths(-12));
            dpkTransDate.SetValue(DatePicker.MaximumDateProperty, DateTime.Today.AddMonths(12));
            dataModel.SelectedDate = DateTime.Today;
        }

        public DataInputViewModel dataModel
        {
            get { return BindingContext as DataInputViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dataModel.ShowPopupAdsCommand.Execute(null); //2020/09/11
            dataModel.UpdateUserDate();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }
        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            dataModel.InitializeTitle();
        }

    }
}