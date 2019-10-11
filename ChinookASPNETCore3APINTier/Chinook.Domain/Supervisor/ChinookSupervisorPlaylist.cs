using System;
using System.Collections.Generic;
using System.Threading;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using System.Linq;
using Chinook.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<PlaylistApiModel> GetAllPlaylist()
        {
            var playlists = _playlistRepository.GetAll();
            foreach (var playlist in playlists)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(playlist.PlaylistId, playlist, cacheEntryOptions);
            }
            return playlists.ConvertAll();
        }

        public PlaylistApiModel GetPlaylistById(int id)
        {
            var playlist = _cache.Get<Playlist>(id);

            if (playlist != null)
            {
                var playlistApiModel = playlist.Convert;
                playlistApiModel.Tracks = (GetTrackByPlaylistIdId(playlistApiModel.PlaylistId)).ToList();
                return playlistApiModel;
            }
            else
            {
                var playlistApiModel = (_playlistRepository.GetById(id)).Convert;
                playlistApiModel.Tracks = (GetTrackByPlaylistIdId(playlistApiModel.PlaylistId)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(playlistApiModel.PlaylistId, playlistApiModel, cacheEntryOptions);

                return playlistApiModel;
            }
        }

        public PlaylistApiModel AddPlaylist(PlaylistApiModel newPlaylistApiModel)
        {
            /*var playlist = new Playlist
            {
                Name = newPlaylistApiModel.Name
            };*/

            var playlist = newPlaylistApiModel.Convert;

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