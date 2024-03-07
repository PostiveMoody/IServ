using IServ.ETL.DAL;
using Newtonsoft.Json;

namespace IServ.ETL.Services
{
    public class ETLService : ETLBase
    {
        public static HttpClient client = new HttpClient();

        public ETLService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task Initialize(int numberThreads)
        {
            await ETL(numberThreads);
        }

        protected override async Task ETL(int numberThreads)
        {
            base.ETL(numberThreads);
        }

        protected override async Task<List<UniversityRawData>> Extract(
            int numberThreads)
        {
            var tasks = new List<Task<List<UniversityRawData>>>(numberThreads);
            var skip = 0;
            var take = numberThreads;

            // O(n)
            for (int j = 0; j < numberThreads; j++)
            {
                ServDbContext db = new ServDbContext();
                UnitOfWork unit = new UnitOfWork(db);
                ETLService extractService = new ETLService(unit);

                var countriesBatch = _countries
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

        protected override void Transofrm()
        {
            Console.WriteLine("Фантики");
        }

        protected override void Load()
        {
            Console.WriteLine("Фантики");
        }

        private async Task<List<UniversityRawData>> GetAsync(List<string> countries)
        {
            var result = new List<UniversityRawData>();

            foreach (var country in countries)
            {
                var uri = $"http://universities.hipolabs.com/search?country={country}";

                using (var response = await client.GetAsync(uri))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var universityRawDatas = JsonConvert.DeserializeObject<List<UniversityRawData>>(responseContent);

                        _uow.UniversityRawDataRepository.AddToContext(universityRawDatas);
                        await _uow.SaveChangesAsync();

                        result.AddRange(universityRawDatas);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Responce Status Code: {response.StatusCode}, smth went wrong");
                    }
                }
            }

            return result;
        }
    }
}
