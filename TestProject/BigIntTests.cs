using NewCalc;
using System;
using Xunit;

namespace TestProject
{
    public class BigIntTests
    {
        private readonly BigInt Firstvalue = new BigInt("876543210");
        private readonly BigInt Secondvalue = new BigInt("123456789");
        private readonly BigInt Zero = new BigInt(0);


        [Fact]
        public void AdditionReturnsExpectedValue()
        {            
            BigInt expected = new BigInt("999999999");

            BigInt actual = Firstvalue + Secondvalue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubstractionReturnsExpectedValue()
        {
            BigInt expected = new BigInt("753086421");

            BigInt actual = Firstvalue - Secondvalue;

            Assert.Equal(expected, actual);
        }        

        [Fact]
        public void DivisionReturnsExpectedValue()
        {
            BigInt expected = new BigInt(7);

            BigInt actual = Firstvalue / Secondvalue;
            BigInt shouldBeOne = Firstvalue / Firstvalue;
            BigInt shouldbeZero = Secondvalue / Firstvalue;


            Assert.Equal(Zero, shouldbeZero);
            Assert.Equal(new BigInt(1), shouldBeOne);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MultiplicationReturnsExpectedValue()
        {
            BigInt expected = new BigInt("108215210126352690");

            BigInt actual = Firstvalue * Secondvalue;
            BigInt shouldBeZero = Firstvalue * Zero;

            Assert.Equal(Zero, shouldBeZero);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubstractionAddsNegativeSign()
        {
            BigInt actual = Secondvalue - Firstvalue;

            Assert.Equal("-", actual.ToString()[0].ToString());
        }

        [Fact]        
        public void DivisionByZeroThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => Firstvalue / Zero);
        }       
    }
}
