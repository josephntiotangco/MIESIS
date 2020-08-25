using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MEESEES2.Models
{
    public class UserData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Pin { get; set; }
        public string Description { get; set; } //source if fund
        public decimal Amount { get; set; }
        public string Type { get; set; } //I= Income E=expense S=Savings
        public string Category { get; set; } //Examples of E, I, S
        public DateTime TransDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public UserData() { }
        public UserData(string description, decimal amount, string category, string type, DateTime transdate, DateTime update, string pin)
        {
            this.Description = description;
            this.Amount = amount;
            this.Category = category;
            this.Type = type;
            this.TransDate = transdate;
            this.UpdateDate = update;
            this.Pin = pin;
        }
    }
}
