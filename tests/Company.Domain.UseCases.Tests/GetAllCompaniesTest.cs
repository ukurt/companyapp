using Company.Domain.Ports;
using Company.Domain.UseCases.GetAllCompanies;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.Tests
{
    public class GetAllCompaniesTest
    {
        [Fact]
        public async Task Handle_Should_Return_Companies_When_Companies_Exist()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var companies = new List<Company.Domain.Models.Company>
            {
                new Company.Domain.Models.Company("Company 1", "TICKER1", "NYSE", "US1234567890", "https://company1.com"),
                new Company.Domain.Models.Company("Company 2", "TICKER2", "NASDAQ", "US0987654321", "https://company2.com")
            };

            mockCompanyRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(companies);

            var handler = new GetAllCompaniesHandler(mockCompanyRepository.Object);
            var query = new GetAllCompaniesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockCompanyRepository.Verify(repo => repo.GetAllAsync(), Times.Once);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, company => company.Name == "Company 1");
            Assert.Contains(result, company => company.Name == "Company 2");
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_List_When_No_Companies_Exist()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var companies = new List<Company.Domain.Models.Company>(); 

            mockCompanyRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(companies);

            var handler = new GetAllCompaniesHandler(mockCompanyRepository.Object);
            var query = new GetAllCompaniesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockCompanyRepository.Verify(repo => repo.GetAllAsync(), Times.Once);

            Assert.Empty(result);
        }
    }
}
