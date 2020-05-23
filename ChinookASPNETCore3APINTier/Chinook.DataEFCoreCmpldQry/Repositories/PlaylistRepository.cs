using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class PlaylistRepository : EfRepository<Album>
    {
        public PlaylistRepository(ChinookContext context) : base(context)
        {
        }

        public List<Playlist> GetAll() 
            => _dbContext.GetAllPlaylists();

        public Playlist GetById(int id)
        {
            var playlist = _dbContext.GetPlaylist(id);
            return playlist;
        }

        public Playlist Add(Playlist newPlaylist)
        {
            _dbContext.Playlist.Add(newPlaylist);
            _dbContext.SaveChanges();
            return newPlaylist;
        }

        public bool Update(Playlist playlist)
        {
            if (!Exists(playlist.PlaylistId))
                return false;
            _dbContext.Playlist.Update(playlist);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Playlist.Find(id);
            _dbContext.Playlist.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Playlist> GetByTrackId(int id) => _dbContext.GetPlaylistByTrackId(id);
    }
}