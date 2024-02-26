using IServ.Domain;
using Microsoft.EntityFrameworkCore;

namespace IServ.DAL.Repositories
{
    public class UniversityRepository : Repository<University>, IUniversityRepository
    {
        public ServDbContext dbContext;

        public UniversityRepository(ServDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public DbSet<University> Universities()
        {
            return this.dbContext.Universities;
        }

        public void AddToContext(University university)
        {
            this.dbContext.Add(university);
        }
    }
}
