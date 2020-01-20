using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool TrackExists(int id) => _trackRepository.TrackExists(id);
        public async Task<IAsyncEnumerable<TrackApiModel>> GetAllTrack()
        {
            var tracks = (await _trackRepository.GetAll()).ConvertAll();
            await foreach (var track in tracks)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Track-", track.TrackId), track, cacheEntryOptions);
            }
            return tracks;
        }

        public async Task<TrackApiModel> GetTrackById(int id)
        {
            var trackApiModelCached = _cache.Get<TrackApiModel>(string.Concat("Track-", id));

            if (trackApiModelCached != null)
            {
                return trackApiModelCached;
            }
            else
            {
                var trackApiModel = (await _trackRepository.GetById(id)).Convert();
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Track-", trackApiModel.TrackId), trackApiModel, cacheEntryOptions);

                return trackApiModel;
            }
        }

        public async Task<IAsyncEnumerable<TrackApiModel>> GetTrackByAlbumId(int id) => (await _trackRepository.GetByAlbumId(id)).ConvertAll();

        public async Task<IAsyncEnumerable<TrackApiModel>> GetTrackByGenreId(int id) => (await _trackRepository.GetByGenreId(id)).ConvertAll();

        public async Task<IAsyncEnumerable<TrackApiModel>> GetTrackByMediaTypeId(int id) => (await _trackRepository.GetByMediaTypeId(id)).ConvertAll();

        public IAsyncEnumerable<TrackApiModel> GetTrackByPlaylistIdId(int id) => _playlistRepository.GetTrackByPlaylistId(id).ConvertAll();

        public async Task<TrackApiModel> AddTrack(TrackApiModel newTrackApiModel)
        {
            var track = newTrackApiModel.Convert();

            await _trackRepository.Add(track);
            newTrackApiModel.TrackId = track.TrackId;
            return newTrackApiModel;
        }

        public async Task<bool> UpdateTrack(TrackApiModel trackApiModel) => await _trackRepository.Update(trackApiModel.Convert());

        public async Task<bool> DeleteTrack(int id) => await _trackRepository.Delete(id);
    }
}