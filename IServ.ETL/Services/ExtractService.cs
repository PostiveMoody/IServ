using IServ.ETL.DAL;
using Newtonsoft.Json;

namespace IServ.ETL.Services
{
    public class ExtractService
    {
        private readonly IUnitOfWork _uow;
        public static HttpClient client = new HttpClient();

        public ExtractService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
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
    }
}
