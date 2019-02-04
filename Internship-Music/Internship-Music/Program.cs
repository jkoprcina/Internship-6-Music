using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            var musicians = connection.Query<Musician>("SELECT * FROM Musician").ToList();
            var albums = connection.Query<Album>("SELECT * FROM Album").ToList();
            var songs = connection.Query<Song>("SELECT * FROM Song").ToList();
            var albumSongConnections = connection.Query<AlbumSong>("SELECT * FROM AlbumSong").ToList();
            foreach (var musician in musicians)
            {
                foreach (var album in albums)
                {
                    if(album.MusicianId == musician.MusicianId)
                        musician.Albums.Add(album);
                }
            }

            foreach (var album in albums)
            {
                foreach (var song in songs)
                {
                    foreach (var albSon in albumSongConnections)
                    {
                        if (album.AlbumId != albSon.AlbumId || song.SongId != albSon.SongId) continue;
                        album.Songs.Add(song);
                        song.Albums.Add(album);
                    }
                }
            }

            foreach (var musician in musicians)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            foreach (var album in albums)
            {
                foreach(var song in album.Songs)
                    Console.WriteLine($"{album.Name} {song.Name} {album.YearOfRelease.Year}");
            }

            //Write out all musicians ordered by name ascending 
            var orderByNameAscending = musicians.OrderBy(musician => musician.Name);

            Console.WriteLine("--- Musicians ordered by name ascending ---");
            foreach (var musician in orderByNameAscending)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            //Write out all musicians with a certain nationality 
            Console.WriteLine("Enter a nationality: ");
            var nationality = Console.ReadLine();
            Console.WriteLine("--- Musicians of a certain nationality ---");
            foreach (var musician in musicians.Where(musician => musician.Nationality == nationality))
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            //Write out all albums grouped by year of release ascending 
            Console.WriteLine("--- All albums ordered by year of release ascending ---");
            foreach (var album in albums.OrderBy(album => album.YearOfRelease))
            {
                foreach (var musician in musicians)
                {
                    if(album.MusicianId == musician.MusicianId)
                        Console.WriteLine($"{musician.Name} {album.Name} {album.YearOfRelease.Year}");
                }
            }
            //All albums which contain certain text ( which will be hardcoded in the code and not asked of the user)

            Console.WriteLine("--- Albums that include a hardcoded text ---");
            Console.WriteLine("Enter a text which should be in one of the albums: ");
            var text = Console.ReadLine();
            foreach (var album in albums.Where(album => album.Name.Contains(text)))
                Console.WriteLine($"{album.Name}");

            //All albums with their lengths ( lengths of all the songs on the album)
            Console.WriteLine("--- Albums with their lengths ---");
            foreach (var album in albums.OrderBy(album => album.Name))
            {
                var albumLength = new TimeSpan(0,0,0);
                foreach (var song in album.Songs)
                    albumLength += song.SongDuration;
                Console.WriteLine($"{album.Name} {albumLength}");
            }

            //All albums with a certain song  
            Console.WriteLine("Enter one of the songs in the database (Innuendo is the only song that's entered twice): ");
            var songStringWeAreLookingFor = Console.ReadLine();

            Console.WriteLine("--- All albums that have the input song on them ---");
            List<Album> albumsWithSongWeAreLookingFor = new List<Album>();
            foreach (var album in albums)
            {
                foreach(var song in album.Songs)
                    if(song.Name == songStringWeAreLookingFor)
                        albumsWithSongWeAreLookingFor.Add(album);
            }

            foreach (var album in albumsWithSongWeAreLookingFor)
                Console.WriteLine(album.Name);

            //All songs by a certain musician that came out after a certain year
            Console.WriteLine("Enter a band from the database: ");
            var musicianWeAreLookingForAsString = Console.ReadLine();
            var musicianWeAreLookingForAsObject = musicians.FirstOrDefault(musician => musician.Name == musicianWeAreLookingForAsString);
            Console.WriteLine("Enter a year: ");
            var year = int.Parse(Console.ReadLine());
            var albumsOfMusicianWeAreLookingFor = musicianWeAreLookingForAsObject.Albums;

            Console.WriteLine("--- All songs by a certain artist after a certain year ---");
            foreach (var album in albumsOfMusicianWeAreLookingFor)
            {
                foreach (var song in album.Songs)
                {
                    if(album.YearOfRelease.Year > year)
                        Console.WriteLine($"{musicianWeAreLookingForAsObject.Name} {album.Name} {album.YearOfRelease.Year} {song.Name}");
                }
            }
        }
    }
}
