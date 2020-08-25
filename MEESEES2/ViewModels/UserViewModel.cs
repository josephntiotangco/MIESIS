using MEESEES2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MEESEES2.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private string _name;
        private string _pin;

        public int Id { get; set; }
        public UserViewModel() { }
        public UserViewModel(User user)
        {
            Id = user.Id;
            Pin = user.Pin;
            Name = user.Name;
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
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }

    }
}
