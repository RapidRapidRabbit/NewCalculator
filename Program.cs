using System;
using System.Diagnostics;
using System.Linq;


namespace NewCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string firstValue = string.Empty;
                string secondValue = string.Empty;
                string result = string.Empty;
                char operation;
                char[] validOperations = { '+', '-', '*', '/' };


                // это просто показывает работу реализации интерфейса IComparable
                BigInt[] numbersForSorting = { new BigInt(100), new BigInt(15), new BigInt(45345), new BigInt("44"), new BigInt(500), new BigInt(-500) };
                Array.Sort(numbersForSorting);
                foreach (var number in numbersForSorting)
                {
                    Console.WriteLine(number);
                }

                Console.WriteLine("Введите первое число");
                firstValue = GetValue();



                while (true)
                {
                    Console.WriteLine("Желаемая математическая операция (+ - * /)");

                    if (char.TryParse(Console.ReadLine(), out char oper) && validOperations.Contains(oper))
                    {
                        operation = oper;
                        break;
                    }
                    else
                        Console.WriteLine("Неверный оператор, попробуйте еще раз");
                }


                Console.WriteLine("Введите второе число");
                secondValue = GetValue();


                switch (operation)
                {
                    case '+':
                        result = (new BigInt(firstValue) + new BigInt(secondValue)).ToString();
                        break;
                    case '-':
                        result = (new BigInt(firstValue) - new BigInt(secondValue)).ToString();
                        break;
                    case '*':
                        result = (new BigInt(firstValue) * new BigInt(secondValue)).ToString();
                        break;
                    case '/':
                        result = (new BigInt(firstValue) / new BigInt(secondValue)).ToString();
                        break;
                    default:
                        Console.WriteLine("something went wrong");
                        break;
                }

                Console.WriteLine($"Результат: {result} \r\n");
            }


            string GetValue()
            {
                string result = string.Empty;

                while (true)
                {                    
                    result = Console.ReadLine();
                    if (CheckValue(result))
                        break;
                    else
                        Console.WriteLine("Ошибка, некорректные данные, попробуйте еще раз");
                }

                return result;
            }
            
            bool CheckValue(string value)
            {
                foreach (var symbol in value)
                {
                    if (!char.IsNumber(symbol))
                        return false;
                }
                return true;
            }
        }

    }
}
