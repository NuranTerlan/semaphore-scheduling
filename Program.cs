using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreUsage
{
    internal class Program
    {
        private static readonly HttpClient _client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
        private static readonly SemaphoreSlim _gate = new SemaphoreSlim(30);

        private static void Main(string[] args)
        {
            Task.WaitAll(CreateCalls().ToArray());
        }

        private static IEnumerable<Task> CreateCalls()
        {
            // when number of calling service is too high, network breaks the calling execution
            // (tasks are canceled)
            // and throws the error
            for (int i = 0; i < 500; i++)
            {
                yield return CallGoogle();
            }
        }

        private static async Task CallGoogle()
        {
            try
            {
                await _gate.WaitAsync();
                var response = await _client.GetAsync("https://hackerrank.com");
                _gate.Release();
                Console.WriteLine(response.StatusCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
