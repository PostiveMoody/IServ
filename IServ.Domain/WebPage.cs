using System.Diagnostics;

namespace IServ.Domain
{
    public class WebPage
    {
        public int WebPageId { get; private set; }
        public string WebPageUrlAddress { get; private set; }
        public string? WebPageName { get; private set; }

        public static WebPage Create(
            string webPageUrlAddress,
            string? webPageName = null)
        {
            // some validation 45555555555555555555555555555555555566666666666543w342342rfgd

            return new WebPage
            {
                WebPageUrlAddress = webPageUrlAddress,
                WebPageName = webPageName
            };
        }
    }
}
