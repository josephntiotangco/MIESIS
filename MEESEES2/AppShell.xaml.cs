using MEESEES2.Commons;
using MEESEES2.ViewModels;
using MEESEES2.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace MEESEES2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            PageService pageService = new PageService();

            shellView = new AppShellViewModel(pageService);
            InitializeComponent();
            //Routing.RegisterRoute(nameof(DataPage), typeof(DataPage));
            //Routing.RegisterRoute(nameof(EntryPage), typeof(EntryPage));

        }
        public AppShellViewModel shellView
        {
            get { return BindingContext as AppShellViewModel; }
            set { BindingContext = value; }
        }
        private void OnLogoutClicked(object sender, EventArgs e)
        {
            shellView.LogOutCommand.Execute(null);
        }

        protected override bool OnBackButtonPressed()
        {
            if(Globals.currentUser != null)
            {
                base.OnBackButtonPressed();
                shellView.LogOutCommand.Execute(null);
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }

        }


    }
}
