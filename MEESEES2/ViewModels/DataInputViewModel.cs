using MEESEES2.Models;
using MEESEES2.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics.Contracts;

namespace MEESEES2.ViewModels
{
    public class DataInputViewModel : BaseViewModel
    {
        IGeneralInterface _generalInterface;
        IPageService _pageService;
        public UserData UserData { get; set; }

        #region Properties
        private int toggleCount = 0;

        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set
            {
                SetValue(ref _remarks, value);
                OnPropertyChanged(nameof(Remarks));
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                SetValue(ref _title, value);
                OnPropertyChanged(nameof(Title));
            }
        }
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

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetValue(ref _selectedDate, value);
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private bool _isIncomeToggled;
        public bool IsIncomeToggled
        {
            get { return _isIncomeToggled; }
            set
            {
                SetValue(ref _isIncomeToggled, value);
                OnPropertyChanged(nameof(IsIncomeToggled));
            }
        }

        private bool _isExpenseToggled;
        public bool IsExpenseToggled
        {
            get { return _isExpenseToggled; }
            set
            {
                SetValue(ref _isExpenseToggled, value);
                OnPropertyChanged(nameof(IsExpenseToggled));
            }
        }

        private bool _isSavingsToggled;
        public bool IsSavingsToggled
        {
            get { return _isSavingsToggled; }
            set
            {
                SetValue(ref _isSavingsToggled, value);
                OnPropertyChanged(nameof(IsSavingsToggled));
            }
        }
        private string _tempType;
        public string TempType
        {
            get { return _tempType; }
            set
            {
                SetValue(ref _tempType, value);
                OnPropertyChanged(nameof(TempType));
            }
        }
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                SetValue(ref _categories, value);
                OnPropertyChanged(nameof(Categories));
            }
        }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                SetValue(ref _selectedCategory, value);
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        private string _pickerPlaceholder;
        public string PickerPlaceholder
        {
            get { return _pickerPlaceholder; }
            set
            {
                SetValue(ref _pickerPlaceholder, value);
                OnPropertyChanged(nameof(PickerPlaceholder));
            }
        }
        public ICommand SaveCommand { get; private set; }
        #endregion

        public DataInputViewModel(IGeneralInterface generalInterface, IPageService pageService)
        {
            _generalInterface = generalInterface;
            _pageService = pageService;

            PickerPlaceholder = "CATEGORY";
            SelectedCategory = null;

            SaveCommand = new Command(async () => await Save());

        }
        public void RefreshUI()
        {
            TempType = "";
            IsIncomeToggled = false;
            IsExpenseToggled = false;
            IsSavingsToggled = false;
            SelectedCategory = null;
            Amount = null;
            Remarks = null;
        }
        public void UpdateUserDate()
        {
            if (Globals.userData.Id != 0)
            {
                Remarks = Globals.userData.Description;
                SelectedDate = Globals.userData.TransDate;
                TempType = Globals.userData.Type;
                switch (TempType)
                {
                    case "E":
                        IsExpenseToggled = true;
                        break;
                    case "I":
                        IsIncomeToggled = true;
                        break;
                    case "S":
                        IsSavingsToggled = true;
                        break;
                }
                Amount = Globals.userData.Amount.ToString();
                SelectedCategory = Categories.Single(c => c.Code == Globals.userData.Category);

            }
            else
            {
                RefreshUI();
                return;
            }
        }

        private bool ValidateSwitch()
        {
            if (IsExpenseToggled || IsIncomeToggled || IsSavingsToggled)
            {
                return true;
            }
            else if (toggleCount > 1)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        private async Task Save()
        {
            if(Globals.userData.Id != 0)
            {
                if(Remarks != Globals.userData.Description || Convert.ToDecimal(Amount) != Globals.userData.Amount || SelectedDate != Globals.userData.TransDate || SelectedCategory.Code != Globals.userData.Category || Globals.Type != Globals.userData.Type)
                {
                    UserData = new UserData
                    {
                        Id = Globals.userData.Id,
                        Description = Remarks,
                        Category = SelectedCategory.Code,
                        Amount = Convert.ToDecimal(Amount),
                        Pin = Globals.userData.Pin,
                        Type = TempType,
                        TransDate = SelectedDate,
                        UpdateDate = DateTime.Now
                    };

                    if(await _pageService.DisplayAlert("CONFIRMATION","Are you sure you want to update this record?", "YES", "NO"))
                    {
                        await _generalInterface.UpdateUserData(UserData);
                        await _pageService.DisplayAlert("SUCCESS", "Update successful.", "OK");
                        Globals.userData = new UserDataViewModel();
                        RefreshUI();
                    }
                    else
                    {
                        Globals.userData = new UserDataViewModel();
                        RefreshUI();
                        return;
                    }
                }
                else
                {
                    await _pageService.DisplayAlert("ALERT", "No changes made.", "OK");
                    Globals.userData = new UserDataViewModel();
                    RefreshUI();
                    return;
                }
            }
            else
            {
                if (ValidateSwitch())
                {
                    if (!string.IsNullOrEmpty(Remarks) || !string.IsNullOrEmpty(Amount) || SelectedCategory != null)
                    {
                        string _temp = TempType == "E" ? "Expense" : TempType == "I" ? "Income" : "Savings";

                        UserData = new UserData
                        {
                            Category = SelectedCategory.Code,
                            Type = TempType,
                            Description = Remarks,
                            Amount = Convert.ToDecimal(Amount),
                            TransDate = SelectedDate,
                            UpdateDate = DateTime.Now,
                            Pin = Globals.currentUser.Pin
                        };
                        
                        if (await _pageService.DisplayAlert("CONFIRMATION", $"Are you sure you want to post the following details?" +
                            $" Type: {_temp}, Category: {SelectedCategory.Description}, Description: {Remarks}, Amount: {Amount}, Transaction Date: {SelectedDate}", "YES", "NO"))
                        {
                            await _generalInterface.AddUserData(UserData);
                            await _pageService.DisplayAlert("SUCCESS", "Posting successful.", "OK");
                            RefreshUI();
                        }
                        else
                        {
                            await _pageService.DisplayAlert("ABORTED", "Posting aborted.", "OK");
                            return;
                        }
                    }
                    else
                    {
                        await _pageService.DisplayAlert("ERROR", "Please complete record details, Category, Description, Date and Amount are required.", "OK");
                        return;
                    }
                }
                else
                {
                    await _pageService.DisplayAlert("ERROR", "Please select entry type INCOME, EXPENSE or SAVINGS?", "OK");
                    return;
                }
            }
        }


        #region Initializer
        public void InitializeCategories()
        {
            if (IsExpenseToggled)
            {
                TempType = "E";
                Categories = Globals.ExpenseCategories;
            }
            if (IsIncomeToggled)
            {
                TempType = "I";
                Categories = Globals.IncomeCategories;
            }
            if (IsSavingsToggled)
            {
                TempType = "S";
                Categories = Globals.SavingsCategories;
            }
            if (!ValidateSwitch())
            {
                Categories = null;
            }
            PickerPlaceholder = "CATEGORY";
        }
        public void InitializeTitle()
        {
            toggleCount = 0;

            if (IsExpenseToggled)
            {
                toggleCount += 1;
                if (Globals.userData.Id == 0)
                {
                    Title = "New Expense";
                }
                else
                {
                    Title = "Update Expense";
                }
            }
            if (IsIncomeToggled)
            {
                toggleCount += 1;
                if (Globals.userData.Id == 0)
                {
                    Title = "New Income";
                }
                else
                {
                    Title = "Update Income";
                }
            }
            if (IsSavingsToggled)
            {
                toggleCount += 1;
                if (Globals.userData.Id == 0)
                {
                    Title = "New Savings";
                }
                else
                {
                    Title = "Update Savings";
                }
            }
            if (!ValidateSwitch())
            {
                Title = "";
            }
            InitializeCategories();
        }
        #endregion
    }
}
