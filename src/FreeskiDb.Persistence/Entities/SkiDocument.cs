using System;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace FreeskiDb.Persistence.Entities
{
    /// <summary>
    /// Represents a ski already stored in CosmosDb as a document. 
    /// </summary>
    public class SkiDocument : Ski
    {
        /// <summary>
        /// The id of the document. Genereated by CosmosDb.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The timestamp the document was last modified. Handled by CosmosDb.
        /// </summary>
        [JsonProperty(PropertyName = "_ts")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }
    }
}