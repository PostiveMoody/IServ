using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
{
    public interface IUniversityRepository
    {
        public DbSet<University> Universities();
        public void AddToContext(University university);
        public void AddToContext(List<University> universities);
    }
}
