using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Provodnik
{
    internal class GetDir
    {
        public static void Drive(string dirName, out ConsoleKeyInfo click2)
        {
            int r = 0, posit;
            string[] dirNames = new string[20];
            dirNames[r] = dirName;
            do
            {
                int min = 3, max = 3, chDir = 0, chFile = 0;
                Console.Clear();
                Console.WriteLine($"   Папка - {dirNames[r]}");
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("     Название                      Дата создания     Тип");
                Console.SetCursorPosition(65, 3);
                Console.WriteLine("| F1 - Создать папку");
                Console.SetCursorPosition(65, 4);
                Console.WriteLine("| F2 - Создать файл");
                Console.SetCursorPosition(65, 5);
                Console.WriteLine("| F3 - Удалить");
                Console.SetCursorPosition(65, 6);
                Console.WriteLine("| Esc - Вернуться");
                Console.SetCursorPosition(65, 7);
                Console.WriteLine("| ----------------------");
                Console.SetCursorPosition(65, 8);
                Console.WriteLine("| Выберите папку или файл и нажмите Enter");

                string[] dirs = Directory.GetDirectories(dirNames[r]);
                foreach (string s in dirs)
                {
                    DirectoryInfo s1 = new DirectoryInfo(s);
                    Console.SetCursorPosition(0, max);
                    Console.WriteLine($"   {s1.Name}");
                    Console.SetCursorPosition(33, max);
                    Console.WriteLine(s1.CreationTime);
                    max++;
                    chDir++;
                }
                string[] files = Directory.GetFiles(dirNames[r]);
                foreach (string s in files)
                {
                    DirectoryInfo s1 = new DirectoryInfo(s);
                    Console.SetCursorPosition(0, max);
                    Console.WriteLine($"   {s1.Name}");
                    Console.SetCursorPosition(33, max);
                    Console.WriteLine(s1.CreationTime);
                    Console.SetCursorPosition(53, max);
                    Console.WriteLine(s.Substring(s.LastIndexOf('.')));
                    max++;
                    chFile++;
                }
                max--;
                Strelka.Show(min, max, out posit, out click2);
                posit = posit - 2;
                if (chDir < posit & click2.Key == ConsoleKey.Enter)
                {
                    Process.Start(new ProcessStartInfo { FileName = files[posit - chDir - 1], UseShellExecute = true });
                }
                if (click2.Key == ConsoleKey.Enter && chDir >= posit)
                {
                    r++;
                    dirNames[r] = dirs[posit - 1];
                }
                if (click2.Key == ConsoleKey.Escape && r >= 0)
                {
                    r--;
                }
                if (click2.Key == ConsoleKey.F1)
                {
                    Console.SetCursorPosition(65, 11);
                    Console.Write("Введите название папки");
                    Console.SetCursorPosition(65, 12);
                    string Papka = Console.ReadLine();
                    Directory.CreateDirectory(dirNames[r] + "\\" + Papka);
                }
                if (click2.Key == ConsoleKey.F2)
                {
                    Console.SetCursorPosition(65, 11);
                    Console.Write("Введите название файла и расширение");
                    Console.SetCursorPosition(65, 12);
                    string Papka = Console.ReadLine();
                    File.Create(dirNames[r] + "\\" + Papka);
                }
                if (click2.Key == ConsoleKey.F3 && r >= 0 && chDir >= posit)
                {
                    Directory.Delete(dirs[posit - 1]);
                }
                if (click2.Key == ConsoleKey.F3 && r >= 0 && chDir < posit)
                {
                    File.Delete(files[posit - chDir - 1]);
                }
                if (click2.Key == ConsoleKey.F10)
                {
                    return;
                }
            }
            while (r >= 0);
        }
    }
}