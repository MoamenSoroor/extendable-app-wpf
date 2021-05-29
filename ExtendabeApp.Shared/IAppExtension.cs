using System;

namespace ExtendabeApp.Shared
{
    public interface IAppExtension
    {
        public string Name { get; }

        object Content { get; }
    }

    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class CompanyInfoAttribute : Attribute
    {
        public CompanyInfoAttribute(string companyUrl,string companyName)
        {
            CompanyName = companyName;
            CompanyUrl = companyUrl;
        }

        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }

    }
}
