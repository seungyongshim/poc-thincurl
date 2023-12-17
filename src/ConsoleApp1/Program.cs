using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurlThin;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new CurlThinClient();

            var ret = client.Post("https://httpbin.org/", new
            {
                Hello = "World"
            });

            Console.WriteLine(ret.CurlResult);
        }
    }
}
