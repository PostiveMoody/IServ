namespace IServ.WebApi.DAL
{
    public interface IUnitOfWork
    {
        IUniversityRepository UniversityRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
