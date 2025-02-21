using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.Ports
{
    public interface ICompanyRepository
    {
        Task AddAsync(Models.Company company);
        Task<Models.Company> GetByIdAsync(int id);
        Task<Models.Company> GetByIsinAsync(string isin);
        Task<IEnumerable<Models.Company>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
