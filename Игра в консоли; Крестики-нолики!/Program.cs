using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Игра_в_консоли__Крестики_нолики_
{
    internal class Program
    {
        static Random random = new Random();

        static void PrintPole(string[,] array)
        {
            Console.Clear();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write(i + " ");
                for (int j = 1; j < array.GetLength(1); j++)
                {
                    if (i == 0) array[i, j] = j.ToString();
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static bool WinPlayer(string[,] array, string symbol)
        {
            return (
                (array[1, 1] == symbol && array[1, 2] == symbol && array[1, 3] == symbol) ||
                (array[2, 1] == symbol && array[2, 2] == symbol && array[2, 3] == symbol) ||
                (array[3, 1] == symbol && array[3, 2] == symbol && array[3, 3] == symbol) ||

                (array[1, 1] == symbol && array[2, 1] == symbol && array[3, 1] == symbol) ||
                (array[1, 2] == symbol && array[2, 2] == symbol && array[3, 2] == symbol) ||
                (array[1, 3] == symbol && array[2, 3] == symbol && array[3, 3] == symbol) ||

                (array[1, 1] == symbol && array[2, 2] == symbol && array[3, 3] == symbol) ||
                (array[3, 1] == symbol && array[2, 2] == symbol && array[1, 3] == symbol)
            );
        }

        private static bool NoMoreFreeCells(string[,] array)
        {
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    if (array[i, j] == "*")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsValidChoice(string[,] field, int row, int col)
        {
            return (1 <= row && row <= 3) &&
                   (1 <= col && col <= 3) &&
                   field[row, col] == "*";
        }

        static void Main(string[] args)
        {
            string[,] pole = new string[4, 4];
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int j = 0; j < pole.GetLength(1); j++)
                {
                    pole[i, j] = "*";
                }
            }
            PrintPole(pole);

            bool okey = true;
            bool game = true;
            bool ok = true;
            bool game1 = true;
            while (okey)
            {
                string player = "Пользователь";
                string HumanSymbol = "X";
                string ComputerSymbol = "O";
                if (okey)
                {
                    int x, o;
                    while (true)
                    {
                        string hod = Console.ReadLine();
                        string[] coor = hod.Split(' ');

                        x = int.Parse(coor[0]);
                        o = int.Parse(coor[1]);

                        if (!IsValidChoice(pole, x, o))
                        {
                            Console.WriteLine("Некорректный ход!");
                        }
                        else
                        {
                            break;
                        }
                    }

                    pole[x, o] = HumanSymbol;
                    PrintPole(pole);

                    if (WinPlayer(pole, HumanSymbol))
                    {
                        Console.WriteLine("Кожаные ублюдки победили. Восстание машин не случится!");
                        break;
                    }
                    if (NoMoreFreeCells(pole))
                    {
                        Console.WriteLine("Ничья, придется играть заново!");
                        break;
                    }

                    int verticalpc, skyline;

                    do
                    {
                        verticalpc = random.Next(1, 4);
                        skyline = random.Next(1, 4);
                    } while (!IsValidChoice(pole, verticalpc, skyline));

                    Thread.Sleep(200);
                    pole[verticalpc, skyline] = ComputerSymbol;
                    PrintPole(pole);

                    if (WinPlayer(pole, ComputerSymbol))
                    {
                        Console.WriteLine("Электронный мозг оказался сильнее. Скоро он захватит мир!");
                        break;
                    }

                    if (NoMoreFreeCells(pole))
                    {
                        Console.WriteLine("Ничья, придется играть заново!");
                        break;
                    }
                }
            }
        }
    }
}
