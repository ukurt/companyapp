using Company.Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<Models.Company>>
    {
    }

    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<Models.Company>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Models.Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetAllAsync();
        }
    }
}
