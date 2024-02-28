using IServ.Domain.Exceptions;
using System.Text.RegularExpressions;

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
            if (!IsUrlValid(webPageUrlAddress))
                throw new DomainException(nameof(webPageUrlAddress) + " param shoud be a URL");

            return new WebPage
            {
                WebPageUrlAddress = webPageUrlAddress,
                WebPageName = webPageName
            };
        }

        private static bool IsUrlValid(string url)
        {

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
    }
}
