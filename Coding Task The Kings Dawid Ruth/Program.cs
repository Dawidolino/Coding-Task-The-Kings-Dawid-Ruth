using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace Coding_Task_The_Kings_Dawid_Ruth
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var monarchs = await FetchMonarchs("https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings");          
            AnalyzeMonarchs(monarchs);
        }
        private static async Task<List<Monarchs>> FetchMonarchs(string url)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Monarchs>>(json);
        }
        private static void AnalyzeMonarchs(List<Monarchs> monarchs)
        {
            foreach (var monarch in monarchs)
            {
                monarch.AssignReignYears();
                //Console.WriteLine($"Monarch: {monarch.Name}, ReignStart: {monarch.ReignStart}, ReignEnd: {monarch.ReignEnd}");

            }
            //1.
            int monarchCount = monarchs.Count;
            Console.WriteLine($"Number of monarchs is {monarchCount} ");

            //2.
            var longestReigningMonarch = monarchs
                .Select(m => new { Monarch = m, ReignYears = m.CountReignYears() })
                .OrderByDescending(m => m.ReignYears)
                .FirstOrDefault();

            Console.WriteLine($"Longest reigning monarch is {longestReigningMonarch.Monarch.Name} with {longestReigningMonarch.ReignYears} years of reign");
            //3.
            var houseReignYears = monarchs
                .GroupBy(m => m.House)
                .Select(group => new
                {
                    House = group.Key,
                    TotalHouseReignYears = group.Sum(m => m.CountReignYears())
                })
                .OrderByDescending(h => h.TotalHouseReignYears)
                .FirstOrDefault();
            if (houseReignYears != null)
            {
                Console.WriteLine($"House with the most total reign years is {houseReignYears.House} with {houseReignYears.TotalHouseReignYears}");
            }
            //4.
            var mostCommonFirstName = monarchs
                .Select(m => m.Name.Split(' ')[0])
                .GroupBy(name => name)
                .Select(group => new
                {
                    firstName = group.Key,
                    count = group.Count()
                })
                .OrderByDescending(x=>x.count)
                .FirstOrDefault();
            if(mostCommonFirstName != null)
            {
                Console.WriteLine($"Most common name of British monarch is {mostCommonFirstName.firstName} with the count of {mostCommonFirstName.count}");
            }

            Console.ReadLine();
        }
    }
}
