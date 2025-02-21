using Company.Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockTicker { get; set; }
        public string Exchange { get; set; }
        public string Website { get; set; }
    }

    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.Id);
            if (company == null)
                throw new KeyNotFoundException("Company not found.");

            company.Update(request.Name, request.StockTicker, request.Exchange, request.Website);
            await _companyRepository.SaveChangesAsync();
        }
    }
}
