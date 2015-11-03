using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client.TransientFaultHandling;
using Microsoft.Azure.Documents.Client.TransientFaultHandling.Strategies;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace MVPStream.Models
{
    public static class DocumentDB
    {

        private static IReliableReadWriteDocumentClient dbClient;
        private static string dbLink;
        private static string colLink;

        private static IReliableReadWriteDocumentClient GetClient()
        {
            if (dbClient == null)
            {
                var documentClient = new DocumentClient(new Uri(AzureEndpoints.DocumentDBUrl), AzureEndpoints.DocumentDBKey);
                var documentRetryStrategy = new DocumentDbRetryStrategy(RetryStrategy.DefaultExponential) {FastFirstRetry = true};
			    dbClient = documentClient.AsReliable(documentRetryStrategy);
                
            }
            return dbClient;
        }

        public static async Task Insert<T>(string collectionName, T item)
        {
            var client = GetClient();
            
            var database = await GetOrCreateDatabaseAsync("mvps");
            var collection = await GetOrCreateCollectionAsync(database, collectionName);
            await client.CreateDocumentAsync(collection, item);                
            
        }

        public static async Task Delete()
        {
            var client = GetClient();
            
            var database = await GetOrCreateDatabaseAsync("mvps");
            var collection = await GetOrCreateCollectionAsync(database, "entries");
            await client.DeleteDocumentCollectionAsync(collection);
            
        }
        public static async Task Insert<T>(string collectionName, IEnumerable<T> items)
        {
            var client = GetClient();
            
            var database = await GetOrCreateDatabaseAsync("mvps");
            var collection = await GetOrCreateCollectionAsync(database, collectionName);
            foreach (var item in items)
            {
                await client.CreateDocumentAsync(collection, item);
            }
            
        }

        public static async Task<Publisher> GetPublisher(string id)
        {
            var client = GetClient();
            
            var database = await GetOrCreateDatabaseAsync("mvps");
            var collection = await GetOrCreateCollectionAsync(database, "publishers");
            return client.CreateDocumentQuery<Publisher>(collection, new FeedOptions {MaxItemCount=1 }).Where(x => x.type == "publisher" && x.id == id).ToArray().FirstOrDefault();
            
        }

        public static async Task<List<Publisher>> GetAllPublishers()
        {
            var client = GetClient();
            
            var database = await GetOrCreateDatabaseAsync("mvps");
            var collection = await GetOrCreateCollectionAsync(database, "publishers");
            return client.CreateDocumentQuery<Publisher>(collection, new FeedOptions { MaxItemCount = 1000 }).Where(x => x.type == "publisher").ToList();
            
        }

        #region Tooling
        private static async Task<string> GetOrCreateCollectionAsync(string dbLink, string id)
        {
            if (string.IsNullOrEmpty(colLink))
            {
                DocumentCollection collection = dbClient.CreateDocumentCollectionQuery(dbLink).Where(c => c.Id == id).ToArray().FirstOrDefault();
                if (collection == null)
                {
                    collection = await dbClient.CreateDocumentCollectionAsync(dbLink, new DocumentCollection { Id = id });
                }

                colLink = collection.DocumentsLink;
            } 
            return colLink;
        }

        private static async Task<string> GetOrCreateDatabaseAsync(string id)
        {
            if (string.IsNullOrEmpty(dbLink))
            {
                Database database = dbClient.CreateDatabaseQuery().Where(db => db.Id == id).ToArray().FirstOrDefault();
                if (database == null)
                {
                    database = await dbClient.CreateDatabaseAsync(new Database { Id = id });
                }

                dbLink= database.CollectionsLink;
            }
            return dbLink;
        } 
        #endregion
    }
}