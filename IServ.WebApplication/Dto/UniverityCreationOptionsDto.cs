using Newtonsoft.Json;

namespace IServ.WebApplication.Dto
{
    public class UniverityCreationOptionsDto
    {
        [JsonProperty("alphaTwoCode")]
        public string AlphaTwoCode { get; set; }

        [JsonProperty("stateProvince")]
        public string? StateProvince { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("webPageUrlAddresses")]
        public string[] WebPageUrlAddresses { get; set; }

        [JsonIgnore]
        public IEnumerable<string> SafeWebPageUrlAddresses
        {
            get
            {
                return this.WebPageUrlAddresses == null
                    ? Enumerable.Empty<string>()
                    : this.WebPageUrlAddresses.ToList();
            }
        }

        [JsonProperty("webPageDomains")]
        public string[] WebPageDomains { get; set; }

        [JsonIgnore]
        public IEnumerable<string> SafeWebPageDomains
        {
            get
            {
                return this.WebPageUrlAddresses == null
                    ? Enumerable.Empty<string>()
                    : this.WebPageUrlAddresses.ToList();
            }
        }
    }
}
