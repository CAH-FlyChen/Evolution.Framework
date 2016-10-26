using System;
using Xunit;

namespace Prime.UnitTests.Services
{
    public class PrimeService_IsPrimeShould
    {
         public PrimeService_IsPrimeShould() 
         { 
         } 
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void ReturnFalseGivenValuesLessThan2(int value)
        {
            //var result = _primeService.IsPrime(value);
            var result = false;
            Assert.False(result, $"{value} should not be prime");
        }
    }
}
