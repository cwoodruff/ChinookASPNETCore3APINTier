using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class TrackRepositoryTest
    {
        private readonly ITrackRepository _repo;

        public TrackRepositoryTest()
        {
        }

        [Fact]
        public void TrackGetAll()
        {
            // Act
            var tracks = _repo.GetAll();

            // Assert
            Assert.True(tracks.Count > 1, "The number of tracks was not greater than 1");
        }
    }
}