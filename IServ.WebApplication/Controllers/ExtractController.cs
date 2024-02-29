using Community.OData.Linq;
using IServ.DAL;
using IServ.WebApplication.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using University = IServ.Domain.University;

namespace IServ.WebApplication.Controllers
{
    [Route("[controller]")]
    public class ExtractController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConventionModelFactory _modelFactory;
        public static HttpClient client = new HttpClient();

        public ExtractController(
            IUnitOfWork uow,
            IConventionModelFactory modelFactory)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(string filter
            , string limit
            , string offset)
        {
            int.TryParse(limit, out var rlimit);
            if (rlimit > 100 || string.IsNullOrEmpty(limit))
            {
                rlimit = 100;
            }

            var uri = $"http://universities.hipolabs.com/search?filter={filter}&offset={offset}&limit={rlimit}";
            List<UniversityDto> universitiesDto = new List<UniversityDto>();
            List<University> universities = new List<University>(rlimit);

            using (var response = await client.GetAsync(uri))
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    universitiesDto = JsonConvert.DeserializeObject<List<UniversityDto>>(responseContent);

                    var now = DateTime.Now;

                    foreach (var universityDto in universitiesDto)
                    {
                        // some method to fill field like AlphaTwoCode, and to form the valid University
                        // or i'll add this method to domain layer

                        universities.Add(University.Save(now,
                            universityDto.AlphaTwoCode,
                            universityDto.Country,
                            universityDto.Name,
                            universityDto.SafeWebPageUrlAddresses.ToList(),
                            universityDto.SafeWebPageDomains.ToList(),
                            universityDto.StateProvince));
                    }

                    _uow.UniversityRepository.AddToContext(universities);
                    await _uow.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Responce Status Code: {response.StatusCode}, smth went wrong");
                }
            }

            var universityIds = universities.Select(university => university.UniversityId);

            var qData = _uow.UniversityRepository.Universities()
            .Where(university => university.IsDeleted == false && universityIds.Contains(universities.First().UniversityId))
            .Select(university => new UniversityDto()
            {
                UniversityId = university.UniversityId,
                AlphaTwoCode = university.AlphaTwoCode,
                StateProvince = university.StateProvince,
                Country = university.Country,
                Name = university.Name,

                WebPageUrlAddresses = university.WebPages.Select(webPage => webPage.WebPageUrlAddress).ToArray(),
                WebPageDomains = university.WebPageDomains.Select(webPageDomain => webPageDomain.WebPageDomainFullName).ToArray(),

                CreationDate = university.CreationDate,
                UpdatedDate = university.UpdatedDate,
                UniversityVersion = university.UniversityVersion,
                IsDeleted = university.IsDeleted,

            });

            var result = qData.ToArray();

            client.Dispose();

            return Ok(new PageDto<UniversityDto>()
            {
                Items = result,
                TotalCount = result.Length,
            });
        }
    }
}
