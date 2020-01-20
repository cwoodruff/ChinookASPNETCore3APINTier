using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;

namespace Chinook.Domain.Supervisor
{
    public interface IChinookSupervisor
    {
        Task<IAsyncEnumerable<AlbumApiModel>> GetAllAlbum();
        Task<AlbumApiModel> GetAlbumById(int id);
        Task<IAsyncEnumerable<AlbumApiModel>> GetAlbumByArtistId(int id);

        Task<AlbumApiModel> AddAlbum(AlbumApiModel newAlbumApiModel);

        Task<bool> UpdateAlbum(AlbumApiModel albumApiModel);
        Task<bool> DeleteAlbum(int id);
        Task<IAsyncEnumerable<ArtistApiModel>> GetAllArtist();
        Task<ArtistApiModel> GetArtistById(int id);

        Task<ArtistApiModel> AddArtist(ArtistApiModel newArtistApiModel);

        Task<bool> UpdateArtist(ArtistApiModel artistApiModel);

        Task<bool> DeleteArtist(int id);
        Task<IAsyncEnumerable<CustomerApiModel>> GetAllCustomer();
        Task<CustomerApiModel> GetCustomerById(int id);

        Task<IAsyncEnumerable<CustomerApiModel>> GetCustomerBySupportRepId(int id);

        Task<CustomerApiModel> AddCustomer(CustomerApiModel newCustomerApiModel);

        Task<bool> UpdateCustomer(CustomerApiModel customerApiModel);

        Task<bool> DeleteCustomer(int id);
        Task<IAsyncEnumerable<EmployeeApiModel>> GetAllEmployee();
        Task<EmployeeApiModel> GetEmployeeById(int id);
        Task<EmployeeApiModel> GetEmployeeReportsTo(int id);

        Task<EmployeeApiModel> AddEmployee(EmployeeApiModel newEmployeeApiModel);

        Task<bool> UpdateEmployee(EmployeeApiModel employeeApiModel);

        Task<bool> DeleteEmployee(int id);

        Task<IAsyncEnumerable<EmployeeApiModel>> GetEmployeeDirectReports(int id);

        Task<IAsyncEnumerable<EmployeeApiModel>> GetDirectReports(int id);
        Task<IAsyncEnumerable<GenreApiModel>> GetAllGenre();
        Task<GenreApiModel> GetGenreById(int id);

        Task<GenreApiModel> AddGenre(GenreApiModel newGenreApiModel);

        Task<bool> UpdateGenre(GenreApiModel genreApiModel);
        Task<bool> DeleteGenre(int id);
        Task<IAsyncEnumerable<InvoiceLineApiModel>> GetAllInvoiceLine();
        Task<InvoiceLineApiModel> GetInvoiceLineById(int id);

        Task<IAsyncEnumerable<InvoiceLineApiModel>> GetInvoiceLineByInvoiceId(int id);

        Task<IAsyncEnumerable<InvoiceLineApiModel>> GetInvoiceLineByTrackId(int id);

        Task<InvoiceLineApiModel> AddInvoiceLine(InvoiceLineApiModel newInvoiceLineApiModel);

        Task<bool> UpdateInvoiceLine(InvoiceLineApiModel invoiceLineApiModel);

        Task<bool> DeleteInvoiceLine(int id);
        Task<IAsyncEnumerable<InvoiceApiModel>> GetAllInvoice();
        Task<InvoiceApiModel> GetInvoiceById(int id);

        Task<IAsyncEnumerable<InvoiceApiModel>> GetInvoiceByCustomerId(int id);

        Task<InvoiceApiModel> AddInvoice(InvoiceApiModel newInvoiceApiModel);

        Task<bool> UpdateInvoice(InvoiceApiModel invoiceApiModel);

        Task<bool> DeleteInvoice(int id);
        Task<IAsyncEnumerable<MediaTypeApiModel>> GetAllMediaType();
        Task<MediaTypeApiModel> GetMediaTypeById(int id);

        Task<MediaTypeApiModel> AddMediaType(MediaTypeApiModel newMediaTypeApiModel);

        Task<bool> UpdateMediaType(MediaTypeApiModel mediaTypeApiModel);

        Task<bool> DeleteMediaType(int id);
        Task<IAsyncEnumerable<PlaylistApiModel>> GetAllPlaylist();
        Task<PlaylistApiModel> GetPlaylistById(int id);

        Task<PlaylistApiModel> AddPlaylist(PlaylistApiModel newPlaylistApiModel);

        Task<bool> UpdatePlaylist(PlaylistApiModel playlistApiModel);

        Task<bool> DeletePlaylist(int id);
        Task<IAsyncEnumerable<TrackApiModel>> GetAllTrack();
        Task<TrackApiModel> GetTrackById(int id);
        Task<IAsyncEnumerable<TrackApiModel>> GetTrackByAlbumId(int id);
        Task<IAsyncEnumerable<TrackApiModel>> GetTrackByGenreId(int id);

        Task<IAsyncEnumerable<TrackApiModel>> GetTrackByMediaTypeId(int id);

        IAsyncEnumerable<TrackApiModel> GetTrackByPlaylistIdId(int id);

        Task<TrackApiModel> AddTrack(TrackApiModel newTrackApiModel);

        Task<bool> UpdateTrack(TrackApiModel trackApiModel);
        Task<bool> DeleteTrack(int id);

        public bool AlbumExists(int id);
        public bool ArtistExists(int id);
        public bool CustomerExists(int id);
        public bool EmployeeExists(int id);
        public bool GenreExists(int id);
        public bool InvoiceExists(int id);
        public bool InvoiceLineExists(int id);
        public bool MediaTypeExists(int id);
        public bool PlaylistExists(int id);
        public bool TrackExists(int id);
    }
}