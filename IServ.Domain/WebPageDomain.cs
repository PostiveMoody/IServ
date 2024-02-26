using IServ.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace IServ.Domain
{
    public class WebPageDomain
    {
        public int WebPageDomainId { get; private set; }
        /// <summary>
        /// Полное доменное имя
        /// </summary>
        /// <remarks> https://www.microsoft.com/ru-ru</remarks>
        public string WebPageDomainFullName { get; private set; }
        /// <summary>
        /// Домен II уровня
        /// </summary>
        /// <remarks> .microsoft - домен второго уровня</remarks>
        public string WebPageDomainSecondLevel { get; private set; }
        /// <summary>
        /// Корневой домен - Домен I уровня
        /// </summary>
        /// <remarks> .com - домен корневого уровня</remarks>
        public string WebPageDomainRoot { get; private set; }

        /// <summary>
        /// Протокол
        /// </summary>
        /// <remarks> https:</remarks>
        public string? WebPageDomainProtocol { get; private set; }
        /// <summary>
        /// Домен IV уровня
        /// </summary>
        /// <remarks> </remarks>
        public string? WebPageDomainFourthLevel { get; private set; }
        /// <summary>
        /// Домен III уровня
        /// </summary>
        /// <remarks> /www - домен третьего уровня</remarks>
        public string? WebPageDomainThirdLevel { get; private set; }
       
        public static WebPageDomain Create(
            string webPageDomainFullName,
            string webPageDomainRoot,
            string webPageDomainSecondLevel)
        {
            // some validationqweqweqweqweqwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

            return new WebPageDomain
            {
                WebPageDomainFullName = webPageDomainFullName,
                WebPageDomainSecondLevel = webPageDomainSecondLevel,
                WebPageDomainRoot = webPageDomainRoot,
            };
        }

        public static WebPageDomain Create(
            string webPageDomainFullName,
            string webPageDomainRoot,
            string webPageDomainSecondLevel,
            string? webPageDomainProtocol = null,
            string? webPageDomainFourthLevel = null,
            string? webPageDomainThirdLevel = null)
        {
            // some validation ffdsfsdf233333333333333333333333333wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

            return new WebPageDomain
            {
                WebPageDomainFullName = webPageDomainFullName,
                WebPageDomainSecondLevel = webPageDomainSecondLevel,
                WebPageDomainRoot = webPageDomainRoot,
                WebPageDomainProtocol = webPageDomainProtocol,
                WebPageDomainFourthLevel = webPageDomainFourthLevel,
                WebPageDomainThirdLevel = webPageDomainThirdLevel,
            };
        }

        public static WebPageDomain SeparateDomainName(string webPageDomainFullName)
        {
            // Separate code
            var clearedStr = String.Join(";", Regex.Matches(webPageDomainFullName, @"\//(.+?)\/")
                                    .Cast<Match>()
                                    .Select(m => m.Groups[1].Value));

            var domainLevels = clearedStr.Split('.');

            if (domainLevels.Length == 0)
                throw new DomainException(nameof(webPageDomainFullName) + "invalid domain address");

            if(domainLevels.Length == 4)
            {

            }

            return new WebPageDomain
            {
                WebPageDomainFullName = webPageDomainFullName
            };
        }
    }
}
