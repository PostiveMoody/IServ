using IServ.DAL.Repositories;

namespace IServ.DAL
{
    public interface IUnitOfWork
    {
        IUniversityRepository UniversityRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
