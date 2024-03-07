using IServ.ETL.DAL;

namespace IServ.ETL.Services
{
    public abstract class ETLBase
    {
        protected static int quantityCountries = 10;
        protected static List<string> _countries = new List<string>(quantityCountries);
        protected readonly IUnitOfWork _uow;

        public ETLBase(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        protected virtual async Task ETL(int numberThreads)
        {
            GetCountries();

            var universityRawDatas = await Extract(numberThreads);

            var transformData = Transofrm(universityRawDatas);

            await Load(transformData);
        }

        protected abstract Task<List<UniversityRawData>> Extract(int numberThreads);
        protected abstract List<UniversityRawData> Transofrm(List<UniversityRawData> universityRawDatas);
        protected abstract Task Load(List<UniversityRawData> universityRawDatas);

        public void GetCountries()
        {
            if (InitializeHelper.Where)
            {
                _countries = _uow.CountryRepository
                    .Countries().Select(it => it.Name).Take(quantityCountries).ToList();
            }
            else
            {
                try
                {
                    using (var sr = new StreamReader(InitializeHelper.pathToJsonWithCountries))
                    {
                        var result = sr.ReadToEnd();
                        _countries = result.Split(',').ToList();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
