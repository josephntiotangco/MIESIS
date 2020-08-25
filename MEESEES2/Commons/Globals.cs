using MEESEES2.Models;
using MEESEES2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MEESEES2.Commons
{
    public class Globals
    {
        //all static variables here
        #region Users
        public static bool isUsersLoaded;
        public static UserViewModel currentUser = new UserViewModel();
        public static ObservableCollection<UserViewModel> Users = new ObservableCollection<UserViewModel>();
        #endregion

        #region Category
        public static ObservableCollection<Category> ExpenseCategories = new ObservableCollection<Category>();
        public static ObservableCollection<Category> IncomeCategories = new ObservableCollection<Category>();
        public static ObservableCollection<Category> SavingsCategories = new ObservableCollection<Category>();
        #endregion

        #region Entry
        public static UserDataViewModel userData = new UserDataViewModel();
        public static string Type;
        #endregion

        #region Settings
        public static decimal targetSavings;
        public static bool isNotify;
        public static bool isSuccess;
        public static string lastError;
        #endregion

    }
}
