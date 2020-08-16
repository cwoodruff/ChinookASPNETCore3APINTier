using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataMartendb.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Playlist> GetAll()
        {
            throw new NotImplementedException();
        }

        public Playlist GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Playlist Add(Playlist newPlaylist)
        {
            throw new NotImplementedException();
        }

        public bool Update(Playlist playlist)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Playlist> GetByTrackId(int id)
        {
            throw new NotImplementedException();
        }
    }
}