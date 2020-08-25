using System;
using System.Collections.Generic;
using System.Text;

namespace MEESEES2.Models
{
    public class ChartData
    {
        public string Type { get; set; }
        public decimal Actual { get; set; }
        public decimal Target { get; set; }

        public ChartData() { }
        public ChartData(string type, decimal actual, decimal target)
        {
            this.Type = type;
            this.Actual = actual;
            this.Target = target;
        }
    }
}
