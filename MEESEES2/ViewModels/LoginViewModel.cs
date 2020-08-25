using MEESEES2.Commons;
using MEESEES2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MEESEES2.Views;

namespace MEESEES2.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        IGeneralInterface _generalInterface;
        IPageService _pageService;

        public User User { get; set; }

        //properties

        #region Properties

        private string _pin1;
        public string Pin1
        {
            get { return _pin1; }
            set
            {
                SetValue(ref _pin1, value);
                OnPropertyChanged(nameof(Pin1));
            }
        }
        private string _pin2;
        public string Pin2
        {
            get { return _pin2; }
            set
            {
                SetValue(ref _pin2, value);
                OnPropertyChanged(nameof(Pin2));
            }
        }
        private string _pin3;
        public string Pin3
        {
            get { return _pin3; }
            set
            {
                SetValue(ref _pin3, value);
                OnPropertyChanged(nameof(Pin3));
            }
        }
        private string _pin4;
        public string Pin4
        {
            get { return _pin4; }
            set
            {
                SetValue(ref _pin4, value);
                OnPropertyChanged(nameof(Pin4));
            }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetValue(ref _isRefreshing, value);
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public ICommand LoginCommand { get; private set; }
        public ICommand LoadUserCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        #endregion




        public LoginViewModel(IGeneralInterface generalInterface, IPageService pageService)
        {
            _generalInterface = generalInterface;
            _pageService = pageService;

            //Initialize pin and users
            Globals.currentUser = null;
            Globals.Users = new ObservableCollection<UserViewModel>();
            Pin1 = null;
            Pin2 = null;
            Pin3 = null;
            Pin4 = null;

            LoadUserCommand = new Command(async () => await LoadUsers());
            LoginCommand = new Command(async () => await Login());
            RefreshCommand = new Command(async () => await Refresh());
        }
        private async Task LoadUsers()
        {
            if (Globals.isUsersLoaded) return;
            Globals.Users.Clear();
           var users = await _generalInterface.GetUsers();
            foreach (User user in users)
                Globals.Users.Add(new UserViewModel(user));
        }
        private bool ValidatePin()
        {
            if (string.IsNullOrEmpty(Pin1) || string.IsNullOrEmpty(Pin2) || string.IsNullOrEmpty(Pin3) || string.IsNullOrEmpty(Pin4))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidateCurrentUserCount()
        {
            if(Globals.Users.Count < 5 && Globals.Users.Count >= 0)
            {
                Globals.isUsersLoaded = true;
                return true;
            }
            else
            {
                Globals.isUsersLoaded = false;
                return false;
            }
        }
        private void RefreshValues()
        {
            Pin1 = null;
            Pin2 = null;
            Pin3 = null;
            Pin4 = null;
        }
        public async Task Refresh()
        {
            IsRefreshing = true;
            RefreshValues();
            Globals.isUsersLoaded = false;
            await LoadUsers();
            IsRefreshing = false;
        }
        public async Task Login()
        {
            if (ValidatePin())
            {
                string concatPin = Pin1 + Pin2 + Pin3 + Pin4;
                if (Globals.Users.Count == 0)
                {
                    Globals.currentUser = null;
                }
                else
                {
                    foreach (UserViewModel uView in Globals.Users)
                    {
                        if (uView.Pin == concatPin)
                        {
                            Globals.currentUser = uView;
                            break;
                        }
                    }
                }

                if (Globals.currentUser != null)
                {
                    RefreshValues();
                    //(Application.Current).MainPage = new AppShell();
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
                else
                {
                    if (await _pageService.DisplayAlert("ERROR", $"User not registered!, do you want to register current pin? PIN:{concatPin}", "YES", "NO"))
                    {
                        if (ValidateCurrentUserCount())
                        {
                            User = new User
                            {
                                Name = "New User",
                                Pin = concatPin
                            };

                            if (await _pageService.DisplayAlert("CONFIRMATION", $"Are you sure you want to register PIN:{concatPin}", "YES", "NO"))
                            {
                                await _generalInterface.CreateUser(User);
                                RefreshValues();
                                Globals.Users.Clear();
                                Globals.isUsersLoaded = false;
                                await LoadUsers();
                            }
                            else
                            {
                                RefreshValues();
                                return;
                            }
                        }
                    }
                    else
                    {
                        RefreshValues();
                        return;
                    }
                }

            }
            else
            {
                RefreshValues();
                await _pageService.DisplayAlert("ERROR", "Please type complete PIN", "OK");
                return;
            }
        }
        
    }
}
