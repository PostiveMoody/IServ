using IServ.DAL;
using IServ.Domain;
using IServ.WebApplication.Dto;
using Microsoft.AspNetCore.Mvc;

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
        [Route("save")]
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

        ///// <summary>
        ///// https://stackoverflow.com/questions/48743165/toarrayasync-throws-the-source-iqueryable-doesnt-implement-iasyncenumerable
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <param name="top"></param>
        ///// <param name="skip"></param>
        ///// <param name="orderby"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("list")]
        //public Task<IActionResult> Get(string filter
        //    , string top
        //    , string skip
        //    , string orderby)
        //{
        //    var qData = _uow.RequestCardRepository.RequestCards()
        //       .Where(requestCard => requestCard.IsDeleted == false)
        //       .Select(requestCard => new RequestCardDto()
        //       {
        //           RequestCardId = requestCard.RequestCardId,
        //           Initiator = requestCard.Initiator,
        //           SubjectOfAppeal = requestCard.SubjectOfAppeal,
        //           Description = requestCard.Description,
        //           DeadlineForHiring = requestCard.DeadlineForHiring,
        //           Status = requestCard.Status,
        //           Category = requestCard.Category,
        //           CreationDate = requestCard.CreationDate,
        //           RequestCardVersion = requestCard.RequestCardVersion,
        //           IsDeleted = requestCard.IsDeleted
        //       }).OData(edmModel: _modelFactory.CreateOrGet());

        //    if (!string.IsNullOrEmpty(filter))
        //    {
        //        qData = qData.Filter(filter);
        //    }

        //    var totalCount = qData.Count();

        //    if (!string.IsNullOrEmpty(orderby))
        //    {
        //        qData = qData.OrderBy(orderby);
        //    }

        //    if (!string.IsNullOrEmpty(top) && !string.IsNullOrEmpty(skip))
        //    {
        //        qData = qData.TopSkip(top, skip);
        //    }

        //    var result = qData.ToArray();

        //    return new PageDto<RequestCardDto>()
        //    {
        //        Items = result,
        //        TotalCount = totalCount,
        //    }.ToApiResult();
        //}

        //[HttpGet]
        //[Route("{requestCardId}")]
        //public async Task<IActionResult> Get(int requestCardId)
        //{
        //    var requestCard = await _uow.RequestCardRepository.RequestCards()
        //        .FirstOrDefaultAsync(requestCard => requestCard.RequestCardId == requestCardId);

        //    if (requestCard == null || requestCard.IsDeleted)
        //    {
        //        return ApiResult<RequestCardDto>.CreateFailed(
        //            Errors.NotFound.Code,
        //            $"RequestCard # {requestCardId} nof found.");
        //    }

        //    return requestCard
        //        .ToDto()
        //        .ToApiResult();
        //}

        //[HttpDelete]
        //[Route("{requestCardId}")]
        //public async Task<IActionResult> Delete(int requestCardId, int requestCardVersion)
        //{
        //    var requestCard = await _uow.RequestCardRepository.RequestCards()
        //        .FirstOrDefaultAsync(requestCard => requestCard.RequestCardId == requestCardId);

        //    if (requestCard == null || requestCard.IsDeleted)
        //    {
        //        return ApiResult<RequestCardDto>.CreateFailed(
        //            Errors.NotFound.Code,
        //            $"RequestCard # {requestCardId} nof found.");
        //    }

        //    var now = DateTime.Now;
        //    requestCard.Delete(now, requestCardVersion);
        //    await _uow.SaveChangesAsync();

        //    return requestCard
        //       .ToDto()
        //       .ToApiResult();
        //}

        //[HttpPut]
        //[Route("{requestCardId}")]
        //public async Task<IActionResult> Put(int requestCardId,
        //    [FromBody] RequestCardUpdateOptionsDto options)
        //{
        //    if (options == null || options.DeadlineForHiring == default)
        //        throw new ArgumentNullException(nameof(options));

        //    var requestCard = await _uow.RequestCardRepository.RequestCards()
        //        .FirstOrDefaultAsync(requestCard => requestCard.RequestCardId == requestCardId);

        //    if (requestCard == null || requestCard.IsDeleted)
        //    {
        //        return ApiResult<RequestCardDto>.CreateFailed(
        //            Errors.NotFound.Code,
        //            $"RequestCard # {requestCardId} nof found.");
        //    }

        //    var now = DateTime.Now;

        //    requestCard.Update(
        //        now,
        //        options.Initiator,
        //        options.SubjectOfAppeal,
        //        options.Description,
        //        options.DeadlineForHiring,
        //        options.Category,
        //        options.RequestCardVersion);

        //    await _uow.SaveChangesAsync();

        //    return requestCard
        //       .ToDto()
        //       .ToApiResult();
        //}

        //[HttpGet]
        //public IActionResult SaveRequestCard()
        //{
        //    return View();
        //}
        ///// <summary>
        ///// Метод создан в целях показать как работает клиентская валидация в ASP.NET Core
        ///// </summary>
        ///// <param name="creationOptions"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //[HttpPost]
        //public async Task<IActionResult> SaveRequestCard([FromForm] RequestCardCreationOptionsDto creationOptions)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(creationOptions);
        //    }

        //    if (creationOptions == null)
        //        throw new ArgumentNullException(nameof(creationOptions));

        //    var now = DateTime.Now;

        //    var requestCard = RequestCard.Save(
        //        now,
        //        creationOptions.Initiator,
        //        creationOptions.SubjectOfAppeal,
        //        creationOptions.Description,
        //        creationOptions.DeadlineForHiring,
        //        creationOptions.Category);

        //    _uow.RequestCardRepository.AddToContext(requestCard);
        //    await _uow.SaveChangesAsync();

        //    return View();
        //}
    }
}
