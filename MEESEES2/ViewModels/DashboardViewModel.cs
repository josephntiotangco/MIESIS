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

namespace MEESEES2.ViewModels
{
    public class DashboardViewModel :BaseViewModel
    {
        IGeneralInterface _generalInterface;
        IPageService _pageService;

        #region Properties
        private double _filterHeight;
        public double FilterHeight
        {
            get { return _filterHeight; }
            set
            {
                SetValue(ref _filterHeight, value);
                OnPropertyChanged(nameof(FilterHeight));
            }
        }
        private bool _filterVisible;
        public bool FilterVisible
        {
            get { return _filterVisible; }
            set
            {
                SetValue(ref _filterVisible, value);
                OnPropertyChanged(nameof(FilterVisible));
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
        private decimal _totalIncome;
        public decimal TotalIncome
        {
            get { return _totalIncome; }
            set
            {
                SetValue(ref _totalIncome, value);
                OnPropertyChanged(nameof(TotalIncome));
            }
        }
        private decimal _totalExpenses;
        public decimal TotalExpenses
        {
            get { return _totalExpenses; }
            set
            {
                SetValue(ref _totalExpenses, value);
                OnPropertyChanged(nameof(TotalExpenses));
            }
        }
        private decimal _totalSavings;
        public decimal TotalSavings
        {
            get { return _totalSavings; }
            set
            {
                SetValue(ref _totalSavings, value);
                OnPropertyChanged(nameof(TotalSavings));
            }
        }
        private decimal _totalBalance;
        public decimal TotalBalance
        {
            get { return _totalBalance; }
            set
            {
                SetValue(ref _totalBalance, value);
                OnPropertyChanged(nameof(TotalBalance));
            }
        }
        private decimal _targetSavings;
        public decimal TargetSavings
        {
            get { return _targetSavings; }
            set
            {
                SetValue(ref _targetSavings, value);
                OnPropertyChanged(nameof(TargetSavings));
            }
        }
        private ObservableCollection<UserDataViewModel> _userInputs;
        public ObservableCollection<UserDataViewModel> UserInputs
        {
            get { return _userInputs; }
            set
            {
                SetValue(ref _userInputs, value);
                OnPropertyChanged(nameof(UserInputs));
            }
        }
        private ObservableCollection<UserSettingViewModel> _settings;
        public ObservableCollection<UserSettingViewModel> Settings
        {
            get { return _settings; }
            set
            {
                SetValue(ref _settings, value);
                OnPropertyChanged(nameof(Settings));
            }
        }
        private ObservableCollection<ChartData> _pieChart;
        public ObservableCollection<ChartData> PieChart
        {
            get { return _pieChart; }
            set
            {
                SetValue(ref _pieChart, value);
                OnPropertyChanged(nameof(PieChart));
            }
        }
        private ObservableCollection<ChartData> _barChart;
        public ObservableCollection<ChartData> BarChart
        {
            get { return _barChart; }
            set
            {
                SetValue(ref _barChart, value);
                OnPropertyChanged(nameof(BarChart));
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
        public ICommand RefreshCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand LoadSettingCommand { get; private set; }
        public ICommand ShowFilterCommand { get; private set; }
        public ICommand LoadFilterDataCommand { get; private set; }
        public ICommand DataTappedCommand { get; private set; }
        public ICommand TargetTappedCommand { get; private set; }
        #endregion


        public DashboardViewModel(IGeneralInterface generalInterface, IPageService pageService)
        {
            _generalInterface = generalInterface;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            LoadSettingCommand = new Command(async () => await LoadSetting());
            RefreshCommand = new Command(async () => await Refresh());
            ShowFilterCommand = new Command(async () => await Show());
            LoadFilterDataCommand = new Command(async () => await LoadFilteredData());
            DataTappedCommand = new Command(async () => await GoToData());
            TargetTappedCommand = new Command(async () => await GoToSetting());
        }

        public async Task GoToData()
        {
            await Shell.Current.GoToAsync("//EntryPage");
        }
        public async Task GoToSetting()
        {
            await Shell.Current.GoToAsync("//SettingPage");
        }
        public async Task LoadData()
        {   
            UserInputs = new ObservableCollection<UserDataViewModel>();
            UserInputs.Clear();

            var usersData = await _generalInterface.GetUserDataByPin(Globals.currentUser.Pin);
            foreach (UserData data in usersData)
                UserInputs.Add(new UserDataViewModel(data, "new"));

            TotalIncome = UserInputs.Where(d => d.Type == "I").Sum(d => d.Amount);
            TotalExpenses = UserInputs.Where(d => d.Type == "E").Sum(d => d.Amount);
            TotalSavings = UserInputs.Where(d => d.Type == "S").Sum(d => d.Amount);
            TotalBalance = TotalIncome - TotalExpenses - TotalSavings;

            RefreshChart();            
        }
        public async Task LoadSetting()
        {
            Settings = new ObservableCollection<UserSettingViewModel>();
            Settings.Clear();

            var settings = await _generalInterface.GetUserSettingsByPin(Globals.currentUser.Pin);
            foreach (UserSetting setting in settings)
            {
                if(setting.Code == "SAVAMT")
                {
                    Settings.Add(new UserSettingViewModel(setting));
                    TargetSavings = Convert.ToDecimal(setting.Value);
                }
            }
        }
        public async Task LoadFilteredData()
        {
            UserInputs = new ObservableCollection<UserDataViewModel>();
            UserInputs.Clear();

            var usersData = await _generalInterface.GetUserDataByPin(Globals.currentUser.Pin);
            foreach (UserData data in usersData)
            {
                if(data.TransDate >= FromDate && data.TransDate <= ToDate)
                {
                    UserInputs.Add(new UserDataViewModel(data, "new"));
                }
            }

            TotalIncome = UserInputs.Where(d => d.Type == "I").Sum(d => d.Amount);
            TotalExpenses = UserInputs.Where(d => d.Type == "E").Sum(d => d.Amount);
            TotalSavings = UserInputs.Where(d => d.Type == "S").Sum(d => d.Amount);
            TotalBalance = TotalIncome - TotalExpenses - TotalSavings;

            RefreshChart();
        }
        public async Task Refresh()
        {
            IsRefreshing = true;
            await LoadData();
            await LoadSetting();
            IsRefreshing = false;
        }
        public async Task Show()
        {
            if(FilterHeight == 0)
            {
                FilterVisible = true;
                FilterHeight = 50;
                await _pageService.DisplayAlert("INFORMATION", "Filter controls activated.", "OK");
            }
            else
            {
                FilterVisible = false;
                FilterHeight = 0;
            }
        }
        public void RefreshChart()
        {
            PieChart = new ObservableCollection<ChartData>();
            PieChart.Clear();

            PieChart.Add(new ChartData
            {
                Actual = TotalBalance,
                Type = "Remaining Balance",
                Target = 0
            });
            PieChart.Add(new ChartData
            {
                Actual = TotalSavings,
                Type = "Actual Savings",
                Target = 0
            });
            PieChart.Add(new ChartData
            {
                Actual = TotalExpenses,
                Type = "Expenses",
                Target = 0
            });


            BarChart = new ObservableCollection<ChartData>();
            BarChart.Clear();

            BarChart.Add(new ChartData
            {
                Actual = TotalSavings,
                Type = "Savings",
                Target = TargetSavings
            });

        }

    }
}
