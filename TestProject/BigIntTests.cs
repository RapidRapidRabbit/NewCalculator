using NewCalc;
using System;
using Xunit;

namespace TestProject
{
    public class BigIntTests
    {
        private readonly BigInt firstvalue = new BigInt("60789854763764564564344462395873985476384509839048650347683749826398473275983749573298749287349872398472385456234");
        private readonly BigInt secondvalue = new BigInt("32453463457543214798342562837648726391872498374562873648726347398475986238654984765983452387484357384928323546889");


        [Fact]
        public void AdditionReturnsExpectedValue()
        {            
            BigInt expected = new BigInt("93243318221307779362687025233522711868257008213611523996410097224874459514638734339282201674834229783400709003123");

            BigInt actual = firstvalue + secondvalue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubstractionReturnsExpectedValue()
        {
            BigInt expected = new BigInt("28336391306221349766001899558225259084512011464485776698957402427922487037328764807315296899865515013544061909345");

            BigInt actual = firstvalue - secondvalue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubstractionAddsNegativeSign()
        {
            BigInt actual = secondvalue - firstvalue;
            
            Assert.Equal("-", actual.ToString()[0].ToString());
        }        

        [Fact]
        public void MultiplicationReturnsExpectedValue()
        {
            BigInt expected = new BigInt("1972841330165192612537242186332314527558168410694813294974103816123904587540584094788818559005080077451467142290994930450377925799501396692658392481081149092104699235248544261832750209058810619645496150430593143907925356356026");

            BigInt actual = firstvalue * secondvalue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DivisionReturnsExpectedValue()
        {
            BigInt expected = new BigInt(1);

            BigInt actual = firstvalue / secondvalue;

            Assert.Equal(expected, actual);
        }

        [Fact]        
        public void DivisionByZeroThrowsDivideByZeroException()
        {
            BigInt divisible = new BigInt("10");
            BigInt divider = new BigInt("0");            

            Assert.Throws<DivideByZeroException>(() => divisible / divider);
        }
    }
}
