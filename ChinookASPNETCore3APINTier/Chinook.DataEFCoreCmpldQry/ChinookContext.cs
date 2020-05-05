using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Chinook.DataEFCoreCmpldQry.Configurations;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCoreCmpldQry
{
    public class ChinookContext : DbContext
    {
        public static long InstanceCount;

        private static readonly Func<ChinookContext, List<Album>> _queryGetAllAlbums =
            EF.CompileQuery((ChinookContext db) =>
                db.Album.ToList());

        private static readonly Func<ChinookContext, int, Album> _queryGetAlbum =
            EF.CompileQuery((ChinookContext db, int id) => db.Album.FirstOrDefault(a => a.AlbumId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<Album>> _queryGetAlbumsByArtistId =
            EF.CompileQuery((ChinookContext db, int id) => db.Album.Where(a => a.ArtistId == id));

        private static readonly Func<ChinookContext, List<Artist>> _queryGetAllArtists =
            EF.CompileQuery((ChinookContext db) =>
                db.Artist.ToList());

        private static readonly Func<ChinookContext, int, Artist> _queryGetArtist =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Artist.FirstOrDefault(a => a.ArtistId == id));

        private static readonly Func<ChinookContext, List<Customer>> _queryGetAllCustomers =
            EF.CompileQuery((ChinookContext db) =>
                db.Customer.ToList());

        private static readonly Func<ChinookContext, int, Customer> _queryGetCustomer =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Customer.FirstOrDefault(c => c.CustomerId == id)); 

        private static readonly Func<ChinookContext, int, IEnumerable<Customer>> _queryGetCustomerBySupportRepId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Customer.Where(a => a.SupportRepId == id));

        private static readonly Func<ChinookContext, List<Employee>> _queryGetAllEmployees =
            EF.CompileQuery((ChinookContext db) =>
                db.Employee.ToList());

        private static readonly Func<ChinookContext, int, Employee> _queryGetEmployee =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Employee.FirstOrDefault(e => e.EmployeeId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<Employee>> _queryGetDirectReports =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.ReportsTo == id));

        private static readonly Func<ChinookContext, int, IEnumerable<Employee>> _queryGetReportsTo =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.ReportsTo == id));

        private static readonly Func<ChinookContext, List<Genre>> _queryGetAllGenres =
            EF.CompileQuery((ChinookContext db) =>
                db.Genre.ToList());

        private static readonly Func<ChinookContext, int, Genre> _queryGetGenre =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Genre.FirstOrDefault(g => g.GenreId == id));

        private static readonly Func<ChinookContext, List<InvoiceLine>> _queryGetAllInvoiceLines =
            EF.CompileQuery((ChinookContext db) =>
                db.InvoiceLine.ToList());

        private static readonly Func<ChinookContext, int, InvoiceLine> _queryGetInvoiceLine =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.InvoiceLine.FirstOrDefault(i => i.InvoiceLineId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<InvoiceLine>> _queryGetInvoiceLinesByInvoiceId
            = EF.CompileQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(a => a.InvoiceId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<InvoiceLine>> _queryGetInvoiceLinesByTrackId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(a => a.TrackId == id));

        private static readonly Func<ChinookContext, List<Invoice>> _queryGetAllInvoices =
            EF.CompileQuery((ChinookContext db) =>
                db.Invoice.ToList());

        private static readonly Func<ChinookContext, int, Invoice> _queryGetInvoice =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Invoice.FirstOrDefault(i => i.InvoiceId == id)); 

        private static readonly Func<ChinookContext, int, IEnumerable<Invoice>> _queryGetInvoicesByCustomerId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Invoice.Where(a => a.CustomerId == id));

        private static readonly Func<ChinookContext, List<MediaType>> _queryGetAllMediaTypes =
            EF.CompileQuery((ChinookContext db) =>
                db.MediaType.ToList());

        private static readonly Func<ChinookContext, int, MediaType> _queryGetMediaType =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.MediaType.FirstOrDefault(m => m.MediaTypeId == id));

        private static readonly Func<ChinookContext, List<Playlist>> _queryGetAllPlaylists =
            EF.CompileQuery((ChinookContext db) =>
                db.Playlist.ToList());

        private static readonly Func<ChinookContext, int, Playlist> _queryGetPlaylist =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Playlist.FirstOrDefault(p => p.PlaylistId == id));

        private static readonly Func<ChinookContext, List<PlaylistTrack>> _queryGetAllPlaylistTracks =
            EF.CompileQuery((ChinookContext db) =>
                db.PlaylistTrack.ToList());

        private static readonly Func<ChinookContext, int, IEnumerable<PlaylistTrack>> _queryGetPlaylistTrackByPlaylistId
            = EF.CompileQuery((ChinookContext db, int id) =>
                db.PlaylistTrack.Where(a => a.PlaylistId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<PlaylistTrack>> _queryGetPlaylistTracksByTrackId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.PlaylistTrack.Where(a => a.TrackId == id));

        private static readonly Func<ChinookContext, List<Track>> _queryGetAllTracks =
            EF.CompileQuery((ChinookContext db) =>
                db.Track.ToList());

        private static readonly Func<ChinookContext, int, Track> _queryGetTrack =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.FirstOrDefault(t => t.TrackId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<Track>> _queryGetTracksByAlbumId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.AlbumId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<Track>> _queryGetTracksByGenreId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.GenreId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<Track>> _queryGetTracksByMediaTypeId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.MediaTypeId == id));

        private static readonly Func<ChinookContext, int, IEnumerable<Track>> _queryGetTracksByArtistId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Album.Where(a => a.ArtistId == 5).SelectMany(t => t.Tracks));

        private static readonly Func<ChinookContext, int, IEnumerable<Track>> _queryGetTracksByInvoiceId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track
                    .Where(c => c.InvoiceLines.Any(o => o.InvoiceId == id)));

        private static readonly Func<ChinookContext, int, IEnumerable<Invoice>> _queryGetInvoicesByEmployeeId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Customer.Where(a => a.SupportRepId == 5).SelectMany(t => t.Invoices));
        
        private static readonly Func<ChinookContext, int, IEnumerable<Playlist>> _queryGetPlaylistByTrackId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Playlist
                    .Where(c => c.PlaylistTracks.Any(o => o.TrackId == id)));

        public ChinookContext(DbContextOptions options) : base(options)
        {
            Interlocked.Increment(ref InstanceCount);
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLine { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTrack { get; set; }
        public virtual DbSet<Track> Track { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AlbumConfiguration(modelBuilder.Entity<Album>());
            new ArtistConfiguration(modelBuilder.Entity<Artist>());
            new CustomerConfiguration(modelBuilder.Entity<Customer>());
            new EmployeeConfiguration(modelBuilder.Entity<Employee>());
            new GenreConfiguration(modelBuilder.Entity<Genre>());
            new InvoiceConfiguration(modelBuilder.Entity<Invoice>());
            new InvoiceLineConfiguration(modelBuilder.Entity<InvoiceLine>());
            new MediaTypeConfiguration(modelBuilder.Entity<MediaType>());
            new PlaylistConfiguration(modelBuilder.Entity<Playlist>());
            new PlaylistTrackConfiguration(modelBuilder.Entity<PlaylistTrack>());
            new TrackConfiguration(modelBuilder.Entity<Track>());
        }

        public List<Album> GetAllAlbums() => _queryGetAllAlbums(this);

        public Album GetAlbum(int id) => _queryGetAlbum(this, id);

        public List<Album> GetAlbumsByArtistId(int id) =>
            _queryGetAlbumsByArtistId(this, id).ToList();

        public List<Artist> GetAllArtists() => _queryGetAllArtists(this);

        public Artist GetArtist(int id) => _queryGetArtist(this, id);

        public List<Customer> GetAllCustomers() => _queryGetAllCustomers(this);

        public Customer GetCustomer(int id) => _queryGetCustomer(this, id);

        public List<Customer> GetCustomerBySupportRepId(int id) =>
            _queryGetCustomerBySupportRepId(this, id).ToList();

        public List<Employee> GetAllEmployees() => _queryGetAllEmployees(this);

        public Employee GetEmployee(int id) => _queryGetEmployee(this, id);

        public List<Employee> GetEmployeeDirectReports(int id) =>
            _queryGetDirectReports(this, id).ToList();

        public List<Employee> GetEmployeeGetReportsTo(int id) =>
            _queryGetReportsTo(this, id).ToList();

        public List<Genre> GetAllGenres() => _queryGetAllGenres(this);

        public Genre GetGenre(int id) => _queryGetGenre(this, id);

        public List<InvoiceLine> GetAllInvoiceLines() =>
            _queryGetAllInvoiceLines(this);

        public InvoiceLine GetInvoiceLine(int id) =>
            _queryGetInvoiceLine(this, id);

        public List<InvoiceLine> GetInvoiceLinesByInvoiceId(int id) =>
            _queryGetInvoiceLinesByInvoiceId(this, id).ToList();

        public List<InvoiceLine> GetInvoiceLinesByTrackId(int id) =>
            _queryGetInvoiceLinesByTrackId(this, id).ToList();

        public List<Invoice> GetAllInvoices() => _queryGetAllInvoices(this);

        public Invoice GetInvoice(int id) => _queryGetInvoice(this, id);

        public List<Invoice> GetInvoicesByCustomerId(int id) =>
            _queryGetInvoicesByCustomerId(this, id).ToList();

        public List<MediaType> GetAllMediaTypes() => _queryGetAllMediaTypes(this);

        public MediaType GetMediaType(int id) =>
            _queryGetMediaType(this, id);

        public List<Playlist> GetAllPlaylists() => _queryGetAllPlaylists(this);

        public Playlist GetPlaylist(int id) => _queryGetPlaylist(this, id);

        public List<PlaylistTrack> GetAllPlaylistTracks() =>
            _queryGetAllPlaylistTracks(this);

        public List<PlaylistTrack> GetPlaylistTrackByPlaylistId(int id) =>
            _queryGetPlaylistTrackByPlaylistId(this, id).ToList();

        public List<PlaylistTrack> GetPlaylistTracksByTrackId(int id) =>
            _queryGetPlaylistTracksByTrackId(this, id).ToList();

        public List<Track> GetAllTracks() => _queryGetAllTracks(this);

        public Track GetTrack(int id) => _queryGetTrack(this, id);

        public List<Track> GetTracksByAlbumId(int id) =>
            _queryGetTracksByAlbumId(this, id).ToList();

        public List<Track> GetTracksByGenreId(int id) =>
            _queryGetTracksByGenreId(this, id).ToList();

        public List<Track> GetTracksByMediaTypeId(int id) =>
            _queryGetTracksByMediaTypeId(this, id).ToList();
        
        public List<Track> GetTracksByArtistId(int id) =>
            _queryGetTracksByArtistId(this, id).ToList();

        public List<Track> GetTracksByInvoiceId(int id) =>
            _queryGetTracksByInvoiceId(this, id).ToList();
        
        public List<Invoice> GetInvoicesByEmployeeId(int id) =>
            _queryGetInvoicesByEmployeeId(this, id).ToList();
        
        public List<Playlist> GetPlaylistByTrackId(int id) =>
            _queryGetPlaylistByTrackId(this, id).ToList();
    }
}