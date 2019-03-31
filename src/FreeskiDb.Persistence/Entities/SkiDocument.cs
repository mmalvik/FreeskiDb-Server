using System;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace FreeskiDb.Persistence.Entities
{
    /// <summary>
    /// Represent a ski with additional CosmosDb document information.
    /// </summary>
    public class SkiDocument : Ski
    {
        /// <summary>
        /// The id of the document.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The timestamp the document was last modified.
        /// </summary>
        [JsonProperty(PropertyName = "_ts")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }
    }
}