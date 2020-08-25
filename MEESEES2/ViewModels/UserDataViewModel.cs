using MEESEES2.Commons;
using MEESEES2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEESEES2.ViewModels
{
    public class UserDataViewModel : BaseViewModel
    {
        private string _type;
        private string _category;
        private string _description;
        private decimal _amount;
        private string _pin;
        private DateTime _transDate;
        private DateTime _updateDate;

        public UserDataViewModel() { }

        public UserDataViewModel(UserData data, string mode)
        {
            if(mode == "new")
            {
                Id = data.Id;
                Type = data.Type;
                Category = data.Category;
                Description = data.Description;
                Amount = data.Amount;
                Pin = data.Pin;
                TransDate = data.TransDate;
                UpdateDate = data.UpdateDate;
            }
            else
            {
                Id = data.Id;
                Type = data.Type == "E" ? "Expense" : data.Type == "I" ? "Income" : "Savings";
                Category = data.Type == "E" ? Globals.ExpenseCategories.Single(c => c.Code == data.Category).Description : data.Type == "S" ? Globals.SavingsCategories.Single(c => c.Code == data.Category).Description 
                    : Globals.IncomeCategories.Single(c => c.Code == data.Category).Description;
                Description = data.Description;
                Amount = data.Amount;
                Pin = data.Pin;
                TransDate = data.TransDate;
                UpdateDate = data.UpdateDate;
            }

        }

        public int Id { get; set; }

        public string Type
        {
            get { return _type; }
            set
            {
                SetValue(ref _type, value);
                OnPropertyChanged(nameof(Type));
            }
        }
        public string Category
        {
            get { return _category; }
            set
            {
                SetValue(ref _category, value);
                OnPropertyChanged(nameof(Category));
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                SetValue(ref _description, value);
                OnPropertyChanged(nameof(Description));
            }
        }
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                SetValue(ref _amount, value);
                OnPropertyChanged(nameof(Amount));
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
        public DateTime TransDate
        {
            get { return _transDate; }
            set
            {
                SetValue(ref _transDate, value);
                OnPropertyChanged(nameof(TransDate));
            }
        }
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set
            {
                SetValue(ref _updateDate, value);
                OnPropertyChanged(nameof(UpdateDate));
            }
        }
    }
}
