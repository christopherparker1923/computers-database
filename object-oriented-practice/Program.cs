using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using object_oriented_practice.Data;
using object_oriented_practice.Models;
using System.Data;
using System.Text.Json;

namespace HelloWorld
{

    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            string computersJSON = File.ReadAllText("Computers.json");

            //Console.WriteLine(computersJSON);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            IEnumerable<Computer>? computers = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJSON, options);

            if(computers != null)
            {
                foreach(Computer computer in computers)
                {
                    Console.WriteLine(computer.Motherboard);
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (
                        Motherboard,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard
                        ) VALUES ('"
                        + EscapeSingleQuote(computer.Motherboard)
                        + "','" + computer.HasWifi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate
                        + "','" + computer.Price
                        + "','" + EscapeSingleQuote(computer.VideoCard)
                        + "')";

                    dapper.ExecuteSql(sql);
                }
            }

            string computersCopy = System.Text.Json.JsonSerializer.Serialize(computers, options);

            File.WriteAllText("computersCopySystem.txt", computersCopy);


        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }
    }
}


