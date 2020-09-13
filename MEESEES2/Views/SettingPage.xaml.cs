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
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            //ca-app-pub-6838059012127071/4882317165
            //ca-app-pub-3940256099942544/6300978111
            Globals.BannerAdId = "ca-app-pub-6838059012127071/4882317165";
            SQLiteGeneral sqlDb = new SQLiteGeneral(DependencyService.Get<ISQLiteDb>(), "");
            PageService pageService = new PageService();
            settingModel = new SettingViewModel(sqlDb, pageService);
            InitializeComponent();
        }

        public SettingViewModel settingModel
        {
            get { return BindingContext as SettingViewModel; }
            set { BindingContext = value; }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            settingModel.ShowPopupAdsCommand.Execute(null); //2020/09/11
            settingModel.LoadUserSettingsCommand.Execute(null);
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            settingModel.ResetTransactionCommand.Execute(null);
        }
    }
}