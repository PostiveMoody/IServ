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
                Country = universityModel.Country,
                Name = universityModel.Name,

                WebPageUrlAddresses = universityModel.WebPages,
            };
        }
    }
}
