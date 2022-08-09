using System;

namespace ConsoleApp
{
    internal abstract class BaseTests
    {
        public abstract void Run();
        
        public void Wait()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        public void WriteTitle(string title)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine();
        }


    }
}
