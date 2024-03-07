using IServ.ETL.Exceptions;

namespace IServ.WebApi.Сountry
{
    public class Country
    {
        public Country(string alphaTwoCode, string name)
        {
            if (string.IsNullOrEmpty(alphaTwoCode))
                throw new DomainException(nameof(alphaTwoCode) + " is empty!");
            if (string.IsNullOrEmpty(name))
                throw new DomainException(nameof(name) + " is empty!");

            AlphaTwoCode = alphaTwoCode;
            Name = name;
        }
        public int CountryId { get; private set; }
        public string AlphaTwoCode { get; private set; }
        public string Name { get; private set; }
    }
}
