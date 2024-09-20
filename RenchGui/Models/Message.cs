using Newtonsoft.Json;

namespace RenchGui.Models;

public class Message {
    [JsonProperty("action")]
    public string Action { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
}
