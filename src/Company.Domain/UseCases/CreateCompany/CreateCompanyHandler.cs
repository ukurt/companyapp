using Company.Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.CreateCompany
{
    public class CreateCompanyCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string StockTicker { get; set; }
        public string Exchange { get; set; }
        public string Isin { get; set; }
        public string Website { get; set; }
    }

    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            Models.Company company = new(
                request.Name,
                request.StockTicker,
                request.Exchange,
                request.Isin,
                request.Website
            );

            await _companyRepository.AddAsync(company);
            await _companyRepository.SaveChangesAsync();

            return company.Id;
        }
    }
}
