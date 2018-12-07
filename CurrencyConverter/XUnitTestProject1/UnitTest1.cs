using System;
using Xunit;

using Cryptolens.CurrencyConverter;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var test = CurrencyService.GetSEKExchangeRates(); 
        }
    }
}
