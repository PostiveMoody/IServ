﻿using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
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

        public void AddToContext(List<University> universities)
        {
            foreach (var university in universities)
            {
                this.dbContext.Add(university);
            }
        }
    }
}
