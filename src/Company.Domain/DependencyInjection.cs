using Company.Domain.UseCases.CreateCompany;
using Company.Domain.UseCases.GetAllCompanies;
using Company.Domain.UseCases.GetCompanyById;
using Company.Domain.UseCases.GetCompanyByIsin;
using Company.Domain.UseCases.UpdateCompany;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRequestHandler<CreateCompanyCommand, int>, CreateCompanyHandler>();
            services.AddScoped<IRequestHandler<GetAllCompaniesQuery, IEnumerable<Models.Company>>, GetAllCompaniesHandler>();
            services.AddScoped<IRequestHandler<GetCompanyByIdQuery, Models.Company>, GetCompanyByIdHandler>();
            services.AddScoped<IRequestHandler<GetCompanyByIsinQuery, Models.Company>, GetCompanyByIsinHandler>();
            services.AddScoped<IRequestHandler<UpdateCompanyCommand>, UpdateCompanyHandler>();

            return services;
        }
    }
}
