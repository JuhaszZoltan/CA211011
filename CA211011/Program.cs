using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA211011
{
    class Program
    {
        static void Main()
        {
            string connectionString = 
                @"Server=(localdb)\MSSQLLocalDB;" +
                "Database=teszt;";

            var connection = new SqlConnection(connectionString);
            connection.Open();

            var sqlCommand = new SqlCommand("SELECT * FROM emberek;", connection);
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Console.WriteLine(
                    $"{sqlDataReader.GetInt32(0) * 10} " +
                    $"{sqlDataReader["nev"]} " +
                    $"{sqlDataReader[2]}");
            }

            sqlDataReader.Close();

            //-----------------
            Console.Write("új név: ");
            string ujNev = Console.ReadLine();
            Console.Write("új telefonszám: ");
            string ujTelefonszam = Console.ReadLine();

            sqlCommand = new SqlCommand(
                $"INSERT INTO emberek " +
                $"VALUES ('{ujNev}', '{ujTelefonszam}');",
                connection);

            var sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.InsertCommand = sqlCommand;
            sqlDataAdapter.InsertCommand.ExecuteNonQuery();

            Console.WriteLine("megcsinmáltam ge!");

            connection.Close();
            Console.ReadKey();
        }
    }
}
