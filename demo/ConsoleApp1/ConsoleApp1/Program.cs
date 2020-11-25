using System;
using System.Reflection;
using Serilog;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("log.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .Enrich.WithProperty("target_index",
                    $"serilog-{Assembly.GetExecutingAssembly().GetName().Name}"
                        .ToLower())
                .WriteTo.Http("http://192.168.16.152:8087")
                .CreateLogger();

            Log.Information("Hello, Serilog!");

            Log.CloseAndFlush();

        }
    }
}
