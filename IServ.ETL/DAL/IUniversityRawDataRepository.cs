using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
{
    public interface IUniversityRawDataRepository
    {
        public DbSet<UniversityRawData> Universities();
        public void AddToContext(UniversityRawData universityRawData);
        public void AddToContext(List<UniversityRawData> universityRawDatas);
    }
}
