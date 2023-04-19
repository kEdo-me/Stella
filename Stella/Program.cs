using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stella.Workers;
namespace Stella
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "Stella";
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<RecognitionService>();
                })
                .Build();

            host.Run();
        }
    }
}
