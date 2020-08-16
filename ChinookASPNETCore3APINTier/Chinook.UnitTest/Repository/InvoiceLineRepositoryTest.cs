using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class InvoiceLineRepositoryTest
    {
        private readonly IInvoiceLineRepository _repo;

        public InvoiceLineRepositoryTest()
        {
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