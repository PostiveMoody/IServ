namespace IServ.ETL
{
    /// <summary>
    /// Страна
    /// </summary>
    public class Country
    {
        public int CountryId { get; private set; }
        public string Name { get; set; }

        public static Country Create(string name)
        {
            return new Country()
            {
                Name = name,
            };
        }
    }
}
