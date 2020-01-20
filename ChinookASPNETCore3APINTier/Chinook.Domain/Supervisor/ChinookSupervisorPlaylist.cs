using System;
using System.Collections.Generic;
using Chinook.Domain.ApiModels;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool PlaylistExists(int id) => _playlistRepository.PlaylistExists(id);
        public async Task<IAsyncEnumerable<PlaylistApiModel>> GetAllPlaylist()
        {
            var playlists = (await _playlistRepository.GetAll()).ConvertAll();
            await foreach (var playlist in playlists)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Playlist-", playlist.PlaylistId), playlist, cacheEntryOptions);
            }
            return playlists;
        }

        public async Task<PlaylistApiModel> GetPlaylistById(int id)
        {
            var playlistApiModelCached = _cache.Get<PlaylistApiModel>(string.Concat("Playlist-", id));

            if (playlistApiModelCached != null)
            {
                return playlistApiModelCached;
            }
            else
            {
                var playlistApiModel = (await _playlistRepository.GetById(id)).Convert();
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Playlist-", playlistApiModel.PlaylistId), playlistApiModel, cacheEntryOptions);

                return playlistApiModel;
            }
        }

        public async Task<PlaylistApiModel> AddPlaylist(PlaylistApiModel newPlaylistApiModel)
        {
            var playlist = newPlaylistApiModel.Convert();
            playlist = await  _playlistRepository.Add(playlist);
            return playlist.Convert();
        }

        public async Task<bool> UpdatePlaylist(PlaylistApiModel playlistApiModel) => 
            await _playlistRepository.Update(playlistApiModel.Convert());

        public async Task<bool> DeletePlaylist(int id) => await _playlistRepository.Delete(id);
    }
}