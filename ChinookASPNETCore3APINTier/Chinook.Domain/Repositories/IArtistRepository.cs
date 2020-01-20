using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.Entities;

namespace Chinook.Domain.Repositories
{
    public interface IArtistRepository : IDisposable
    {
        bool ArtistExists(int id);
        Task<IAsyncEnumerable<Artist>> GetAll();
        Task<Artist> GetById(int id);
        Task<Artist> Add(Artist newArtist);
        Task<bool> Update(Artist artist);
        Task<bool> Delete(int id);
    }
}