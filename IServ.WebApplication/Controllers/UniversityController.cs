using Community.OData.Linq;
using IServ.DAL;
using IServ.Domain;
using IServ.WebApplication.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IServ.WebApplication.Controllers
{
    [Route("[controller]")]
    public class UniversityController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConventionModelFactory _modelFactory;

        public UniversityController(
            IUnitOfWork uow,
            IConventionModelFactory modelFactory)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        }

        /// <summary>
        /// Создать карточку обращения
        /// </summary>
        /// <param name="creationOptions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] UniverityCreationOptionsDto creationOptions)
        {
            if (creationOptions == null || string.IsNullOrEmpty(creationOptions.AlphaTwoCode))
                throw new ArgumentNullException(nameof(creationOptions));

            var now = DateTime.Now;

            var university = University.Save(
               now,
               creationOptions.AlphaTwoCode,
               creationOptions.Country,
               creationOptions.Name,
               creationOptions.SafeWebPageUrlAddresses.ToList(),
               creationOptions.SafeWebPageDomains.ToList(),
               creationOptions.StateProvince);

            _uow.UniversityRepository.AddToContext(university);
            await _uow.SaveChangesAsync();

            return Ok(university.ToDto());
        }


        // <summary>
        /// https://stackoverflow.com/questions/48743165/toarrayasync-throws-the-source-iqueryable-doesnt-implement-iasyncenumerable
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Get(string filter
            , string top
            , string skip
            , string orderby)
        {
            var qData = _uow.UniversityRepository.Universities()
               .Where(university => university.IsDeleted == false)
               .Include(u => u.WebPages)
               .Include(u => u.WebPageDomains)
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

               }).OData(edmModel: _modelFactory.CreateOrGet());

            if (!string.IsNullOrEmpty(filter))
            {
                qData = qData.Filter(filter);
            }

            var totalCount = qData.Count();

            if (!string.IsNullOrEmpty(orderby))
            {
                qData = qData.OrderBy(orderby);
            }

            if (!string.IsNullOrEmpty(top) && !string.IsNullOrEmpty(skip))
            {
                qData = qData.TopSkip(top, skip);
            }

            var result = qData.ToArray();

            return Ok(new PageDto<UniversityDto>()
            {
                Items = result,
                TotalCount = totalCount,
            });
        }

        [HttpGet]
        [Route("{universityId}")]
        public async Task<IActionResult> Get(int universityId)
        {
            var university = await _uow.UniversityRepository.Universities()
                .Include(u => u.WebPages)
                .Include(u => u.WebPageDomains)
                .FirstOrDefaultAsync(university => university.UniversityId == universityId);

            if (university == null || university.IsDeleted)
            {
                return NotFound();
            }

            return Ok(university.ToDto());     
        }

        [HttpDelete]
        [Route("{universityId}")]
        public async Task<IActionResult> Delete(int universityId, int universityIdVersion)
        {
            var university = await _uow.UniversityRepository.Universities()
                .Include(u => u.WebPages)
                .Include(u => u.WebPageDomains)
                .FirstOrDefaultAsync(university => university.UniversityId == universityId);

            if (university == null || university.IsDeleted)
            {
                return NotFound();
            }

            var now = DateTime.Now;
            university.Delete(now, universityIdVersion);
            await _uow.SaveChangesAsync();

            return Ok(university.ToDto());
        }

        [HttpPut]
        [Route("{universityId}")]
        public async Task<IActionResult> Put(int universityId,
            [FromBody] UniversityUpdateOptionsDto options)
        {
            if (options == null || string.IsNullOrEmpty(options.AlphaTwoCode))
                throw new ArgumentNullException(nameof(options));

            var university = await _uow.UniversityRepository.Universities()
                .Include(u => u.WebPages)
                .Include(u => u.WebPageDomains)
                .FirstOrDefaultAsync(university => university.UniversityId == universityId);

            if (university == null || university.IsDeleted)
            {
                return NotFound();
            }

            var now = DateTime.Now;

            university.Update(
                now,
                options.AlphaTwoCode,
                options.Country,
                options.Name,
                options.SafeWebPageUrlAddresses.ToList(),
                options.SafeWebPageDomains.ToList(),
                options.UniversityVersion,
                options.StateProvince);

            await _uow.SaveChangesAsync();

            return Ok(university.ToDto());
        }
    }
}
