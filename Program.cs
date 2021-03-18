using System;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreUsage
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var gate = new SemaphoreSlim(2);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Start");
                await gate.WaitAsync();
                Console.WriteLine("Do Some Work");
                Console.WriteLine("Finish");
            }
        }
    }
}
