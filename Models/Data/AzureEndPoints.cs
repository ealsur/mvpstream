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
            DocumentDBUrl = configuration["ddburl"];
            DocumentDBKey = configuration["ddbkey"];
            SearchAccount = configuration["searchaccount"];
            SearchKey = configuration["searchkey"];
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