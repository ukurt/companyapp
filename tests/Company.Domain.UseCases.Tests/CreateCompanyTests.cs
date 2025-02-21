using Company.Domain.Ports;
using Company.Domain.UseCases.CreateCompany;
using Moq;
using System.Reflection;

namespace Company.Domain.UseCases.Tests
{
    public class CreateCompanyTests
    {
        [Fact]
        public async Task Handle_Should_Create_Company_And_Save()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var handler = new CreateCompanyHandler(mockCompanyRepository.Object);

            var command = new CreateCompanyCommand
            {
                Name = "Test Company",
                StockTicker = "TC",
                Exchange = "NYSE",
                Isin = "US1234567890",
                Website = "https://testcompany.com"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockCompanyRepository.Verify(repo => repo.AddAsync(It.IsAny<Company.Domain.Models.Company>()), Times.Once);
            mockCompanyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Isin_Is_Invalid()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var handler = new CreateCompanyHandler(mockCompanyRepository.Object);

            var command = new CreateCompanyCommand
            {
                Name = "Test Company",
                StockTicker = "TC",
                Exchange = "NYSE",
                Isin = "INVALIDISIN",
                Website = "https://testcompany.com"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("ISIN must be 12 characters long and start with two letters.", exception.Message);
        }
    }
}
