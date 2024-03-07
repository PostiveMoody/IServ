namespace IServ.ETL.DAL
{
    public interface IUnitOfWork
    {
        IUniversityRepository UniversityRepository { get; }
        IUniversityRawDataRepository UniversityRawDataRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
