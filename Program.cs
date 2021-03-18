﻿using System;
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
        private static readonly SemaphoreSlim _gate = new SemaphoreSlim(1);

        private static void Main(string[] args)
        {
            Task.WaitAll(CreateCalls().ToArray());
        }

        private static IEnumerable<Task> CreateCalls()
        {
            // when number of calling service is too high, network breaks the calling execution
            // (threads are canceled)
            // and throws the error
            for (int i = 0; i < 500; i++)
            {
                yield return CallGoogle();
            }
        }

        private static async Task CallGoogle()
        {
            var response = await _client.GetAsync("https://google.com");
            Console.WriteLine(response.StatusCode);
        }
    }
}
