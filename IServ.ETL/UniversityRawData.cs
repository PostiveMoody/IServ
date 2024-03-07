using Newtonsoft.Json;

namespace IServ.ETL
{
    /// <summary>
    /// Университеты - сырые данные
    /// </summary>
    public class UniversityRawData
    {
        public UniversityRawData()
        {

        }
        public UniversityRawData(
            string сountry,
            string name,
            string[] webPageUrlAddresses,
            string[] webPageDomains,
            string? alphaTwoCode,
            string? stateProvince)
        {
            Name = name;
            Country = сountry;
            WebPages = webPageUrlAddresses;
            Domains = webPageDomains;
            AlphaTwoCode = alphaTwoCode;
            StateProvince = stateProvince;
        }
        public int UniversityRawDataId { get; set; }

        [JsonProperty("alpha_two_code")]
        public string? AlphaTwoCode { get; set; }
        [JsonProperty("state-province")]
        public string? StateProvince { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("web_pages")]
        public string[] WebPages { get; set; }
        [JsonProperty("domains")]
        public string[] Domains { get; set; }
    }
}
