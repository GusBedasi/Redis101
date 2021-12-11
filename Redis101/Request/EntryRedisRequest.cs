using Newtonsoft.Json;

namespace Redis101.Request
{
    public class EntryRedisRequest 
    {
        [JsonProperty("key")]
        public string Key {get;set;}
        [JsonProperty("value")]
        public string Value {get;set;}
    }
}