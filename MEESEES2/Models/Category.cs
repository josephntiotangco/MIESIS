using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MEESEES2.Models
{
    public class Category
    {
        [AutoIncrement,PrimaryKey]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }

        public Category() { }

        public Category(int id, string description, string code, string type)
        {
            this.Id = id;
            this.Description = description;
            this.Code = code;
            this.Type = type;
        }
    }
}
