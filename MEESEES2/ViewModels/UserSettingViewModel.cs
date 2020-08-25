using MEESEES2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MEESEES2.ViewModels
{
    public class UserSettingViewModel : BaseViewModel
    {
        private string _code;
        private string _value;
        private string _pin;

        public int Id { get; set; }
        public UserSettingViewModel() { }
        public UserSettingViewModel(UserSetting userSetting)
        {
            Id = userSetting.Id;
            Code = userSetting.Code;
            Value = userSetting.Value;
            Pin = userSetting.Pin;
        }

        public string Code
        {
            get { return _code; }
            set
            {
                SetValue(ref _code, value);
                OnPropertyChanged(nameof(Code));
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                SetValue(ref _value, value);
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Pin
        {
            get { return _pin; }
            set
            {
                SetValue(ref _pin, value);
                OnPropertyChanged(nameof(Pin));
            }
        }
    }
}
