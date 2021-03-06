﻿using MEESEES2.Commons;
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
    public partial class MainPage : ContentPage
    {
        Initializers init = new Initializers();

        public MainPage()
        {
            //ca-app-pub-6838059012127071/5073888855
            //ca-app-pub-3940256099942544/6300978111
            Globals.BannerAdId = "ca-app-pub-6838059012127071/5073888855";
            SQLiteGeneral sqlDb = new SQLiteGeneral(DependencyService.Get<ISQLiteDb>(), "");
            PageService pageService = new PageService();
            mainModel = new DashboardViewModel(sqlDb, pageService);
            InitializeComponent();
            init.InitializeCategories();
            dpkFrom.SetValue(DatePicker.MinimumDateProperty, DateTime.Today.AddMonths(-12));
            dpkFrom.SetValue(DatePicker.MaximumDateProperty, DateTime.Today.AddMonths(12)); //2020/09/11
            dpkTo.SetValue(DatePicker.MinimumDateProperty, DateTime.Today.AddMonths(-12));
            dpkTo.SetValue(DatePicker.MaximumDateProperty, DateTime.Today.AddMonths(12));
            mainModel.FromDate = DateTime.Today.AddDays(-30);
            mainModel.ToDate = DateTime.Today;
        }

        public DashboardViewModel mainModel
        {
            get { return BindingContext as DashboardViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainModel.LoadDataCommand.Execute(null);
            mainModel.LoadSettingCommand.Execute(null);
        }
    }
}