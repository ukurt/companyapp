using Company.Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<Models.Company>
    {
        public int Id { get; set; }
    }

    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, Models.Company>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIdHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Models.Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetByIdAsync(request.Id);
        }
    }
}
