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
    public class GenreRepositoryTest
    {
        private readonly GenreRepository _repo;

        public GenreRepositoryTest()
        {
            _repo = new GenreRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GenreGetAll()
        {
            // Act
            var genres= await (await _repo.GetAll()).ToListAsync();

            // Assert
            Assert.Single(genres);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Genre)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new GenreRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<Genre>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}