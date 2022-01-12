using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using summary.Models;

namespace summary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateSummaries(host);
            host.Run();
        }

        private static void CreateSummaries(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var summaryContext = scope
                    .ServiceProvider
                    .GetRequiredService<SummaryContext>();

                for (var i = 0; i < 5; ++i)
                {
                    summaryContext.Add(SummaryGenerator.Generate(10, 5));
                }

                summaryContext.SaveChanges();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}