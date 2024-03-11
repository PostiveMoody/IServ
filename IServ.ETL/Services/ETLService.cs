using IServ.ETL.DAL;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IServ.ETL.Services
{
    public class ETLService : ETLBase<UniversityRawData, University>
    {
        protected readonly IUnitOfWork _uow;
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<EtlServiceOptions> _options;

        protected static int quantityCountries = 10;

        public ETLService(IUnitOfWork uow, HttpClient httpClient, IOptionsMonitor<EtlServiceOptions> options)
        {
            _uow = uow;
            _httpClient = httpClient;
            _options = options;
        }

        protected override async Task<List<UniversityRawData>> Extract()
        {
            string[] countries = _options.CurrentValue.Countries;
            var threadsCount = _options.CurrentValue.ThreadNumbers;

            var tasks = new Task<List<UniversityRawData>>[threadsCount];
            var skip = 0;
            var take = countries.Length / threadsCount + 1;

            for (int j = 0; j < threadsCount; j++)
            {
                var countriesBatch = countries
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var work = GetAsync(countriesBatch);
                tasks[j] = work;
                skip += take;
            }

            await Task.WhenAll(tasks);

            var universityRawDatas = new List<UniversityRawData>();
            foreach (var task in tasks)
            {
                var result = task.Result;
                universityRawDatas.AddRange(result);
            }

            return universityRawDatas;
        }

        protected override List<University> Transofrm(List<UniversityRawData> universityRawDatas)
        {
            List<University> result = new List<University>(universityRawDatas.Count);
           
            for (int i = 0; i < universityRawDatas.Count; i++)
            {
                var webPages = string.Join(";", universityRawDatas[i].WebPages);
                var university = University.Save(universityRawDatas[i].Country, universityRawDatas[i].Name, webPages);
                result.Add(university);
            }

            return result;
        }

        protected override async Task Load(List<UniversityRawData> rawData, List<University> transformData)
        {
            _uow.UniversityRawDataRepository.AddToContext(rawData);
            _uow.UniversityRepository.AddToContext(transformData);
            await _uow.SaveChangesAsync();
        }

        private async Task<List<UniversityRawData>> GetAsync(List<string> countries)
        {
            var result = new List<UniversityRawData>();

            foreach (var country in countries)
            {
                var url = $"http://universities.hipolabs.com/search?country={country}";

                using (var response = await _httpClient.GetAsync(url))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var universityRawDatas = JsonConvert.DeserializeObject<List<UniversityRawData>>(responseContent);
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


