using System;
using System.Collections.Generic;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<TrackApiModel> GetAllTrack()
        {
            var tracks = _trackRepository.GetAll().ConvertAll();
            foreach (var track in tracks)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Track-", track.TrackId), track, cacheEntryOptions);
            }
            return tracks;
        }

        public TrackApiModel GetTrackById(int id)
        {
            var trackApiModelCached = _cache.Get<TrackApiModel>(string.Concat("Track-", id));

            if (trackApiModelCached != null)
            {
                return trackApiModelCached;
            }
            else
            {
                var trackApiModel = (_trackRepository.GetById(id)).Convert();
                trackApiModel.Genre = GetGenreById(trackApiModel.GenreId.GetValueOrDefault());
                trackApiModel.Album = GetAlbumById(trackApiModel.AlbumId);
                trackApiModel.MediaType = GetMediaTypeById(trackApiModel.MediaTypeId);
                if (trackApiModel.Album != null)
                {
                    trackApiModel.AlbumName = trackApiModel.Album.Title;
                }
                trackApiModel.MediaTypeName = trackApiModel.MediaType.Name;
                if (trackApiModel.Genre != null)
                {
                    trackApiModel.GenreName = trackApiModel.Genre.Name;   
                }

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Track-", trackApiModel.TrackId), trackApiModel, cacheEntryOptions);

                return trackApiModel;
            }
        }

        public IEnumerable<TrackApiModel> GetTrackByAlbumId(int id)
        {
            var tracks = _trackRepository.GetByAlbumId(id);
            return tracks.ConvertAll();
        }

        public IEnumerable<TrackApiModel> GetTrackByGenreId(int id)
        {
            var tracks = _trackRepository.GetByGenreId(id);
            return tracks.ConvertAll();
        }

        public IEnumerable<TrackApiModel> GetTrackByMediaTypeId(int id)
        {
            var tracks = _trackRepository.GetByMediaTypeId(id);
            return tracks.ConvertAll();
        }

        public IEnumerable<TrackApiModel> GetTrackByPlaylistIdId(int id)
        {
            var tracks = _trackRepository.GetByPlaylistId(id);
            return tracks.ConvertAll();
        }

        public TrackApiModel AddTrack(TrackApiModel newTrackApiModel)
        {
            var track = newTrackApiModel.Convert();

            _trackRepository.Add(track);
            newTrackApiModel.TrackId = track.TrackId;
            return newTrackApiModel;
        }

        public bool UpdateTrack(TrackApiModel trackApiModel)
        {
            var track = _trackRepository.GetById(trackApiModel.TrackId);

            if (track == null) return false;
            track.TrackId = trackApiModel.TrackId;
            track.Name = trackApiModel.Name;
            track.AlbumId = trackApiModel.AlbumId;
            track.MediaTypeId = trackApiModel.MediaTypeId;
            track.GenreId = trackApiModel.GenreId;
            track.Composer = trackApiModel.Composer;
            track.Milliseconds = trackApiModel.Milliseconds;
            track.Bytes = trackApiModel.Bytes;
            track.UnitPrice = trackApiModel.UnitPrice;

            return _trackRepository.Update(track);
        }

        public bool DeleteTrack(int id) 
            => _trackRepository.Delete(id);
        
        public IEnumerable<TrackApiModel> GetTrackByArtistId(int id)
        {
            var tracks = _trackRepository.GetByArtistId(id);
            return tracks.ConvertAll();
        }

        public IEnumerable<TrackApiModel> GetTrackByInvoiceId(int id)
        {
            var tracks = _trackRepository.GetByInvoiceId(id);
            return tracks.ConvertAll();
        }
    }
}