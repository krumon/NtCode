using System;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp.Tests
{
    internal abstract class BaseTests
    {
        public abstract void Run();
        
        public void Title(string title)
        {
            string line = string.Empty;
            foreach (char c in title)
                line += "-";
            WriteLine(line);
            WriteLine(title.ToUpper());
            WriteLine(line);
        }
        
        public void Subtitle(string subtitle)
        {
            WriteLine($"*** {subtitle.ToUpper()} ***");
        }
        
        public void WriteLine(object o = null)
        {
            if (o == null)
            {
                Console.WriteLine();
                return;
            }
            Console.WriteLine(o.ToString());
        }

        public void NewLine()
        {
            Console.WriteLine();
        }
        
        public void Wait()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void WaitAndClear()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
