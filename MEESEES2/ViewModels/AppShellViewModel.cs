using MEESEES2.Commons;
using MEESEES2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MEESEES2.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        IPageService _pageService;
        public ICommand LogOutCommand { get; private set; }

        public AppShellViewModel(IPageService pageService)
        {
            _pageService = pageService;
            LogOutCommand = new Command(async () => await LogOut());
        }

        private async Task LogOut()
        {
            if(await _pageService.DisplayAlert("CONFIRMATION","Are you sure you want to logout?", "YES", "NO"))
            {
                Globals.isUsersLoaded = false;
                Globals.Users = new System.Collections.ObjectModel.ObservableCollection<UserViewModel>();
                Globals.currentUser = new UserViewModel();

                //(Application.Current).MainPage = new LoginPage();
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                return;
            }
        }
    }
}
