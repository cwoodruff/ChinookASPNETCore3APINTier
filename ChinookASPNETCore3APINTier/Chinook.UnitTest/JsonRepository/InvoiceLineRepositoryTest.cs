using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.JsonRepository
{
    public class InvoiceLineRepositoryTest
    {
        private readonly IInvoiceLineRepository _repo;

        public InvoiceLineRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IInvoiceLineRepository, InvoiceLineRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IInvoiceLineRepository>();
        }

        [Fact]
        public void InvoiceLineGetAll()
        {
            // Act
            var invoiceLines = _repo.GetAll();

            // Assert
            Assert.True(invoiceLines.Count > 1, "The number of invoice lines was not greater than 1");
        }
    }
}