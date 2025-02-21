using Company.Domain.Ports;
using Company.Domain.UseCases.UpdateCompany;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.UseCases.Tests
{
    public class UpdateCompanyTest
    {
        [Fact]
        public async Task Handle_Should_Update_Company_When_Company_Exists()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var company = new Company.Domain.Models.Company("Company 1", "TICKER1", "NYSE", "US1234567890", "https://company1.com");

            mockCompanyRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(company);

            mockCompanyRepository
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var handler = new UpdateCompanyHandler(mockCompanyRepository.Object);
            var command = new UpdateCompanyCommand
            {
                Id = company.Id,
                Name = "Updated Company",
                StockTicker = "TICKER2",
                Exchange = "NASDAQ",
                Website = "https://updatedcompany.com"
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Updated Company", company.Name);
            Assert.Equal("TICKER2", company.StockTicker);
            Assert.Equal("NASDAQ", company.Exchange);
            Assert.Equal("https://updatedcompany.com", company.Website);

            mockCompanyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Company_Not_Found()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();

            mockCompanyRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Company.Domain.Models.Company)null);

            var handler = new UpdateCompanyHandler(mockCompanyRepository.Object);
            var command = new UpdateCompanyCommand
            {
                Id = 999, 
                Name = "Non-existing Company",
                StockTicker = "TICKER999",
                Exchange = "NASDAQ",
                Website = "https://nonexistingcompany.com"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Company not found.", exception.Message);
        }
    }
}
