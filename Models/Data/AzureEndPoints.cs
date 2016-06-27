using Microsoft.Extensions.Configuration;

namespace MVPStream.Models.Data
{
    public interface IAzureEndpoints
    {
        string GetDocumentDBUrl();
        string GetSearchAccount();
        string GetDocumentDBKey();
        string GetSearchKey();
    }

    public class AzureEndpoints:IAzureEndpoints
    {
        private string DocumentDBUrl;
        private string DocumentDBKey;
        private string SearchAccount;
        private string SearchKey;
        public AzureEndpoints(IConfigurationRoot configuration)
        {
            DocumentDBUrl = configuration["APPSETTING_ddburl"];
            DocumentDBKey = configuration["APPSETTING_ddbkey"];
            SearchAccount = configuration["APPSETTING_searchaccount"];
            SearchKey = configuration["APPSETTING_searchkey"];
        }

        public string GetDocumentDBUrl()
        {
            return DocumentDBUrl;
        }

        public string GetSearchAccount()
        {
            return SearchAccount;
        }

        public string GetDocumentDBKey()
        {
            return DocumentDBKey;
        }

        public string GetSearchKey()
        {
            return SearchKey;
        }
    }
}
