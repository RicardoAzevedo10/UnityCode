using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Covid19;

namespace RunCovid19
{
    class Program
    {
        private static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        private static async Task Run()
        {
            const string file = "covid-19.json";
            const string uri = "https://pomber.github.io/covid19/timeseries.json";
            var downloader = new Downloader(uri, file);
            var parser = new JsonParser<Dictionary<string, List<Sample>>>();
            Dictionary<string, List<Sample>> samples = null;

            try
            {
                samples = await downloader.After(24).Get(parser);
                var plot = new Plot(samples);
                plot.Confirmed().Deaths().Recovered();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}