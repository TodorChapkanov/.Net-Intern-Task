using Json_Converter.MapModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MENTOTMATE.Net_Intern_Task
{
    public class StartUp
    {
        private const string PathPrefix = "$@";
        public static void Main(string[] args)
        {
            Console.WriteLine("Pleas type the full File Path");
            var inputFilePath = Console.ReadLine();

            Console.WriteLine("Pleas type maximum number of years in league");
            var maxYears = int.Parse(Console.ReadLine());

            Console.WriteLine("Pleas type minimum rating the player should have");
            var minRating = int.Parse(Console.ReadLine());

            Console.WriteLine("Please type the output File Path");
            var outputFilePath = Console.ReadLine();

            var players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText($@"{inputFilePath}"));
            var currentYear = DateTime.UtcNow.Year;


            players
                .ForEach(years =>
                years.PlayngSince =
                (currentYear - years.PlayngSince));

            using (var writher = new StreamWriter($@"{outputFilePath}"))
            {
                writher.WriteLine("Name, Rating");

                foreach (var player in players
                .Where(player => player.Rating >= minRating
                && player.PlayngSince <= maxYears).OrderByDescending(player => player.Rating))
                {
                    writher.WriteLine($"{player.Name}, {player.Rating}");
                }
            }
        }
    }
}
