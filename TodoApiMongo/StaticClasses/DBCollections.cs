using MongoDB.Driver;
using TodoApiMongo.Models;

namespace TodoApiMongo.StaticClasses
{
    public class DBCollections
    {
        private static MongoClient client = new MongoClient("mongodb://localhost:27017");
        private static IMongoDatabase database = client.GetDatabase("TestMongo");

        public static IMongoCollection<TodoItem> tareaCollection = database.GetCollection<TodoItem>("Test");
    }
}
