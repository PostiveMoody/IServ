using IServ.ETL;
using IServ.ETL.DAL;
using IServ.ETL.Services;

namespace IServ.Start
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Guard Clause
            if(args.Length != 2)
                throw new ArgumentNullException(nameof(args));

            foreach (char letter in args[1])
            {
                // Guard from durak
                if (!char.IsDigit(letter))
                    throw new ArgumentException(nameof(args));
            }

            var countries = args[0].Split(',').ToList();
            int.TryParse(args[1], out var numberThreads);

            await Extract(numberThreads, countries);

            Console.ReadLine();
        }

        private static async Task<List<UniversityRawData>> Extract(
            int numberThreads,
            List<string> countries)
        {
            var tasks = new List<Task<List<UniversityRawData>>>(numberThreads);
            var skip = 0;
            var take = numberThreads;
            
            // O(n)
            for (int j = 0; j < numberThreads; j++)
            {
                ServDbContext db = new ServDbContext();
                UnitOfWork unit = new UnitOfWork(db);
                ExtractService extractService = new ExtractService(unit);

                var countriesBatch = countries
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var work = extractService.GetAsync(countriesBatch);
                tasks.Add(work);
                skip += numberThreads;
            }

            await Task.WhenAll(tasks.ToArray());

            var universityRawDatas = new List<UniversityRawData>();
            foreach (var task in tasks)
            {
                var result = task.Result;
                universityRawDatas.AddRange(result);
            }

            return universityRawDatas;
        }
    }
}
