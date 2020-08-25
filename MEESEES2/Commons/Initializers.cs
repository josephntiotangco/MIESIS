using MEESEES2.Models;
using MEESEES2.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MEESEES2.Commons
{
    public class Initializers
    {
        public void InitializeCategories()
        {
            InitializeExpenseCategory();
            InitializeIncomeCategory();
            InitializeSavingsCategory();
        }
        public void InitializeIncomeCategory()
        {
            string[] defaultIncome = { "Salary", "Business", "Freelancing","Interest Income", "Others" };
            Globals.IncomeCategories.Clear();
            for (int i = 0; i < defaultIncome.Length; i++)
            {
                Globals.IncomeCategories.Add(new Category
                {
                    Id = i,
                    Code = "INC" + i,
                    Description = defaultIncome[i],
                    Type = "I"
                });
            }
        }
        public void InitializeExpenseCategory()
        {
            string[] defaultExpenses = { "Electric Bill", "Water Bill", "Rent", "Amortization","Loans" ,
                "Insurance","Mobile Postpaid", "Mobile Prepaid Load", "Internet", "Laundry", "Grocery", "Food","Medicine","Gas", "Parking", "Others" };

            Globals.ExpenseCategories.Clear();
            for (int i = 0; i < defaultExpenses.Length; i++)
            {
                Globals.ExpenseCategories.Add(new Category
                {
                    Id = i,
                    Code = "EXP" + i,
                    Description = defaultExpenses[i],
                    Type = "E"
                });
            }
        }
        public void InitializeSavingsCategory()
        {
            string[] defaultSavings = { "Bank", "Cash Box","Piggy Bank", "Others" };
            Globals.SavingsCategories.Clear();
            for (int i = 0; i < defaultSavings.Length; i++)
            {
                Globals.SavingsCategories.Add(new Category
                {
                    Id = i,
                    Code = "SAV" + i,
                    Description = defaultSavings[i],
                    Type = "S"
                });
            }

        }

    }
}
