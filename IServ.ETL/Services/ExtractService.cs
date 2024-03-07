using IServ.ETL.DAL;
using Newtonsoft.Json;

namespace IServ.ETL.Services
{
    public class ExtractService
    {
        private readonly IUnitOfWork _uow;
        public static HttpClient client = new HttpClient();

        private static int quantityCountries = 10;
        private static List<string> _countries = new List<string>(quantityCountries);

        public ExtractService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task Extarct(int numberThreads)
        {
            GetCountries();
            await Extract(numberThreads);
        }

        public void GetCountries()
        {
            if (InitializeHelper.Where)
            {
                _countries = _uow.CountryRepository
                    .Countries().Select(it => it.Name).Take(quantityCountries).ToList();
            }
            else
            {
                try
                {
                    using (var sr = new StreamReader(InitializeHelper.pathToJsonWithCountries))
                    {
                        var result = sr.ReadToEnd();
                        _countries = result.Split(',').ToList();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task<List<UniversityRawData>> GetAsync(List<string> countries)
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
       
        private static async Task<List<UniversityRawData>> Extract(
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
                ExtractService extractService = new ExtractService(unit);

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

        
    }
}
