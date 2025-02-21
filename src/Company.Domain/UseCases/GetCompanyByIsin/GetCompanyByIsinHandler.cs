using Company.Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.GetCompanyByIsin
{
    public class GetCompanyByIsinQuery : IRequest<Models.Company>
    {
        public string Isin { get; set; }
    }

    public class GetCompanyByIsinHandler : IRequestHandler<GetCompanyByIsinQuery, Models.Company>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIsinHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Models.Company> Handle(GetCompanyByIsinQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetByIsinAsync(request.Isin);
        }
    }
}
