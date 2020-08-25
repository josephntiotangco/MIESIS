using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MEESEES2.Models
{
    public class UserSetting
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Pin { get; set; }
        public UserSetting() { }
        public UserSetting(string code, string value, string pin)
        {
            this.Code = code;
            this.Value = value;
            this.Pin = pin;
        }
    }
}
