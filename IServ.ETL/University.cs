using Newtonsoft.Json;

namespace IServ.ETL
{
    /// <summary>
    /// Университеты
    /// </summary>
    public class University
    {
        public int UniversityId { get; private set; }
        public string? AlphaTwoCode { get; private set; }
        public string? StateProvince { get; private set; }
        public string Country { get; private set; }
        public string Name { get; private set; }

        public string[] WebPages { get; set; }
        public string[] Domains { get; set; }

        public DateTime CreationDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public int UniversityVersion { get; private set; }
        public bool IsDeleted { get; private set; }

        public static University Save(
            DateTime now,
            string alphaTwoCode,
            string country,
            string name,
            string[] webPages,
            string[] domains,
            string? stateProvince = null)
        {
            //if (string.IsNullOrEmpty(alphaTwoCode))
            //    throw new DomainException(nameof(alphaTwoCode) + " is empty!");
            //if (alphaTwoCode.Length != 2)
            //    throw new DomainException(nameof(alphaTwoCode) + " is invalid!");
            //if (string.IsNullOrEmpty(country))
            //    throw new DomainException(nameof(country) + " is empty!");
            //if (string.IsNullOrEmpty(name))
            //    throw new DomainException(nameof(name) + " is empty!");
            //if (webPageUrlAddresses.Count == 0)
            //    throw new DomainException(nameof(webPageUrlAddresses) + " collection contains 0 items!");
            //if (webPageDomains.Count == 0)
            //    throw new DomainException(nameof(webPageDomains) + " collection contains 0 items!");
      
            return new University()
            {
                AlphaTwoCode = alphaTwoCode,
                StateProvince = stateProvince,
                Country = country,
                Name = name,

                WebPages = webPages,
                Domains = domains,

                CreationDate = now,
                UpdatedDate = now,
                UniversityVersion = 1,
                IsDeleted = false,
            };
        }

        public void Update(
            DateTime now,
            string alphaTwoCode,
            string country,
            string name,
            string[] webPages,
            string[] domains,
            int universityVersion,
            string? stateProvince = null)
        {

            CheckVersion(universityVersion);

            //if (string.IsNullOrEmpty(alphaTwoCode))
            //    throw new DomainException(nameof(alphaTwoCode) + " is empty!");
            //if (alphaTwoCode.Length != 2)
            //    throw new DomainException(nameof(alphaTwoCode) + " is invalid!");
            //if (string.IsNullOrEmpty(country))
            //    throw new DomainException(nameof(country) + " is empty!");
            //if (string.IsNullOrEmpty(name))
            //    throw new DomainException(nameof(name) + " is empty!");
            //if (webPageUrlAddresses.Count == 0)
            //    throw new DomainException(nameof(webPageUrlAddresses) + " collection contains 0 items!");
            //if (webPageDomains.Count == 0)
            //    throw new DomainException(nameof(webPageDomains) + " collection contains 0 items!");

            this.AlphaTwoCode = alphaTwoCode;
            this.StateProvince = stateProvince;
            this.Country = country;
            this.Name = name;

            this.WebPages = webPages;
            this.Domains = domains;

            this.CreationDate = now;
            this.UpdatedDate = now;
            this.UniversityVersion += 1;
        }

        public void Delete(DateTime now, int universityVersion)
        {
            CheckVersion(universityVersion);

            this.IsDeleted = true;
            this.UpdatedDate = now;
            this.UniversityVersion += 1;
        }

        private void CheckVersion(int universityVersion)
        {
            if (universityVersion != this.UniversityVersion)
            {
                //throw new AlreadyChangedException(
                //    $"The state of university # {this.UniversityId} has been changed by another user");
            }
        } 
    }
}
