using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using MVPStream.Models.Data;

namespace MVPStream.Services
{
    public interface IDocumentDB
    {
        Publisher GetPublisher(string id);
        List<Publisher> GetAllPublishers();
    }
    public class DocumentDB:IDocumentDB
    {

        private DocumentClient dbClient;

        public DocumentDB(IAzureEndpoints AzureEndpoints){
            dbClient = new DocumentClient(new Uri(AzureEndpoints.GetDocumentDBUrl()), AzureEndpoints.GetDocumentDBKey());
        }


        public Publisher GetPublisher(string id)
        {
            var collectionUri = UriFactory.CreateDocumentCollectionUri("mvps","publishers");
            return dbClient.CreateDocumentQuery<Publisher>(collectionUri, new FeedOptions {MaxItemCount=1 }).Where(x => x.type == "publisher" && x.id == id).ToArray().FirstOrDefault();
            
        }

        public List<Publisher> GetAllPublishers()
        {
            var collectionUri = UriFactory.CreateDocumentCollectionUri("mvps","publishers");
            return dbClient.CreateDocumentQuery<Publisher>(collectionUri, new FeedOptions { MaxItemCount = 1000 }).Where(x => x.type == "publisher").ToList();
            
        }
    }
}