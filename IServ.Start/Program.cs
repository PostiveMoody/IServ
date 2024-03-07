using IServ.ETL.DAL;
using IServ.ETL.Services;

namespace IServ.Start
{
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"> Первый arg - количество потоков, Второй arg - Флаг БД или Файл</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        static async Task Main(string[] args)
        {
            // Guard Clause
            if(args.Length != 2)
                throw new ArgumentNullException(nameof(args));

            foreach (char letter in args[0])
            {
                // Guard from durak
                if (!char.IsDigit(letter))
                    throw new ArgumentException(nameof(args));
            }

            if (!bool.TryParse(args[1], out bool result))
                throw new ArgumentException(nameof(args));

            InitializeHelper.Initialize(result);

            int.TryParse(args[0], out var numberThreads);

            ServDbContext db = new ServDbContext();
            UnitOfWork unit = new UnitOfWork(db);
            ETLService etlService = new ETLService(unit);
            await etlService.Initialize(numberThreads);

            Console.ReadLine();
        }
    }
}
