using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCalc
{
    public class BigInt : IComparable<BigInt>
    {
        private List<byte> digits = new List<byte>(); // список для хранения цифр

        public bool isNegativeValue { get; private set; } // отрицательное ли значение
        
        public int Length { get { return this.digits.Count(); } } // просто длина числового значения для удобства        


        // конструкторы из строки или числа, можно добавить другие при необходимости
        public BigInt() { }
        public BigInt(string value) 
        {
            if (value[0] == '-')
                isNegativeValue = true;
            foreach (var c in value)
            {
                digits.Add(Convert.ToByte(c.ToString()));
            }            
        }
        public BigInt(int value)
        {
            if (value < 0)
            {
                isNegativeValue = true;
                value *= -1;
            }
            else if (value == 0)            
                digits.Add(0);
            

            while (value > 0)
            {
                digits.Insert(0,((byte)(value % 10)));
                value /= 10;
            }
        }
        


        // свой ToString, возвращает значение 
        public override string ToString()
        {
            var result = new StringBuilder();

            if (isNegativeValue)
                result.Append("-");

            foreach (var digit in this.digits)
            {
                result.Append(digit);
            }

            return result.ToString();
        }


       //реализация IComparable<T>
        public int CompareTo(BigInt obj)
        {
            if (this.isNegativeValue == false && obj.isNegativeValue == true)
                return 1;
            if (this.isNegativeValue == true && obj.isNegativeValue == false)
                return -1;

            if (this.Length > obj.Length)
                return 1;
            else if (obj.Length > this.Length)
                return -1;

            else
            {
                for (int i = 0; i < this.Length; i++)
                {
                    if (this.digits[i] > obj.digits[i])
                        return 1;
                    if (obj.digits[i] > this.digits[i])
                        return -1;
                }
            }
            return 0;            
        }       

        // далее идут методы арифметических вычислений, все построены на принципе вычислений в столбик :) только деление через вычитание

        //cложение
        private static BigInt Addition (BigInt firstValue, BigInt secondValue)
        {
            firstValue.digits.Reverse();
            secondValue.digits.Reverse();
            BigInt result = new BigInt();
            byte temp = 0;

            for(int i = 0; i < firstValue.Length || i < secondValue.Length; i++)
            {
                byte digit = 0;

                if (i < firstValue.Length)
                    digit += firstValue.digits[i];

                if (i < secondValue.Length)
                    digit += secondValue.digits[i];

                digit += temp;
                
                result.digits.Insert(0,(byte)(digit % 10));
                temp = (byte)(digit / 10);
            }

            if (temp != 0)
                result.digits.Insert(0, temp);
           
            return result;
        }

        //вычитание
        private static BigInt Substraction (BigInt firstValue, BigInt secondValue)
        {
            BigInt result = new BigInt();
            BigInt largiest = new BigInt();
            BigInt lowest = new BigInt();
            bool isNegativeResult = false;            
            int compareResult = firstValue.CompareTo(secondValue);

            if (compareResult == 1)
            {
                largiest = firstValue;
                lowest = secondValue;
            }
            else if (compareResult == -1)
            {
                largiest = secondValue;
                lowest = firstValue;
                isNegativeResult = true;
            }
            else if (compareResult == 0)
                return new BigInt(0);

            largiest.digits.Reverse();
            lowest.digits.Reverse();
            byte temp = 0;

            for(int i = 0; i < largiest.Length; i++)
            {
                int digit = largiest.digits[i] - temp;

                if (i < lowest.Length)
                    digit = digit - lowest.digits[i];

                if (digit < 0)
                {
                    digit += 10;
                    temp = 1;
                }
                else
                    temp = 0;

                result.digits.Add((byte)digit);
            }

            for (int i = result.Length - 1; i > 0; i--)
            {
                if (result.digits[i] == 0)
                    result.digits.RemoveAt(i);
                else
                    break;
            }

            result.isNegativeValue = isNegativeResult;
            result.digits.Reverse();
            return result;
        }


        //Умножение
        private static BigInt Multiplication (BigInt firstValue, BigInt secondValue)
        {
            if (firstValue == new BigInt(0) || secondValue == new BigInt(0))
                return new BigInt(0);

            firstValue.digits.Reverse();
            secondValue.digits.Reverse();
            BigInt result = new BigInt();

            for(int i = 0; i < firstValue.Length; i++)
            {
                for (int j = 0, carry = 0; j < secondValue.Length || carry > 0; j++)
                {
                    int current = 0;
                    int previous = (i + j) < result.Length ? result.digits[i + j] : 0;
                    int currentA = i < firstValue.Length ? firstValue.digits[i] : 0;
                    int currentB = j < secondValue.Length ? secondValue.digits[j] : 0;

                    current = previous + currentA * currentB + carry;

                    byte toCollection = (byte)(current % 10);
                    if (result.Length <= i + j)
                        result.digits.Insert(i + j, toCollection);
                    else
                        result.digits[i + j] = toCollection;

                    carry = current / 10;
                }
            }

            result.digits.Reverse();
            return result;
        }


        //Деление
        private static BigInt Division (BigInt divisible, BigInt divider)
        {            
            int compare = divisible.CompareTo(divider);

            if (divider.digits[0] == 0)
                throw new DivideByZeroException();
            if (compare == -1)
                return new BigInt(0);
            if (compare == 0)
                return new BigInt(1);

            BigInt counter = new BigInt();            

            while (divisible.digits[0] != 0 && divisible.isNegativeValue != true)
            {
                divisible = Substraction(new BigInt(divisible.ToString()), new BigInt(divider.ToString()));

                if (divisible.digits[0] != 0 && divisible.isNegativeValue != true)
                    counter = Addition(counter, new BigInt(1));
            }

            return counter;
        }



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
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
