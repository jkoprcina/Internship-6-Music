using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
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
                join album in albums
                    on albumSongConnection.AlbumId equals album.AlbumId
                join song in songs
                    on albumSongConnection.SongId equals song.SongId
                select new { album, song};

            foreach(var x in albumSong)
                Console.WriteLine($"{x.album.Name}, {x.song.Name}");

            foreach (var song in songs)
            {
                song.SongOnAlbums = albumSong.Where(albumS => albumS.song.SongId == song.SongId)
                    .Select(albumS => albumS.album).ToList();
            }

            foreach (var album in albums)
            {
                album.Songs = albumSong.Where(albumS => albumS.album.AlbumId == album.AlbumId)
                    .Select(albumS => albumS.song).ToList();
            }

            foreach (var album in albums)
            {
                foreach (var musician in musicians)
                {
                    if (musician.MusicianId == album.MusicianId)
                    {
                        musician.MusiciansAlbums.Add(album);
                        album.MusicianOnAlbum = musician;
                    }
                }
            }

            foreach (var musician in musicians)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            foreach (var album in albums)
                Console.WriteLine($"{album.Name} {album.YearOfRelease.Year}");

            foreach (var song in songs)
                Console.WriteLine($"{song.Name} {song.SongDuration}");

            //Write out all musicians ordered by name ascending 
            var orderByNameAscending = connection.Query<Musician>("SELECT * FROM Musician ORDER BY Name");

            Console.WriteLine("--- Musicians ordered by name ascending ---");
            foreach (var musician in orderByNameAscending)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            //Write out all musicians with a certain nationality
            var musiciansOfCertainNationality = connection.Query<Musician>("SELECT * FROM Musician WHERE Nationality = 'British'");

            Console.WriteLine("--- Musicians of a certain nationality ---");
            foreach (var musician in musiciansOfCertainNationality)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            //Write out all albums grouped by year of release ascending 

            var albumsByYear = albums.OrderBy(album => album.YearOfRelease);

            Console.WriteLine("--- Albums by year of release ---");
            foreach (var album in albumsByYear)
            {
                foreach (var musician in musicians)
                {
                    if(musician.MusicianId == album.MusicianId)
                        Console.WriteLine($"{album.Name} {album.YearOfRelease.Year} {musician.Name}");
                }
            }
            //All albums which contain certain text ( which will be hardcoded in the code and not asked of the user)

            var albumsWithText = connection.Query<Album>("SELECT * FROM Album WHERE Name LIKE '%w%'");

            Console.WriteLine("--- Albums that include a hardcoded text ('w' in this case) ---");
            foreach (var album in albumsWithText)
                Console.WriteLine($"{album.Name} {album.YearOfRelease.Year}");

            //All albums with their lengths ( lengths of all the songs on the album)

            //All albums with certain song

            var albumsWithSong = songs.Select(song => song.SongOnAlbums.Where(s => s.Name == "Dream of Mirrors")).SelectMany(x => x);

            Console.WriteLine("--- Albums that have a certain song ---");
            foreach (var album in albumsWithSong)
                Console.WriteLine($"{album.Name} {album.YearOfRelease.Year}");

        }
    }
}
