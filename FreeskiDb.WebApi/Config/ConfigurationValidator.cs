using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace FreeskiDb.WebApi.Config
{
    public class ConfigurationValidator
    {
        private static readonly List<string> ConfigKeys = new List<string>
        {
            "CosmosUri",
            "CosmosKey"
        };

        public static void Vaildate(IConfiguration configuration)
        {
            foreach (var key in ConfigKeys)
            {
                if (string.IsNullOrEmpty(configuration.GetValue<string>(key)))
                {
                    throw new ArgumentException($"{key} is not configured");
                }
            }
        }
    }
}