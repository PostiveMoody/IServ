using IServ.ETL.DAL;


namespace IServ.ETL.Services
{
    public static class InitializeHelper
    {
        public const string countriesInitialData = "Russian+Federation,Canada,Brazil,Ukraine,Chile,France,China,United+Kingdom,India,United+States";

        /// <summary>
        /// Инциализирует-заполняет список стран в БД
        /// </summary>
        public static void InitializeCountriesDb()
        {
            var countries = countriesInitialData.Split(',').ToList();
            ServDbContext db = new ServDbContext();
            UnitOfWork unit = new UnitOfWork(db);

            List<Country> result = new List<Country>();

            foreach (var country in countries)
            {
                result.Add(Country.Create(country));
            }

            unit.CountryRepository.AddToContext(result);
            unit.SaveChanges();
        }
    }
}
