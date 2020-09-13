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
    public partial class DataPage : ContentPage
    {
        public DataPage()
        {
            //ca-app-pub-6838059012127071/6137179779
            //ca-app-pub-3940256099942544/6300978111
            Globals.BannerAdId = "ca-app-pub-6838059012127071/6137179779";
            SQLiteGeneral sqlDb = new SQLiteGeneral(DependencyService.Get<ISQLiteDb>(), "");
            PageService pageService = new PageService();
            dataList = new DataListViewModel(sqlDb, pageService);
            InitializeComponent();

            dpkFrom.SetValue(DatePicker.MinimumDateProperty, DateTime.Today.AddMonths(-12));
            dpkFrom.SetValue(DatePicker.MaximumDateProperty, DateTime.Today.AddMonths(12)); //2020/09/11
            dpkTo.SetValue(DatePicker.MinimumDateProperty, DateTime.Today.AddMonths(-12));
            dpkTo.SetValue(DatePicker.MaximumDateProperty, DateTime.Today.AddMonths(12));
            dataList.FromDate = DateTime.Today.AddDays(-30);
            dataList.ToDate = DateTime.Today;
        }

        public DataListViewModel dataList
        {
            get { return BindingContext as DataListViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dataList.ResetList();
        }
        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            dataList.InitializeCategories();
        }

        private void OnDataSelected(object sender, SelectedItemChangedEventArgs e)
        {
            dataList.SelectDataCommand.Execute(e.SelectedItem);
        }
    }
}