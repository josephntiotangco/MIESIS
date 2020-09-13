using MEESEES2.Models;
using MEESEES2.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MEESEES2.Services;

namespace MEESEES2.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        IGeneralInterface _generalInterface;
        IPageService _pageInterface;

        public UserSetting UserSetting { get; set; }

        #region Properties
        private string _amount;
        public string Amount
        {
            get { return _amount; }
            set
            {
                SetValue(ref _amount, value);
                OnPropertyChanged(nameof(Amount));
            }
        }
        private bool _isNotify;
        public bool IsNotify
        {
            get { return _isNotify; }
            set
            {
                SetValue(ref _isNotify, value);
                OnPropertyChanged(nameof(IsNotify));
            }
        }
        private ObservableCollection<UserSettingViewModel> _userSettings;
        public ObservableCollection<UserSettingViewModel> UserSettings
        {
            get { return _userSettings; }
            set
            {
                SetValue(ref _userSettings, value);
                OnPropertyChanged(nameof(UserSettings));
            }
        }

        public ICommand LoadUserSettingsCommand { get; private set; }
        public ICommand SaveSettingsCommand { get; private set; }
        public ICommand ResetTransactionCommand { get; private set; }
        public ICommand ShowPopupAdsCommand { get; private set; } //2020/09/11
        #endregion


        public SettingViewModel(IGeneralInterface generalInterface, IPageService pageService)
        {
            _generalInterface = generalInterface;
            _pageInterface = pageService;

            LoadUserSettingsCommand = new Command(async () => await LoadSettings());
            SaveSettingsCommand = new Command(async () => await SaveSetting());
            ResetTransactionCommand = new Command(async () => await Reset());
            ShowPopupAdsCommand = new Command(async () => await ShowPopupAds()); //2020/09/11
        }
        private async Task ShowPopupAds() //2020/09/11
        {
            DateTime timeNow = DateTime.Now;
            int timeMinute = timeNow.Minute;

            if (timeMinute % 3 == 0)
            {
                if (Globals.PopAdCount == 0)
                {
                    Globals.isAdClosed = false;
                }
            }

            if (Globals.isAdClosed)
            {
                Globals.PopAdCount = 0;
            }
            else
            {
                if (Globals.PopAdCount == 1)
                {                 
                    return;
                }
                else
                {
                    await DependencyService.Get<IInterstitialAds>().Display(Globals.PopAdId); //Globals.PopAd ca-app-pub-3940256099942544/1033173712

                }

            }
        }
        private async Task Reset()
        {
            if (await _pageInterface.DisplayAlert("WARNING", "Are you sure you want to delete your transactions?", "YES", "NO"))
            {
                await _generalInterface.ResetUserData(Globals.currentUser.Pin);
                if (Globals.isSuccess)
                {
                    await _pageInterface.DisplayAlert("SUCCESS", Globals.lastError, "OK");
                    return;
                }
                else
                {
                    await _pageInterface.DisplayAlert("ERROR", Globals.lastError, "OK");
                    return;
                }
            }
            else
            {
                return;
            }
        }
        private bool ValidateSavings()
        {
            if (UserSettings.Count > 0)
            {
                bool isSavings = false;
                foreach (UserSettingViewModel setting in UserSettings)
                {
                    if (setting.Code == "SAVAMT")
                    {
                        isSavings = true;
                    }
                }
                if (isSavings)
                {
                    return false;
                }
                else { return true; }
            }
            else
            {
                return true;
            }
        }
        private bool ValidateNotif()
        {
            if (UserSettings.Count > 0)
            {
                bool isNotif = false;
                foreach (UserSettingViewModel setting in UserSettings)
                {
                    if (setting.Code == "NOTIF")
                    {
                        isNotif = true;
                    }
                }
                if (isNotif)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        private async Task SaveSetting()
        {
            bool isUpdated = false;
            bool isCreated = false;

            if (!string.IsNullOrEmpty(Amount))
            {
                if (ValidateSavings())
                {
                    UserSetting = new UserSetting
                    {
                        Code = "SAVAMT",
                        Value = Amount,
                        Pin = Globals.currentUser.Pin
                    };
                    await _generalInterface.CreateSetting(UserSetting);
                    isCreated = true;
                }
                else
                {
                    var savamt = UserSettings.Single(c => c.Code == "SAVAMT");

                    if (savamt.Value != Amount)
                    {
                        UserSetting = new UserSetting
                        {
                            Id = savamt.Id,
                            Value = Amount,
                            Code = savamt.Code,
                            Pin = savamt.Pin
                        };

                        await _generalInterface.UpdateUserSetting(UserSetting);
                        isUpdated = true;
                    }
                    else
                    {
                        isUpdated = false;
                        return;
                    }
                }
            }
            else
            {
                isCreated = false;
                return;
            }

            if (ValidateNotif())
            {
                UserSetting = new UserSetting
                {
                    Code = "NOTIF",
                    Value = IsNotify ? "TRUE" : "FALSE",
                    Pin = Globals.currentUser.Pin
                };
                await _generalInterface.CreateSetting(UserSetting);
                isCreated = true;
            }
            else
            {
                var notifset = UserSettings.Single(c => c.Code == "NOTIF");
                string temp = IsNotify ? "TRUE" : "FALSE";
                if (notifset.Value != temp)
                {
                    UserSetting = new UserSetting
                    {
                        Id = notifset.Id,
                        Value = temp,
                        Code = notifset.Code,
                        Pin = notifset.Pin
                    };

                    await _generalInterface.UpdateUserSetting(UserSetting);
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                    return;
                }
            }

            if (isCreated || isUpdated)
            {
                await LoadSettings();
                await _pageInterface.DisplayAlert("SUCCESS", "Settings updated / created.", "OK");
                //ca-app-pub-3940256099942544/1033173712
                //if (Globals.isShowAds)
                //{
                //    await DependencyService.Get<IInterstitialAds>().Display(Globals.PopAdId); //Globals.PopAd ca-app-pub-3940256099942544/1033173712 //2020/09/11
                //}
                return;
            }
        }
        private async Task LoadSettings()
        {
            var settings = await _generalInterface.GetUserSettingsByPin(Globals.currentUser.Pin);
            if (settings != null)
            {
                UserSettings = new ObservableCollection<UserSettingViewModel>();
                UserSettings.Clear();
                foreach (UserSetting setting in settings)
                {
                    UserSettings.Add(new UserSettingViewModel(setting));
                }
                if (UserSettings.Count > 0)
                {
                    foreach (UserSettingViewModel setting in UserSettings)
                    {
                        if (setting.Code == "SAVAMT")
                        {
                            Globals.targetSavings = Convert.ToDecimal(setting.Value);
                            Amount = setting.Value;
                        }
                        if (setting.Code == "NOTIF")
                        {
                            Globals.isNotify = setting.Value == "TRUE" ? true : false;
                            IsNotify = setting.Value == "TRUE" ? true : false;
                        }
                    }
                }
            }
            else
            {
                await _pageInterface.DisplayAlert("INFORMATION", "You have not initialized settings.", "OK");
                return;
            }
        }
    }
}
