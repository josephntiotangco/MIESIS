using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MEESEES2.Models
{
    public class User
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }

        public User() { }
        public User(string pin, string name)
        {
            this.Pin = pin;
            this.Name = name;
        }
    }
}
