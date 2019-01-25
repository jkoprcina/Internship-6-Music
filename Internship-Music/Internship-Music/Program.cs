using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Internship_Music
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MUSIC;Integrated Security=true;MultipleActiveResultSets=true";

            using (var connnection = new SqlConnection(connectionString))
            {

            }
        }
    }
}
