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

namespace MEESEES2.ViewModels
{
    public class DataListViewModel : BaseViewModel
    {
        IPageService _pageService;
        IGeneralInterface _generalInterface;

        #region Properties
        private int toggleCount = 0;
        private string _testImage;
        public string TestImage
        {
            get { return _testImage; }
            set
            {
                SetValue(ref _testImage, value);
                OnPropertyChanged(nameof(TestImage));
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
        private DateTime _fromDate;
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                SetValue(ref _fromDate, value);
                OnPropertyChanged(nameof(FromDate));
            }
        }
        private DateTime _toDate;
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                SetValue(ref _toDate, value);
                OnPropertyChanged(nameof(ToDate));
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
        private ObservableCollection<UserDataViewModel> _listViewData;
        public ObservableCollection<UserDataViewModel> ListViewData
        {
            get { return _listViewData; }
            set
            {
                SetValue(ref _listViewData, value);
                OnPropertyChanged(nameof(ListViewData));
            }
        }
        private ObservableCollection<UserDataViewModel> _origData;
        public ObservableCollection<UserDataViewModel> OrigData
        {
            get { return _origData; }
            set
            {
                SetValue(ref _origData, value);
                OnPropertyChanged(nameof(OrigData));
            }
        }
        private UserDataViewModel _selectedData;
        public UserDataViewModel SelectedData
        {
            get { return _selectedData; }
            set
            {
                SetValue(ref _selectedData, value);
                OnPropertyChanged(nameof(SelectedData));
            }
        }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand SelectDataCommand { get; private set; }
        public ICommand DeleteDataCommand { get; private set; }

        #endregion

        public DataListViewModel(IGeneralInterface generalInterface, IPageService pageService)
        {
            _generalInterface = generalInterface;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            SelectDataCommand = new Command<UserDataViewModel>(async c => await SelectData(c));
            DeleteDataCommand = new Command<UserDataViewModel>(async c => await DeleteData(c));
        }
        public void ResetList()
        {
            ListViewData = new ObservableCollection<UserDataViewModel>();
            ListViewData.Clear();
            OrigData = new ObservableCollection<UserDataViewModel>();
            OrigData.Clear();
        }

        private bool ValidateDate()
        {
            if(FromDate == ToDate || FromDate > ToDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidateSwitch()
        {
            if (IsExpenseToggled || IsIncomeToggled || IsSavingsToggled)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task SelectData(UserDataViewModel data)
        {
            if (SelectedData == null) return;
            Globals.userData = OrigData.Single(d => d.Id == data.Id);
            //Globals.userData = data;
            await Shell.Current.GoToAsync("//EntryPage");
        }
        public async Task DeleteData(UserDataViewModel data)
        {
            string _type = data.Type == "F" ? "Fund :" : data.Type == "S" ? "Savings :" : data.Type=="E" ? "Expense :" : "";
            if (await _pageService.DisplayAlert("WARNING",$"Are you sure you want to delete this record? Type : {_type}, Amount : {data.Amount}, Remarks : {data.Description}", "YES", "NO"))
            {
                var userdata = await _generalInterface.GetUserData(data.Id);
                await _generalInterface.DeleteUserData(userdata);
                ListViewData.Remove(data);
            }
            else
            {
                return;
            }
        }

        public async Task LoadData()
        {
            if (ValidateDate())
            {
                var datalist = await _generalInterface.GetUserDataByPin(Globals.currentUser.Pin);
                ListViewData = new ObservableCollection<UserDataViewModel>();
                ListViewData.Clear();

                OrigData = new ObservableCollection<UserDataViewModel>();
                OrigData.Clear();

                if (!ValidateSwitch())
                {
                    ListViewData.Clear();
                    if (SelectedCategory != null)
                    {
                        foreach (UserData data in datalist)
                        {
                            if (data.TransDate >= FromDate && data.TransDate <= ToDate && data.Category == SelectedCategory.Code)
                            {
                                ListViewData.Add(new UserDataViewModel(data,"list"));
                                OrigData.Add(new UserDataViewModel(data, "new"));
                            }
                        }
                    }
                    else
                    {
                        foreach (UserData data in datalist)
                        {
                            if (data.TransDate >= FromDate && data.TransDate <= ToDate)
                            {
                                ListViewData.Add(new UserDataViewModel(data,"list"));
                                OrigData.Add(new UserDataViewModel(data, "new"));
                            }
                        }
                    }
                }
                else
                {
                    ListViewData.Clear();
                    string _tempExpense = IsExpenseToggled ? "E" : "";
                    string _tempIncome = IsIncomeToggled ? "I" : "";
                    string _tempSavings = IsSavingsToggled ? "S" : "";

                    if (SelectedCategory != null && toggleCount == 1)
                    {
                        foreach (UserData data in datalist)
                        {
                            if (data.TransDate >= FromDate && data.TransDate <= ToDate && data.Category == SelectedCategory.Code && data.Type == TempType)
                            {
                                ListViewData.Add(new UserDataViewModel(data,"list"));
                                OrigData.Add(new UserDataViewModel(data, "new"));
                            }
                        }
                    }
                    else
                    {
                        foreach (UserData data in datalist)
                        {
                            if (data.TransDate >= FromDate && data.TransDate <= ToDate && (data.Type == _tempExpense || data.Type == _tempSavings || data.Type == _tempIncome))
                            {
                                ListViewData.Add(new UserDataViewModel(data,"list"));
                                OrigData.Add(new UserDataViewModel(data, "new"));
                            }
                        }
                    }
                }
            }
            else
            {
                await _pageService.DisplayAlert("ERROR", "Please input valid date range.", "OK");
                return;
            }
        }


        #region Initializer
        public void InitializeCategories()
        {
            if (IsExpenseToggled)
            {
                toggleCount += 1;
                TempType = "E";
                Categories = Globals.ExpenseCategories;
            }

            if (IsIncomeToggled)
            {
                toggleCount += 1;
                TempType = "I";
                Categories = Globals.IncomeCategories;
            }

            if (IsSavingsToggled)
            {
                toggleCount += 1;
                TempType = "S";
                Categories = Globals.SavingsCategories;
            }

            if (!ValidateSwitch())
            {
                toggleCount = 0;
                Categories = null;
            }
        }
        #endregion
    }
}
