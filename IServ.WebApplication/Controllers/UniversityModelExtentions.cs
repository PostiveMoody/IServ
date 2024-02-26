using IServ.Domain;
using IServ.WebApplication.Dto;


namespace IServ.WebApplication.Controllers
{
    public static class UniversityModelExtentions
    {
        public static UniversityDto ToDto(this University universityModel)
        {
            return new UniversityDto()
            {
                UniversityId = universityModel.UniversityId,
                AlphaTwoCode = universityModel.AlphaTwoCode,
                StateProvince = universityModel.StateProvince,
                Country = universityModel.Country,
                Name = universityModel.Name,

                WebPageUrlAddresses = universityModel.WebPages
                    .Select(webPage => webPage.WebPageUrlAddress)
                    .ToArray(),
                WebPageDomains = universityModel.WebPageDomains
                    .Select(webPageDomain => webPageDomain.WebPageDomainFullName)
                    .ToArray(),

                CreationDate = universityModel.CreationDate,
                UpdatedDate = universityModel.UpdatedDate,
                UniversityVersion = universityModel.UniversityVersion,
                IsDeleted = universityModel.IsDeleted,
            };
        }
    }
}
