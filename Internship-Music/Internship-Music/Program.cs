using System;
using System.Data.Common;
using System.Data.SqlClient;
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

            foreach(var musician in musicians)
                Console.WriteLine($"{musician.Name} {musician.Nationality}");

            foreach (var album in albums)
                Console.WriteLine($"{album.Name} {album.YearOfRelease}");

            foreach (var song in songs)
                Console.WriteLine($"{song.Name} {song.SongLength}");

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
        }
    }
}
