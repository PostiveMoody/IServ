using IServ.ETL;
using Microsoft.EntityFrameworkCore;

namespace IServ.WebApi.DAL
{
    public interface IUniversityRepository
    {
        public DbSet<University> Universities();
        public void AddToContext(University university);

        public void AddToContext(List<University> universities);
    }
}
