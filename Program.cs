using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB_Console_Project2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int AntalFelterIDokument = 0;
            List<string> DocumentString = new List<string>();
            string connectionString = "mongodb://localhost:27017";
            //string connectionString = "mongodb://223.187.56.30:27017";

            try
            {
                // Create a MongoDB client
                var client = new MongoClient(connectionString);

                // Get a reference to the database (create it if it doesn't exist)
                var database = client.GetDatabase("TestDatabase_h3pd101124_Dynamic");

                // Get a reference to a collection (create it if it doesn't exist)
                var collection = database.GetCollection<BsonDocument>("TestCollection");

                Console.Clear();
                Console.Write("Indtast antal felter i document : ");
                AntalFelterIDokument = Convert.ToInt32(Console.ReadLine());

                var document = new BsonDocument();
                for (int i = 0; i < AntalFelterIDokument; i++)
                {
                    document.Add($"field{i}", new BsonString($"Value for field {i}"));
                }

                await collection.InsertOneAsync(document);

                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = await collection.Find(filter).ToListAsync();
                if (result != null)
                {
                    Console.WriteLine("Retrived documents:");
                    foreach (var thisDocument in result)
                    {
                        Console.WriteLine(thisDocument.ToJson());
                    }
                }
                else
                {
                    Console.WriteLine("No document found!");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
