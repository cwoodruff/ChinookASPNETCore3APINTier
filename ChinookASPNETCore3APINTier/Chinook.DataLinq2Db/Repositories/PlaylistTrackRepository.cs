using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataLinq2Db.Repositories
{
    public class PlaylistTrackRepository : IPlaylistTrackRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<PlaylistTrack> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<PlaylistTrack> GetByPlaylistId(int id)
        {
            throw new NotImplementedException();
        }

        public List<PlaylistTrack> GetByTrackId(int id)
        {
            throw new NotImplementedException();
        }

        public PlaylistTrack Add(PlaylistTrack newPlaylistTrack)
        {
            throw new NotImplementedException();
        }

        public bool Update(PlaylistTrack playlistTrack)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}