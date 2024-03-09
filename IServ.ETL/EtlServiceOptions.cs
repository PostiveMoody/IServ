namespace IServ.ETL
{
    public class EtlServiceOptions
    {
        public int ThreadNumbers { get; set; }
        public string SourceApiBaseUrl { get; set; }
        public string[] Countries {  get; set; }
    }
}
