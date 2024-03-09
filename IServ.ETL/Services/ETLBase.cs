using IServ.ETL.DAL;

namespace IServ.ETL.Services
{
    public abstract class ETLBase<TExtractModel, TLoadModel> 
        where TExtractModel : class 
        where TLoadModel : class
    {
        public virtual async Task RunEtl()
        {
            var rawData = await Extract();

            var transformData = Transofrm(rawData);

            await Load(rawData,transformData);
        }

        protected abstract Task<List<TExtractModel>> Extract();
        protected abstract List<TLoadModel> Transofrm(List<TExtractModel> rawData);
        protected abstract Task Load(List<TExtractModel> rawData, List<TLoadModel> transformData);
    }
}
