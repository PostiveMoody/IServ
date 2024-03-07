using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public ServDbContext dbContext;

        public CountryRepository(ServDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddToContext(Country сountry)
        {
            this.dbContext.Add(сountry);
        }

        public void AddToContext(List<Country> сountries)
        {
            foreach (var сountry in сountries)
            {
                this.dbContext.Add(сountry);
            }
        }

        public DbSet<Country> Countries()
        {
            return this.dbContext.Countries;
        }
    }
}
