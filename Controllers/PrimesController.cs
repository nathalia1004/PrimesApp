using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace mywebapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimesController : ControllerBase
    {
        [HttpGet]
        [Route("{n}")]
        public IActionResult Get(int n)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var count = PrimeCount(n);
            watch.Stop();
            return Ok($"Primes - {count}, Time Taken - {watch.ElapsedMilliseconds}ms, Instance Id - {Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID")}");
        }

        public static int PrimeCount(int lessThanN)
        {
            Func<int, bool> isPrime = new Func<int, bool>((n) =>
            {
                if (n <= 1) return false;
                if (n == 2) return true;
                if (n % 2 == 0) return false;
                for (int i = 3; i <= Math.Sqrt(n); i += 2)
                {
                    if (n % i == 0) return false;
                }
                return true;
            });

            int primeCount = 0;
            for (int i = 2; i < lessThanN; i++)
                primeCount += isPrime(i) ? 1 : 0;
            return primeCount;
        }
    }
}
