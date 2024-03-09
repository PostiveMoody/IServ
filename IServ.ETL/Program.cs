using IServ.ETL.DAL;
using IServ.ETL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IServ.ETL
{
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        static async Task Main(string[] args)
        {
            await Console.Out.WriteLineAsync("Процесс обработки данных начался");

            var configuration = CreateConfiguration();
            var section = configuration.GetSection(nameof(EtlServiceOptions));
            var options = section?.Get<EtlServiceOptions>();
            
            //setup our DI
            var services = new ServiceCollection();

            services.Configure<EtlServiceOptions>(section);

            services.AddLogging()
            .AddTransient<ServDbContext>()
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddTransient<ETLBase<UniversityRawData, University>, ETLService>()
            .AddHttpClient<ETLService>(clientAction =>
            {
                clientAction.BaseAddress = new Uri(options.SourceApiBaseUrl);
            });

            var serviceProvider = services.BuildServiceProvider();

            InitializeHelper.InitializeCountriesDb(options.Countries);

            var etlService = serviceProvider.GetService<ETLBase<UniversityRawData, University>>();
            await etlService.RunEtl();

            await Console.Out.WriteLineAsync("Процесс обработки данных завершился. Нажмите любую кнопку для завершения");
            Console.ReadLine();
        }

        private static IConfiguration CreateConfiguration()
        {
            var confBuilder = new ConfigurationBuilder();
            confBuilder.AddJsonFile("appsettings.json", false);
            return confBuilder.Build();
        }

    }
}
