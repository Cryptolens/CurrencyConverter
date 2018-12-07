using System;

namespace Cryptolens.CurrencyConverter
{
    public class CurrencyService
    {
        /// <summary>
        /// Returns the latest exchange rates of USD and EUR (in SEK)
        /// </summary>
        public static object GetSEKExchangeRates()
        {
            var client = new ExRateAPI.SweaWebServicePortTypeClient();
            var res = client.getLatestInterestAndExchangeRatesAsync(ExRateAPI.LanguageType.en, new string[] { "SEKUSDPMI", "SEKEURPMI" }).Result;

            var result = new CurrencyResponse();

            if(((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(1)).seriesid.Trim(' ') == "SEKUSDPMI")
            {
                result.USD = ((ExRateAPI.ResultRow)((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(1)).resultrows.GetValue(0)).value;
            }
            else if ((((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(1)).seriesid.Trim(' ') == "SEKEURPMI"))
            {
                result.EUR = ((ExRateAPI.ResultRow)((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(1)).resultrows.GetValue(0)).value;
            }

            if (((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(0)).seriesid.Trim(' ') == "SEKUSDPMI")
            {
                result.USD = ((ExRateAPI.ResultRow)((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(0)).resultrows.GetValue(0)).value;
            }
            else if ((((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(0)).seriesid.Trim(' ') == "SEKEURPMI"))
            {
                result.EUR = ((ExRateAPI.ResultRow)((ExRateAPI.ResultSeries)((ExRateAPI.ResultGroup)res.@return.groups.GetValue(0)).series.GetValue(0)).resultrows.GetValue(0)).value;
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
