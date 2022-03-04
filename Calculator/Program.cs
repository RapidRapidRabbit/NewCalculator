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
                try
                {
                    BigInt firstValue = new BigInt(0);
                    BigInt secondValue = new BigInt(0);
                    BigInt result = new BigInt(0);
                    char operation;
                    char[] validOperations = { '+', '-', '*', '/' };


                    // cледующее просто показывает работу реализации интерфейса IComparable

                    /*BigInt[] numbersForSorting = { new BigInt(100), new BigInt(15), new BigInt(45345), new BigInt("44"), new BigInt(500), new BigInt(-500) };
                    Array.Sort(numbersForSorting);
                    foreach (var number in numbersForSorting)
                    {
                        Console.WriteLine(number);
                    }*/

                    Console.WriteLine("Введите первое число");
                    firstValue = new BigInt(GetValue());



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

            string GetValue()
            {
                string value = string.Empty;

                while (true)
                {
                    value = Console.ReadLine();
                    if (CheckValue(value))
                        break;
                    else
                        Console.WriteLine("Ошибка, некорректные данные, попробуйте еще раз");
                }

                return value;
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
