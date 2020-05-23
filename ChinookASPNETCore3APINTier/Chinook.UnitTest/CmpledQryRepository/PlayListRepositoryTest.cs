using Chinook.DataEFCoreCmpldQry.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.CmpledQryRepository
{
    public class PlayListRepositoryTest
    {
        private readonly IPlaylistRepository _repo;

        public PlayListRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IPlaylistRepository>();
        }

        [Fact]
        public void PlayListGetAll()
        {
            // Act
            var playLists = _repo.GetAll();

            // Assert
            Assert.True(playLists.Count > 1, "The number of play lists was not greater than 1");
        }
    }
}