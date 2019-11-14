using System;
using System.Collections.Generic;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<PlaylistApiModel> GetAllPlaylist()
        {
            var playlists = _playlistRepository.GetAll().ConvertAll();
            foreach (var playlist in playlists)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Playlist-", playlist.PlaylistId), playlist, cacheEntryOptions);
            }
            return playlists;
        }

        public PlaylistApiModel GetPlaylistById(int id)
        {
            var playlistApiModelCached = _cache.Get<PlaylistApiModel>(string.Concat("Playlist-", id));

            if (playlistApiModelCached != null)
            {
                return playlistApiModelCached;
            }
            else
            {
                var playlistApiModel = (_playlistRepository.GetById(id)).Convert();
                playlistApiModel.Tracks = (GetTrackByPlaylistIdId(playlistApiModel.PlaylistId)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Playlist-", playlistApiModel.PlaylistId), playlistApiModel, cacheEntryOptions);

                return playlistApiModel;
            }
        }

        public PlaylistApiModel AddPlaylist(PlaylistApiModel newPlaylistApiModel)
        {
            var playlist = newPlaylistApiModel.Convert();

            playlist = _playlistRepository.Add(playlist);
            newPlaylistApiModel.PlaylistId = playlist.PlaylistId;
            return newPlaylistApiModel;
        }

        public bool UpdatePlaylist(PlaylistApiModel playlistApiModel)
        {
            var playlist = _playlistRepository.GetById(playlistApiModel.PlaylistId);

            if (playlist == null) return false;
            playlist.PlaylistId = playlistApiModel.PlaylistId;
            playlist.Name = playlistApiModel.Name;

            return _playlistRepository.Update(playlist);
        }

        public bool DeletePlaylist(int id) 
            => _playlistRepository.Delete(id);
    }
}