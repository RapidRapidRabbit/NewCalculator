using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCalc
{
    public class BigInt
    {
        public List<byte> digits = new List<byte>(); // список для хранения цифр

        public bool isNegativeValue { get; private set; } // отрицательное ли значение
        
        public int Length { get { return this.digits.Count(); } } // просто длина числового значения для удобства

        private enum WhatValueIsGreater // для сравнения значений
        {
            First,
            Second,
            Same
        }


        public BigInt(string value) // конструктор, можно добавить другие при необходимости
        {            
            foreach (var c in value)
            {
                digits.Add(Convert.ToByte(c.ToString()));
            }            
        }
        public BigInt() { }


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


        //метод для сравнения значений
        private static WhatValueIsGreater CompareValues(BigInt first, BigInt second)
        {
            if (first.Length > second.Length)
                return WhatValueIsGreater.First;
            else if (second.Length > first.Length)
                return WhatValueIsGreater.Second;

            for(int i = 0; i < first.Length; i++)
            {
                if (first.digits[i] > second.digits[i])
                    return WhatValueIsGreater.First;
                else if (second.digits[i] > first.digits[i])
                    return WhatValueIsGreater.Second;
               
                   
            }

            return WhatValueIsGreater.Same;
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
            WhatValueIsGreater compareResult = CompareValues(firstValue, secondValue);

            if (compareResult == WhatValueIsGreater.First)
            {
                largiest = firstValue;
                lowest = secondValue;
            }
            else if (compareResult == WhatValueIsGreater.Second)
            {
                largiest = secondValue;
                lowest = firstValue;
                isNegativeResult = true;
            }
            else if (compareResult == WhatValueIsGreater.Same)
                return new BigInt("0");

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
            WhatValueIsGreater compare = CompareValues(divisible, divider);

            if (compare == WhatValueIsGreater.Second)
                return new BigInt("0");
            if (compare == WhatValueIsGreater.Same)
                return new BigInt("1");

            BigInt counter = new BigInt();
            BigInt temp = divisible;

            while (temp.digits[0] != 0 && temp.isNegativeValue != true)
            {
                temp -= divider;

                if (temp.digits[0] != 0 || temp.isNegativeValue != true)
                    counter = Addition(counter, new BigInt("1"));
            }

            return counter;
        }



        // перегрузка необходимых операторов, другие можно добавить
        public static BigInt operator + (BigInt first, BigInt second) 
        {
            return Addition(first, second);
        }
        public static BigInt operator - (BigInt first, BigInt second)
        {            
            return Substraction(first, second);            
        }
        public static BigInt operator * (BigInt first, BigInt second)
        {            
            return Multiplication(first, second);
        }
        public static BigInt operator / (BigInt first, BigInt second)
        {
            return Division(first, second);            
        }

    }
}
