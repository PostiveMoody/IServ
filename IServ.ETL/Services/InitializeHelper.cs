using IServ.ETL.DAL;


namespace IServ.ETL.Services
{
    /// <summary>
    /// Придумал убогий класс, чтобы вспомнить все :)
    /// </summary>
    public static class InitializeHelper
    {
        public const string countriesInitialData = "Russian+Federation,Canada,Brazil,Ukraine,Chile,France,China,United+Kingdom,India,United+States";
        public const string pathToJsonWithCountries = "D:\\IServ\\TechinicalSpecification\\IServ\\IServ.Start\\Countries.json";

        /// <summary>
        /// True - в БД, False - в файл
        /// </summary>
        public static bool Where {  get; private set; }

        /// <summary>
        /// Инициализирует-заполняет список
        /// </summary>
        /// <param name="where">True - в БД, False - в файл</param>
        public static void Initialize(bool where)
        {
            Where = where;

            if (where)
            {
                InitializeCountriesDb();
            }
            else
            {
                InitializeCountriesFile();
            }
        }

        /// <summary>
        /// Инциализирует-заполняет список стран в файл
        /// </summary>
        private static void InitializeCountriesFile()
        {
            if (!File.Exists(pathToJsonWithCountries))
                throw new InvalidOperationException("Сам создал там файл и сделал проверку :) ");

            File.WriteAllText(pathToJsonWithCountries,countriesInitialData);
        }

        /// <summary>
        /// Инциализирует-заполняет список стран в БД
        /// </summary>
        private static void InitializeCountriesDb()
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
