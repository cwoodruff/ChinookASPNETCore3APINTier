using System.Collections.Generic;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Supervisor
{
    public interface IChinookSupervisor
    {
        IEnumerable<AlbumApiModel> GetAllAlbum();
        AlbumApiModel GetAlbumById(int id);
        IEnumerable<AlbumApiModel> GetAlbumByArtistId(int id);

        AlbumApiModel AddAlbum(AlbumApiModel newAlbumApiModel);

        bool UpdateAlbum(AlbumApiModel albumApiModel);
        bool DeleteAlbum(int id);
        IEnumerable<ArtistApiModel> GetAllArtist();
        ArtistApiModel GetArtistById(int id);

        ArtistApiModel AddArtist(ArtistApiModel newArtistApiModel);

        bool UpdateArtist(ArtistApiModel artistApiModel);

        bool DeleteArtist(int id);
        IEnumerable<CustomerApiModel> GetAllCustomer();
        CustomerApiModel GetCustomerById(int id);

        IEnumerable<CustomerApiModel> GetCustomerBySupportRepId(int id);

        CustomerApiModel AddCustomer(CustomerApiModel newCustomerApiModel);

        bool UpdateCustomer(CustomerApiModel customerApiModel);

        bool DeleteCustomer(int id);
        IEnumerable<EmployeeApiModel> GetAllEmployee();
        EmployeeApiModel GetEmployeeById(int id);
        EmployeeApiModel GetEmployeeReportsTo(int id);

        EmployeeApiModel AddEmployee(EmployeeApiModel newEmployeeApiModel);

        bool UpdateEmployee(EmployeeApiModel employeeApiModel);

        bool DeleteEmployee(int id);

        IEnumerable<EmployeeApiModel> GetEmployeeDirectReports(int id);

        IEnumerable<EmployeeApiModel> GetDirectReports(int id);
        IEnumerable<GenreApiModel> GetAllGenre();
        GenreApiModel GetGenreById(int id);

        GenreApiModel AddGenre(GenreApiModel newGenreApiModel);

        bool UpdateGenre(GenreApiModel genreApiModel);
        bool DeleteGenre(int id);
        IEnumerable<InvoiceLineApiModel> GetAllInvoiceLine();
        InvoiceLineApiModel GetInvoiceLineById(int id);

        IEnumerable<InvoiceLineApiModel> GetInvoiceLineByInvoiceId(int id);

        IEnumerable<InvoiceLineApiModel> GetInvoiceLineByTrackId(int id);

        InvoiceLineApiModel AddInvoiceLine(InvoiceLineApiModel newInvoiceLineApiModel);

        bool UpdateInvoiceLine(InvoiceLineApiModel invoiceLineApiModel);

        bool DeleteInvoiceLine(int id);
        IEnumerable<InvoiceApiModel> GetAllInvoice();
        InvoiceApiModel GetInvoiceById(int id);

        IEnumerable<InvoiceApiModel> GetInvoiceByCustomerId(int id);

        InvoiceApiModel AddInvoice(InvoiceApiModel newInvoiceApiModel);

        bool UpdateInvoice(InvoiceApiModel invoiceApiModel);

        bool DeleteInvoice(int id);
        IEnumerable<MediaTypeApiModel> GetAllMediaType();
        MediaTypeApiModel GetMediaTypeById(int id);

        MediaTypeApiModel AddMediaType(MediaTypeApiModel newMediaTypeApiModel);

        bool UpdateMediaType(MediaTypeApiModel mediaTypeApiModel);

        bool DeleteMediaType(int id);
        IEnumerable<PlaylistApiModel> GetAllPlaylist();
        PlaylistApiModel GetPlaylistById(int id);

        PlaylistApiModel AddPlaylist(PlaylistApiModel newPlaylistApiModel);

        bool UpdatePlaylist(PlaylistApiModel playlistApiModel);

        bool DeletePlaylist(int id);
        IEnumerable<TrackApiModel> GetAllTrack();
        TrackApiModel GetTrackById(int id);
        IEnumerable<TrackApiModel> GetTrackByAlbumId(int id);
        IEnumerable<TrackApiModel> GetTrackByGenreId(int id);

        IEnumerable<TrackApiModel>
            GetTrackByMediaTypeId(int id);

        IEnumerable<TrackApiModel> GetTrackByPlaylistIdId(int id);

        TrackApiModel AddTrack(TrackApiModel newTrackApiModel);

        bool UpdateTrack(TrackApiModel trackApiModel);
        bool DeleteTrack(int id);
    }
}