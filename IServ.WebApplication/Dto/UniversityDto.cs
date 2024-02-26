namespace IServ.WebApplication.Dto
{
    public class UniversityDto
    {
        public int UniversityId { get; set; }
        public string AlphaTwoCode { get; set; }
        public string? StateProvince { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
       
        public string[] WebPageUrlAddresses { get; set; }
        public string[] WebPageDomains { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UniversityVersion { get; set; }
        public bool IsDeleted { get; set; }
    }
}
