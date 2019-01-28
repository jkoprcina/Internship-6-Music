using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Dapper;

namespace Internship_Music
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MUSIC;Integrated Security=true;MultipleActiveResultSets=true";

            var connection = new SqlConnection(connectionString);
            var musicians = connection.Query<Musician>("SELECT * FROM Musician");
            var albums = connection.Query<Album>("SELECT * FROM Album");
            var songs = connection.Query<Song>("SELECT * FROM Song");
            var albumSongConnections = connection.Query<AlbumSong>("SELECT * FROM AlbumSong");

            var albumSong = from albumSongConnection in albumSongConnections
                join album in albums on albumSongConnection.AlbumId equals album.AlbumId
                join song in songs on albumSongConnection.SongId equals song.SongId
                select new { album, song};

            foreach(var albSong in albumSong)
                Console.WriteLine($"{albSong.album.Name}, {albSong.song.Name}");

            foreach (var musician in musicians)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            foreach (var album in albums)
                Console.WriteLine($"{album.Name} {album.YearOfRelease.Year}");

            foreach (var song in songs)
                Console.WriteLine($"{song.Name} {song.SongDuration}");

            //Write out all musicians ordered by name ascending 
            var orderByNameAscending = musicians.OrderBy(musician => musician.Name);

            Console.WriteLine("--- Musicians ordered by name ascending ---");
            foreach (var musician in orderByNameAscending)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            //Write out all musicians with a certain nationality hardcoded to "British"
            Console.WriteLine("Enter a nationality: ");
            var nationality = Console.ReadLine();
            Console.WriteLine("--- Musicians of a certain nationality ---");
            foreach (var musician in musicians.Where(musician => musician.Nationality == nationality))
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            //Write out all albums grouped by year of release ascending 

            var albumMusicians = from album in albums
                join musician in musicians on album.MusicianId equals musician.MusicianId
                select new { album.Name, album.YearOfRelease, NameOfMusician = musician.Name };

            Console.WriteLine("--- Albums by year of release ---");
            foreach (var album in albumMusicians.OrderBy(albMus => albMus.YearOfRelease))
                 Console.WriteLine($"{album.Name} {album.YearOfRelease.Year} {album.NameOfMusician}");

            //All albums which contain certain text ( which will be hardcoded in the code and not asked of the user)

            Console.WriteLine("--- Albums that include a hardcoded text ---");
            Console.WriteLine("Enter a text which should be in one of the albums: ");
            var text = Console.ReadLine();
            foreach (var album in albums.Where(album => album.Name.Contains(text)))
                Console.WriteLine($"{album.Name}");

            //All albums with their lengths ( lengths of all the songs on the album)
            Console.WriteLine("--- Albums with their lengths ---");
            foreach (var album in albumSong.GroupBy(album => album.album.Name))
            {
                var albumLength = new TimeSpan(0,0,0);
                foreach (var length in album)
                    albumLength += length.song.SongDuration;
                Console.WriteLine($"{ album.Key} {albumLength}");
            }

            //All albums with a certain song  
            Console.WriteLine("Enter one of the songs in the database (Innuendo is the only song that's entered twice): ");
            var songWeAreLookingFor = Console.ReadLine();
            
            foreach (var album in albumSong)
            {
                if(album.song.Name == songWeAreLookingFor)
                    Console.WriteLine(album.album.Name);
            }

            //All songs by a certain musician that came out after a certain year
            Console.WriteLine("Enter a band from the database: ");
            var musicianWeAreLookingFor = Console.ReadLine();
            Console.WriteLine("Enter a year: ");
            var year = int.Parse(Console.ReadLine());

            Console.WriteLine("--- All songs by a certain artist after a certain year ---");
            var songsAfterYearByMusician = from song in songs
                join conn in albumSongConnections on song.SongId equals conn.SongId
                join album in albums on conn.AlbumId equals album.AlbumId
                join musician in musicians on album.MusicianId equals musician.MusicianId
                where album.YearOfRelease.Year > year && musician.Name == musicianWeAreLookingFor
                select new {Musician = musician.Name, song.Name, album.YearOfRelease};

            foreach (var musician in songsAfterYearByMusician.GroupBy(song => song.Musician))
            {
                foreach(var song in musician)
                    Console.WriteLine($"{song.Name} {song.YearOfRelease.Year}");
            }
        }
    }
}
