using IServ.Domain;
using Microsoft.EntityFrameworkCore;

namespace IServ.DAL.Repositories
{
    public interface IUniversityRepository
    {
        public DbSet<University> Universities();
        public void AddToContext(University university);
    }
}
