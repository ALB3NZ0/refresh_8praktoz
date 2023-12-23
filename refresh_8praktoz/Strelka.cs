using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provodnik
{
    internal class Strelka
    {
        public static void Show(int minStrelochka, int maxStrelochka, out int posit, out ConsoleKeyInfo key)
        {
            posit = minStrelochka;
            Console.SetCursorPosition(0, posit);
            Console.Write("->");
            do
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow && posit != minStrelochka)
                {
                    Console.SetCursorPosition(0, posit);
                    Console.Write("  ");
                    posit--;
                }
                else if (key.Key == ConsoleKey.DownArrow && posit != maxStrelochka)
                {
                    Console.SetCursorPosition(0, posit);
                    Console.Write("  ");
                    posit++;
                }
                Console.SetCursorPosition(0, posit);
                Console.Write("->");
            }
            while (key.Key != ConsoleKey.Enter & key.Key != ConsoleKey.Escape & key.Key != ConsoleKey.F1 & key.Key != ConsoleKey.F2 & key.Key != ConsoleKey.F3 & key.Key != ConsoleKey.F10);
        }
    }
}
