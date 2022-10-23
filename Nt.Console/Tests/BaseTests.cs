using System;

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
            Console.WriteLine(line);
            Console.WriteLine(title.ToUpper());
            Console.WriteLine(line);
        }
        
        public void Write(string msg = "")
        {
            Console.WriteLine(msg);
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
