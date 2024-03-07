using IServ.ETL.DAL;

namespace IServ.ETL.Services
{
    public class TransformService
    {
        private readonly IUnitOfWork _uow;

        public TransformService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
    }
}
