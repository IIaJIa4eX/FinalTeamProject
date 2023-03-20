using System.Text.Json.Serialization;

namespace DatabaseConnector;


public class Database
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("UserName")]
    public string UserName { get; set; }
    
    [JsonPropertyName("ConnectionString")]
    public string ConnectionString { get; set; }
    public Database()
    {

    }
}
