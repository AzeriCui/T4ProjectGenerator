using System;
using System.Threading;

namespace T4ProjectGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonBuilder builder = new CommonBuilder();
            builder.Run();

            Console.WriteLine("OK");
            Thread.Sleep(2000);
        }
    }
}
