using System.Text.Json.Serialization;

namespace twi.Propper;

public class Config {
    [JsonInclude] public int NewLimit = 10;
}
