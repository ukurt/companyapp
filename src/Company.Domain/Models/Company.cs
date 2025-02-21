using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.Models
{
    public class Company
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string StockTicker { get; private set; }
        public string Exchange { get; private set; }
        public string Isin { get; private set; }
        public string Website { get; private set; }


        public Company(string name, string stockTicker, string exchange, string isin, string website)
        {
            ValidateIsin(isin);
            Name = name;
            StockTicker = stockTicker;
            Exchange = exchange;
            Isin = isin;
            Website = website;
        }

        public void Update(string name, string stockTicker, string exchange, string website)
        {
            Name = name;
            StockTicker = stockTicker;
            Exchange = exchange;
            Website = website;
        }

        private void ValidateIsin(string isin)
        {
            if (isin.Length != 12 || !char.IsLetter(isin[0]) || !char.IsLetter(isin[1]))
                throw new ArgumentException("ISIN must be 12 characters long and start with two letters.");
        }
    }
}
