using Newtonsoft.Json;

namespace IServ.WebApi.Models
{
    public class UniversityViewModel
    {
        public int UniversityId { get; set; }
        public string AlphaTwoCode { get; set; }
        public string? StateProvince { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }

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
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UniversityVersion { get; set; }
        public bool IsDeleted { get; set; }
    }
}
