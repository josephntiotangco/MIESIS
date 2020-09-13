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
    public partial class LoginPage : ContentPage
    {
        SQLiteGeneral sqlDb = new SQLiteGeneral(DependencyService.Get<ISQLiteDb>(), "");
        PageService pageService = new PageService();
        public LoginPage()
        {
            loginModel = new LoginViewModel(sqlDb, pageService);
            InitializeComponent();
            txtPin1.Focus();
        }

        public LoginViewModel loginModel
        {
            get { return BindingContext as LoginViewModel; }
            set { BindingContext = value; }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            loginModel.LoadUserCommand.Execute(null);
        }
        private void OnPinTextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPin1.IsFocused)
            {
                if (!string.IsNullOrEmpty(loginModel.Pin1))
                {
                    txtPin1.Unfocus();
                    txtPin2.Focus();
                }
            }
            else if (txtPin2.IsFocused)
            {
                if (!string.IsNullOrEmpty(loginModel.Pin2))
                {
                    txtPin2.Unfocus();
                    txtPin3.Focus();
                }
            }
            else if (txtPin3.IsFocused)
            {
                if (!string.IsNullOrEmpty(loginModel.Pin3))
                {
                    txtPin3.Unfocus();
                    txtPin4.Focus();
                }
            }
            else if (txtPin4.IsFocused)
            {
                if (!string.IsNullOrEmpty(loginModel.Pin4))
                {
                    txtPin4.Unfocus();
                }
                loginModel.LoginCommand.Execute(null);
            }
        }

        private void OnPinTextFocused(object sender, FocusEventArgs e)
        {
            if (txtPin1.IsFocused)
            {
                if (!string.IsNullOrEmpty(loginModel.Pin1))
                {
                    loginModel.Pin1 = null;
                }
            }
            else if (txtPin2.IsFocused)
            {
                if (!string.IsNullOrEmpty(loginModel.Pin2))
                {
                    loginModel.Pin2 = null;
                }
            }
            else if (txtPin3.IsFocused)
            {
                if (string.IsNullOrEmpty(loginModel.Pin3))
                {
                    loginModel.Pin3 = null;
                }
            }
            else if (txtPin4.IsFocused)
            {
                if (string.IsNullOrEmpty(loginModel.Pin4))
                {
                    loginModel.Pin4 = null;
                }
            }
        }

        private void OnPinCompleted(object sender, EventArgs e)
        {
            this.Unfocus();
            loginModel.LoginCommand.Execute(null);
        }
    }
}