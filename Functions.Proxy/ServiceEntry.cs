namespace Devlight.Azure.Functions.Proxy
{
    using Newtonsoft.Json;
    using System;

    public class ServiceEntry
    {
        [JsonProperty("name")]
        public string PartitionKey { get; set; }

        [JsonProperty("version")]
        public string RowKey { get; set; }

        public string Host { get; set; }

        public DateTime? RegisteredAt { get; set; }

        [JsonIgnore]
        public string ETag { get; set; } = "*";
    }
}
