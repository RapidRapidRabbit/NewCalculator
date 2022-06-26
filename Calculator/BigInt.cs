using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewCalc
{
    public class BigInt : IComparable<BigInt> , ICloneable
    {
        private readonly List<byte> _digits = new(); // список для хранения цифр

        public bool IsNegativeValue { get; private set; } // отрицательное ли значение
        
        public int Length => _digits.Count(); // просто длина числового значения для удобства        


        // конструкторы из строки или числа, можно добавить другие при необходимости
        private BigInt()
        {

        }

        public BigInt(string value) 
        {
            if (value[0] == '-')
                IsNegativeValue = true;

            foreach (var c in value)
            {
                _digits.Add(Convert.ToByte(c.ToString()));
            }            
        }

        public BigInt(int value)
        {
            switch (value)
            {
                case < 0:
                    IsNegativeValue = true;
                    value *= -1;
                    break;
                case 0:
                    _digits.Add(0);
                    break;
            }

            while (value > 0)
            {
                _digits.Insert(0,(byte)(value % 10));
                value /= 10;
            }
        }

        // свой ToString, возвращает значение 
        public override string ToString()
        {
            var result = new StringBuilder();

            if (IsNegativeValue)
            {
                result.Append('-');
            }

            foreach (var digit in _digits)
            {
                result.Append(digit);
            }

            return result.ToString();
        }


       //реализация IComparable<T>
        public int CompareTo(BigInt obj)
        {
            switch (IsNegativeValue)
            {
                case false when obj.IsNegativeValue:
                    return 1;

                case true when obj.IsNegativeValue == false:
                    return -1;
            }

            if (Length > obj.Length)
                return 1;

            if (obj.Length > Length)
                return -1;

            for (int i = 0; i < Length; i++)
            {
                if (_digits[i] > obj._digits[i])
                    return 1;
                if (obj._digits[i] > _digits[i])
                    return -1;
            }

            return 0;            
        }

        // далее идут методы арифметических вычислений, все построены на принципе вычислений в столбик :) только деление через вычитание

        #region AriphmeticOperations

        //cложение
        private static BigInt Addition (BigInt firstValue, BigInt secondValue)
        {
            var first = (BigInt)firstValue.Clone();
            var second = (BigInt)secondValue.Clone();
            first._digits.Reverse();
            second._digits.Reverse();

            BigInt result = new();

            byte temp = 0;

            for(int i = 0; i < first.Length || i < second.Length; i++)
            {
                byte digit = 0;

                if (i < first.Length)
                    digit += first._digits[i];

                if (i < second.Length)
                    digit += second._digits[i];

                digit += temp;
                
                result._digits.Insert(0,(byte)(digit % 10));
                temp = (byte)(digit / 10);
            }

            if (temp != 0)
                result._digits.Insert(0, temp);
           
            return result;
        }

        //вычитание
        private static BigInt Substraction (BigInt firstValue, BigInt secondValue)
        {
            var result = new BigInt();
            var largiest = new BigInt();
            var lowest = new BigInt();
            bool isNegativeResult = false;

            switch (firstValue.CompareTo(secondValue))
            {
                case 1:
                    largiest = (BigInt)firstValue.Clone();
                    lowest = (BigInt)secondValue.Clone();
                    break;

                case -1:
                    largiest = (BigInt)secondValue.Clone();
                    lowest = (BigInt)firstValue.Clone();
                    isNegativeResult = true;
                    break;

                case 0:
                    return new BigInt(0);
            }

            largiest._digits.Reverse();
            lowest._digits.Reverse();

            byte temp = 0;

            for(int i = 0; i < largiest.Length; i++)
            {
                int digit = largiest._digits[i] - temp;

                if (i < lowest.Length)
                    digit -= lowest._digits[i];

                if (digit < 0)
                {
                    digit += 10;
                    temp = 1;
                }
                else
                    temp = 0;

                result._digits.Add((byte)digit);
            }

            for (int i = result.Length - 1; i > 0; i--)
            {
                if (result._digits[i] == 0)
                {
                    result._digits.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }

            result.IsNegativeValue = isNegativeResult;
            result._digits.Reverse();
            return result;
        }


        //Умножение
        private static BigInt Multiplication (BigInt firstValue, BigInt secondValue)
        {
            if (firstValue == new BigInt(0) || secondValue == new BigInt(0))
                return new BigInt(0);

            var first = (BigInt)firstValue.Clone();
            var second = (BigInt)secondValue.Clone();
            first._digits.Reverse();
            second._digits.Reverse();
            BigInt result = new();

            for(int i = 0; i < first.Length; i++)
            {
                for (int j = 0, carry = 0; j < second.Length || carry > 0; j++)
                {
                    int previous = i + j < result.Length ? result._digits[i + j] : 0;
                    int currentA = i < first.Length ? first._digits[i] : 0;
                    int currentB = j < second.Length ? second._digits[j] : 0;

                    var current = previous + currentA * currentB + carry;

                    byte toCollection = (byte)(current % 10);

                    if (result.Length <= i + j)
                    {
                        result._digits.Insert(i + j, toCollection);
                    }
                    else
                    {
                        result._digits[i + j] = toCollection;
                    }                        

                    carry = current / 10;
                }
            }

            result._digits.Reverse();
            return result;
        }


        //Деление
        private static BigInt Division (BigInt divisible, BigInt divider)
        {
            if (divider._digits[0] == 0)
                throw new DivideByZeroException();

            switch (divisible.CompareTo(divider))
            {
                case -1:
                    return new BigInt(0);
                case 0:
                    return new BigInt(1);
            }

            BigInt counter = new(0);            

            while (divisible._digits[0] != 0 && divisible.IsNegativeValue != true)
            {                
                divisible = Substraction(new BigInt(divisible.ToString()), new BigInt(divider.ToString()));

                if (divisible._digits[0] != 0 && divisible.IsNegativeValue != true)
                    counter = Addition(counter, new BigInt(1));
            }

            return counter;
        }

        #endregion

        public object Clone() => new BigInt(ToString());        

        // перегрузка необходимых операторов, другие можно добавить
        public static BigInt operator + (BigInt first, BigInt second) => Addition(first, second);        
        public static BigInt operator - (BigInt first, BigInt second) => Substraction(first, second);
        public static BigInt operator *(BigInt first, BigInt second) => Multiplication(first, second);
        public static BigInt operator /(BigInt first, BigInt second) => Division(first, second);
        public static bool operator <(BigInt first, BigInt second) => first.CompareTo(second) < 0;       
        public static bool operator >(BigInt first, BigInt second) => first.CompareTo(second) > 0;
        public static bool operator <=(BigInt first, BigInt second) => first.CompareTo(second) <= 0;
        public static bool operator >=(BigInt first, BigInt second) => first.CompareTo(second) >= 0;
        public static bool operator ==(BigInt first, BigInt second) => first.CompareTo(second) == 0;
        public static bool operator !=(BigInt first, BigInt second) => first.CompareTo(second) != 0;
        
        public override bool Equals(object obj)
        {
            if (obj is BigInt number)
                return this == number;

            return false;
        }
    }
}
