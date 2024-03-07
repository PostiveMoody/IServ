using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
{
    public interface ICountryRepository
    {
        public DbSet<Country> Countries();
        public void AddToContext(Country сountry);
        public void AddToContext(List<Country> сountries);
    }
}
