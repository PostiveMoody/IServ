﻿using IServ.API.DTO;
using IServ.ETL;
using IServ.ETL.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IServ.API.Controllers
{
    [Route("[controller]")]
    public class UniversityController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UniversityController(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Публикация загруженных-сырых данных
        /// </summary>
        /// <param name="quatity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(string name, string country, int quatity = 100)
        {
            var querableRawData = _uow.UniversityRawDataRepository
                .UniversityRawDatas()
                .Take(quatity);

            if (!string.IsNullOrEmpty(country))
            {
                querableRawData = querableRawData.Where(universityRawData => universityRawData.Country == country);
            }

            if (!string.IsNullOrEmpty(name))
            {
                querableRawData = querableRawData.Where(universityRawData => universityRawData.Name == name);
            }

            var rawData = await querableRawData.ToListAsync();
            var result = new List<University>();
            var now = DateTime.Now;

            foreach (var data in rawData)
            {
                result.Add(University.Save(
                    data.Country,
                    data.Name,
                    string.Join("",data.WebPages)));

            }
            
            _uow.UniversityRepository.AddToContext(result);
            await _uow.SaveChangesAsync();

            return Ok(new PageDto<UniversityDto>()
            {
                Items = result.Select(university => university.ToDto()).ToArray(),
                TotalCount = result.Count,
            });
        }

        /// <summary>
        /// Получение университетов
        /// </summary>
        /// <param name="name"></param>
        /// <param name="country"></param>
        /// <param name="quatity"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Get(string name, string country, int quatity = 100)
        {
            var qData = _uow.UniversityRepository.Universities()
               .Take(quatity)
               .Select(university => new UniversityDto()
               {
                   UniversityId = university.UniversityId,
                   Country = university.Country,
                   Name = university.Name,

                   WebPageUrlAddresses = university.WebPages,
               });

            var t = new List<UniversityDto>();
            if (!string.IsNullOrEmpty(country))
            {
                qData = qData.Where(universityRawData => universityRawData.Country == country);
            }

            if (!string.IsNullOrEmpty(name))
            {
                qData = qData.Where(universityRawData => universityRawData.Name == name);
            }

            var result = qData.ToArray();

            return Ok(new PageDto<UniversityDto>()
            {
                Items = result,
                TotalCount = result.Length,
            });
        }

        [HttpGet]
        [Route("{universityId}")]
        public async Task<IActionResult> Get(int universityId)
        {
            var university = await _uow.UniversityRepository.Universities()
                .FirstOrDefaultAsync(university => university.UniversityId == universityId);

            if (university == null)
            {
                return NotFound();
            }

            return Ok(university.ToDto());
        }
    }
}
