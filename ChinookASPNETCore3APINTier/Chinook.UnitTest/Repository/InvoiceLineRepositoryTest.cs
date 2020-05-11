using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
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

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void InvoiceLineGetAll()
        {
            // Act
            var invoiceLines = _repo.GetAll();

            // Assert
            Assert.True(invoiceLines.Count > 1, "The number of invoice lines was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 170368, Types = new[] {typeof(InvoiceLine)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new InvoiceLineRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(2662, memory.GetObjects(where => where.Type.Is<InvoiceLine>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}