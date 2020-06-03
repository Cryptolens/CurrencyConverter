using System;
using System.Globalization;
using System.Net;
using System.Text;

namespace Cryptolens.CurrencyConverter
{
    public class CurrencyService
    {
        /// <summary>
        /// Returns the latest exchange rates of USD and EUR (in SEK)
        /// </summary>
        public static object GetSEKExchangeRates()
        {

            var today = DateTime.UtcNow;

            if(today.DayOfWeek == DayOfWeek.Saturday)
            {
                today = today.AddDays(-1);
            }
            if(today.DayOfWeek == DayOfWeek.Sunday)
            {
                today = today.AddDays(-2);
            }

            var result = new CurrencyResponse { };

            using (WebClient client = new WebClient())
            {
                client.Proxy = WebRequest.DefaultWebProxy;
                client.Proxy.Credentials = CredentialCache.DefaultCredentials;

                byte[] responsebytes = client.DownloadData($"https://www.riksbank.se/sv/statistik/sok-rantor--valutakurser/?c=cAverage&f=Day&from={today.ToShortDateString()}&g130-SEKEURPMI=on&g130-SEKUSDPMI=on&s=Comma&to=&export=txt");
                string responsebody = Encoding.UTF8.GetString(responsebytes);


                var data = responsebody.Split('\n');
                foreach (var row in data)
                {
                    var columns = row.Split('\t');
                    if(columns[0] == today.ToShortDateString())
                    {
                        if (columns[2].Contains("EUR"))
                        {
                            result.EUR = Convert.ToDouble(columns[3], CultureInfo.GetCultureInfo("sv-SE"));
                        }
                        else if (columns[2].Contains("USD"))
                        {
                            result.USD = Convert.ToDouble(columns[3], CultureInfo.GetCultureInfo("sv-SE"));
                        }
                    }
                }


            }

            return result;

        }
    }

    public class CurrencyResponse
    {
        public double? USD { get; set; }
        public double? EUR { get; set; }
    }
}
