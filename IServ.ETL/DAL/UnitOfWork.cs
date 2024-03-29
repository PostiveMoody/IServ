﻿namespace IServ.ETL.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ServDbContext _context;

        public UnitOfWork(ServDbContext context)
        {
            _context = context;
        }

        public IUniversityRepository UniversityRepository
        {
            get
            {
                return new UniversityRepository(_context);
            }
        }

        public IUniversityRawDataRepository UniversityRawDataRepository
        {
            get
            {
                return new UniversityRawDataRepository(_context);
            }
        }

        public ICountryRepository CountryRepository
        {
            get
            {
                return new CountryRepository(_context);
            }
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
