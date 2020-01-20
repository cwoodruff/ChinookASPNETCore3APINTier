using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Chinook.MockData.Repositories;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class InvoiceLineRepositoryTest
    {
        private readonly InvoiceLineRepository _repo;

        public InvoiceLineRepositoryTest()
        {
            _repo = new InvoiceLineRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task InvoiceLineGetAll()
        {
            // Act
            var invoiceLines= await (await _repo.GetAll()).ToListAsync();

            // Assert
            Assert.Single(invoiceLines);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(InvoiceLine)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new InvoiceLineRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<InvoiceLine>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}