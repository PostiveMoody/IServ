using IServ.ETL;

namespace IServ.API.DTO
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

                WebPageUrlAddresses = universityModel.WebPages,
                WebPageDomains = universityModel.Domains,
                    
                CreationDate = universityModel.CreationDate,
                UpdatedDate = universityModel.UpdatedDate,
                UniversityVersion = universityModel.UniversityVersion,
                IsDeleted = universityModel.IsDeleted,
            };
        }
    }
}
