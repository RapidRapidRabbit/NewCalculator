using System;
using System.Diagnostics;
using System.Linq;


namespace NewCalc
{
    public class Program
    {
        private static readonly char[] ValidOperations = { '+', '-', '*', '/' };

        public static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var firstValue = new BigInt(0);
                    var secondValue = new BigInt(0);
                    var result = new BigInt(0);
                    char operation;

                    Console.WriteLine("Введите первое число");

                    firstValue = new BigInt(GetValue());

                    while (true)
                    {
                        Console.WriteLine("Желаемая арифметическая операция (+ - * /)");

                        if (char.TryParse(Console.ReadLine(), out char oper) && ValidOperations.Contains(oper))
                        {
                            operation = oper;
                            break;
                        }

                        Console.WriteLine("Неверный оператор, попробуйте еще раз");
                    }


                    Console.WriteLine("Введите второе число");

                    secondValue = new BigInt(GetValue());

                    switch (operation)
                    {
                        case '+':
                            result = firstValue + secondValue;
                            break;
                        case '-':
                            result = firstValue - secondValue;
                            break;
                        case '*':
                            result = firstValue * secondValue;
                            break;
                        case '/':
                            result = firstValue / secondValue;
                            break;
                        default:
                            Console.WriteLine("something went wrong");
                            break;
                    }

                    Console.WriteLine($"Результат: {result} \r\n");
                }
                catch
                {
                    Console.WriteLine("Что-то пошло не так, попробуйте еще раз");
                }
            }
        }

        private static bool CheckValue(string value) => value.All(char.IsNumber);

        private static string GetValue()
        {
            string value;

            while (true)
            {
                value = Console.ReadLine();

                if (CheckValue(value))
                    break;

                Console.WriteLine("Ошибка, некорректные данные, попробуйте еще раз");
            }

            return value;
        }

    }
}
