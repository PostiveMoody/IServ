using IServ.DAL.Repositories;

namespace IServ.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ServDbContext _context;
        public IUniversityRepository UniversityRepository
        {
            get
            {
                return new UniversityRepository(_context);
            }
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
