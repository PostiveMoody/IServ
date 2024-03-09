namespace IServ.ETL
{
    /// <summary>
    /// Университеты
    /// </summary>
    public class University
    {
        public int UniversityId { get; private set; }
        public string Country { get; private set; }
        public string Name { get; private set; }
        public string WebPages { get; set; }

        public static University Save(
            string country,
            string name,
            string webPages)
        {
            return new University()
            {
                Country = country,
                Name = name,

                WebPages = webPages,
            };
        }
    }
}
