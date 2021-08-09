using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

// todo: when reaching a linebreak behaviour breaks
// todo: modularize the printer and the challenge

namespace TypeBenchmark
{
    class Program
    {
        static Random random = new Random();

        static void print<T>(T t) => Console.Write(t);
        static IEnumerable<int> range(int start, int end) => Enumerable.Range(start, end);


        const int LO = 32, HI = 126; 

        static char nextChar() => (char)random.Next(LO,HI+1);
       

        static void Main(string[] args)
        {
            Console.Clear();
            int length = 200;

            var colorDimmed = ConsoleColor.DarkGray;
            var colorCorrect = ConsoleColor.Green;
            var colorWrong = ConsoleColor.Red;

            Console.ForegroundColor = colorDimmed;
            var sb = new StringBuilder(length);
            foreach (int i in range(0,length)) sb.Append(nextChar());
            var str = sb.ToString();

            Console.Write(str);
            Console.SetCursorPosition(0,0);
            Console.CursorVisible = false;

            int entered = 0;
            while (entered < length) {

                var (left, top) = Console.GetCursorPosition();
                var inputKey = Console.ReadKey();
                var tmp = Console.ForegroundColor;

                if (inputKey.Key == ConsoleKey.Backspace) {
                    entered--;
                    (left, top) = Console.GetCursorPosition();
                    Console.ForegroundColor = colorDimmed;
                    print(str[entered]);
                    Console.SetCursorPosition(left, top);
                    Console.ForegroundColor = tmp;
                    continue;
                }

                var input = inputKey.KeyChar;
                var actual = str[entered];

                Console.ForegroundColor = input == actual ? colorCorrect : colorWrong;
                Console.SetCursorPosition(left, top);
                print(actual);

                Console.ForegroundColor = tmp;

                entered++;
            }

            Console.WriteLine();
            Console.WriteLine("Finished! :)");

            Console.ReadKey();
            Console.Clear();
            Environment.Exit(0);
        }
    }
}
