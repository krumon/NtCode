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
            string line = string.Empty;
            foreach (char c in title)
                line += "-";
            Console.WriteLine(line);
            Console.WriteLine(title.ToUpper());
            Console.WriteLine(line);
        }

        public void WriteEnum<T>()
            where T : Enum
        {
            Array array = Enum.GetValues(typeof(T));
            foreach (T t in array)
                Console.WriteLine(t.ToString());
        }

        public void WriteEnum<T>(Action<T> action)
            where T : Enum
        {
            Array array = Enum.GetValues(typeof(T));
            foreach (T t in array)
                action(t);
        }


    }
}
