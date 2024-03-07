using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
{
    public class UniversityRawDataRepository : Repository<UniversityRawData>, IUniversityRawDataRepository
    {
        public ServDbContext dbContext;

        public UniversityRawDataRepository(ServDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public DbSet<UniversityRawData> Universities()
        {
            return this.dbContext.UniversityRawDatas;
        }

        public void AddToContext(UniversityRawData university)
        {
            this.dbContext.Add(university);
        }

        public void AddToContext(List<UniversityRawData> universities)
        {
            foreach (var university in universities)
            {
                this.dbContext.Add(university);
            }
        }
    }
}

