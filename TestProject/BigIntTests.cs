using NewCalc;
using System;
using Xunit;

namespace TestProject
{
    public class BigIntTests
    {
        private readonly BigInt _firstValue = new("876543210");
        private readonly BigInt _secondValue = new("123456789");
        private readonly BigInt _zero = new(0);

        [Fact]
        public void Addition_ReturnsExpectedValue()
        {
            //Arrange
            var expected = new BigInt("999999999");

            //Act and Assert
            Assert.Equal(expected, _firstValue + _secondValue);
        }

        [Fact]
        public void Substraction_ReturnsExpectedValue()
        {
            //Arrange
            var expected = new BigInt("753086421");

            //Act and Assert
            Assert.Equal(expected, _firstValue - _secondValue);
        }        

        [Fact]
        public void Division_ReturnsExpectedValue()
        {
            //Arrange
            var expected = new BigInt(7);

            //Act and Assert
            Assert.Equal(expected, _firstValue / _secondValue);
        }

        [Fact]
        public void Division_DivisionByTheSameValue_Returns_1()
        {
            //Act and Assert
            Assert.Equal(new BigInt(1), _firstValue / _firstValue);
        }

        [Fact]
        public void Division_DivisonByTheLargerValue_Returns_0()
        {
            //Act and Assert
            Assert.Equal(_zero, _secondValue / _firstValue);
        }

        [Fact]
        public void Multiplication_ReturnsExpectedValue()
        {
            //Arrange
            var expected = new BigInt("108215210126352690");

            //Act and Assert
            Assert.Equal(expected, _firstValue * _secondValue);
        }

        [Fact]
        public void Multiplication_MultiplicationByZero_Returns_0()
        {
            //Act and Assert
            Assert.Equal(_zero, _firstValue * _zero);
        }

        [Fact]
        public void Substraction_AddsNegativeSign()
        {
            //Act
            var actual = _secondValue - _firstValue;

            //Assert
            Assert.Equal("-", actual.ToString()[0].ToString());
        }

        [Fact]        
        public void Division_DivisionByZero_ThrowsDivideByZeroException()
        {
            //Act and Assert
            Assert.Throws<DivideByZeroException>(() => _firstValue / _zero);
        }       
    }
}
