﻿using IServ.Domain.Exceptions;

namespace IServ.Domain
{
    public class University
    {
        public int UniversityId { get; private set; }
        public string AlphaTwoCode { get; private set; }
        public string? StateProvince { get; private set; }
        public string Country { get; private set; }
        public string Name { get; private set; }
        public ICollection<WebPage> WebPages { get; private set; }
        public ICollection<WebPageDomain> WebPageDomains { get; private set; }

        public DateTime CreationDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public int UniversityVersion { get; private set; }
        public bool IsDeleted { get; private set; }

        public static University Save(
            DateTime now,
            string alphaTwoCode,
            string country,
            string name,
            ICollection<string> webPageUrlAddresses,
            ICollection<string> webPageDomains,
            string? stateProvince = null)
        {
            if (string.IsNullOrEmpty(alphaTwoCode))
                throw new DomainException(nameof(alphaTwoCode) + " is empty!");
            if (string.IsNullOrEmpty(country))
                throw new DomainException(nameof(country) + " is empty!");
            if (string.IsNullOrEmpty(name))
                throw new DomainException(nameof(name) + " is empty!");
            if (webPageUrlAddresses.Count == 0)
                throw new DomainException(nameof(webPageUrlAddresses) + " collection contains 0 items!");
            if (webPageDomains.Count == 0)
                throw new DomainException(nameof(webPageDomains) + " collection contains 0 items!");

            var webPageUrlList = new List<WebPage>(webPageUrlAddresses.Count);
            foreach(var webPageUrlAddress in webPageUrlAddresses)
            {
                webPageUrlList.Add(WebPage.Create(webPageUrlAddress));
            }

            var webPageDomainList = new List<WebPageDomain>(webPageDomains.Count);
            foreach (var webPageDomain in webPageDomains)
            {
                webPageDomainList.Add(WebPageDomain.Create(
                    webPageDomain,
                    SeparateDomainName(webPageDomain).WebPageDomainRoot,
                    SeparateDomainName(webPageDomain).WebPageDomainSecondLevel));
            }

            return new University()
            {
                AlphaTwoCode = alphaTwoCode,
                StateProvince = stateProvince,
                Country = country,
                Name = name,
                WebPages = webPageUrlList,
                WebPageDomains = webPageDomainList,

                CreationDate = now,
                UpdatedDate = now,
                UniversityVersion = 1,
                IsDeleted = false,
            };
        }

        private static WebPageDomain SeparateDomainName(string webPageDomainFullName)
        {
            return WebPageDomain.SeparateDomainName(webPageDomainFullName);
        }

        [Obsolete("Использовался для отладки",true)]
        public static University DebugSave(string webPageDomainFullName)
        {
            return new University
            {
                AlphaTwoCode = "RU",
                Country = "Russian Federation",
                Name = "St.Petersburg State Conservatory",

                WebPages = new[]
                {
                    WebPage.Create(
                        "http://www.conservatory.ru/",
                        "Conservatory")
                },

                WebPageDomains = new[] 
                {
                    WebPageDomain.Create(
                        webPageDomainFullName,
                        SeparateDomainName(webPageDomainFullName).WebPageDomainRoot,
                        SeparateDomainName(webPageDomainFullName).WebPageDomainSecondLevel
                        )
                }
            };
        }
    }
}