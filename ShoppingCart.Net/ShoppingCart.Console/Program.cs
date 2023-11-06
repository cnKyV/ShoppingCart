using System.Text.Json;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShoppingCart.Infrastructure.Services;

namespace ShoppingCart.Console;

using System;

class Program
{

    static async Task Main(string[] args)
    {
        while (true)
        {
            var input = Console.ReadLine();
            try
            {
                if (input == "exit")
                {
                    break;
                }
                var result = CommandInputConverterService.ConvertAndProcess(input);
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}