using System.Text.Json.Serialization;

namespace DatabaseConnector;

public class Database
{
    [JsonPropertyName("ConnectionString")]
    public string ConnectionString { get; set; }
    public Database()
    {

    }
}
