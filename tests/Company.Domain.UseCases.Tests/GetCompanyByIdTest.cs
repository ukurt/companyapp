using Company.Domain.Ports;
using Company.Domain.UseCases.GetCompanyById;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.Tests
{
    public class GetCompanyByIdTest
    {
        [Fact]
        public async Task Handle_Should_Return_Company_When_Company_Exists()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var company = new Company.Domain.Models.Company("Company 1", "TICKER1", "NYSE", "US1234567890", "https://company1.com");

            mockCompanyRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(company);

            var handler = new GetCompanyByIdHandler(mockCompanyRepository.Object);
            var query = new GetCompanyByIdQuery { Id = 1 }; 

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockCompanyRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);

            Assert.NotNull(result); 
            Assert.Equal("Company 1", result.Name); 
            Assert.Equal("TICKER1", result.StockTicker); 
        }

        [Fact]
        public async Task Handle_Should_Return_Null_When_Company_Does_Not_Exist()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            Company.Domain.Models.Company company = null; 

            mockCompanyRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(company);

            var handler = new GetCompanyByIdHandler(mockCompanyRepository.Object);
            var query = new GetCompanyByIdQuery { Id = 999 }; 

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockCompanyRepository.Verify(repo => repo.GetByIdAsync(999), Times.Once);

            Assert.Null(result); 
        }
    }
}
