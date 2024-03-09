using IServ.ETL.DAL;

namespace IServ.ETL.Services
{
    public static class InitializeHelper
    {
        /// <summary>
        /// Инциализирует-заполняет список стран в БД
        /// </summary>
        public static void InitializeCountriesDb(string[] countriesInitialData)
        {
            var countries = countriesInitialData;
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
