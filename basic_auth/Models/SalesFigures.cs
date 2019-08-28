using System;

namespace basic_auth.Models
{
    public class SalesFigures
    {
        public DateTime Date { get; set; }

        public int SalesMade { get; set; }

        public decimal Profit { get; set;}

        public string Summary { get; set; }
    }
}
