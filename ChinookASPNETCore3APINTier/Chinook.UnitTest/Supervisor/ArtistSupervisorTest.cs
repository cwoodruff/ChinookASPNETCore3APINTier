using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Chinook.MockData.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Supervisor;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class ArtistSupervisorTest
    {
        private readonly ChinookSupervisor _super;

        public ArtistSupervisorTest(ChinookSupervisor super)
        {
            _super = super;
        }

        [DotMemoryUnitAttribute(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task ArtistGetAll()
        {
            // Act
            var artists = await (await _super.GetAllArtist()).ToListAsync();

            // Assert
            Assert.Single(artists);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Artist)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new ArtistRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<Artist>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}