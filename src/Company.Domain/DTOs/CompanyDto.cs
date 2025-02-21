using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.DTOs
{
    public class CompanyDto
    {
        public string Name { get;  set; }
        public string StockTicker { get;  set; }
        public string Exchange { get;  set; }
        public string Isin { get;  set; }
        public string Website { get;  set; }
    }
}
