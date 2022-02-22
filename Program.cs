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
                string firstValue;
                string secondValue;
                string result = "";
                char operation;
                char[] validOperations = { '+', '-', '*', '/' };

                while (true)
                {
                    Console.WriteLine("Введите первое число");
                    firstValue = Console.ReadLine();

                    if (CheckValue(firstValue))
                        break;
                    else
                        Console.WriteLine("Ошибка, некорректные данные, попробуйте еще раз");
                }

                while (true)
                {
                    Console.WriteLine("Желаемая математическая операция (+ - * /)");

                    if (char.TryParse(Console.ReadLine(), out char oper) && validOperations.Contains(oper))
                    {
                        operation = oper;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный оператор, попробуйте еще раз");
                    }
                }

                while (true)
                {
                    Console.WriteLine("Введите второе число");

                    secondValue = Console.ReadLine();

                    if (CheckValue(secondValue))
                        break;
                    else
                        Console.WriteLine("Ошибка, некорректные данные, попробуйте еще раз");
                }


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
