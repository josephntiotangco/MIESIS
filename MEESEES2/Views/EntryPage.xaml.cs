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
    public partial class EntryPage : ContentPage
    {
        public EntryPage()
        {
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