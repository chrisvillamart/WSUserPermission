using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSUserPermission.Entities;
using WSUserPermission.Utils;

namespace WSUserPermission.ElasticSearch
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticSearchUrl = ConfigurationManager.AppSetting["elasticSearchUrl"];
            var elasticSearchUser = ConfigurationManager.AppSetting["elasticSearchUser"];
            var elasticSearchPassword = ConfigurationManager.AppSetting["elasticSearchPassword"]; 
            var elasticSearchDefaultIndex = ConfigurationManager.AppSetting["elasticSearchDefaultIndex"];


            var settings = new ConnectionSettings(new Uri(elasticSearchUrl))
                .DefaultIndex(elasticSearchDefaultIndex);
            settings.BasicAuthentication(elasticSearchUser, elasticSearchPassword);
            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, elasticSearchDefaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.
                DefaultMappingFor<Permission>(m => m
            );
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Permission>(x => x.AutoMap())
            );
        }
    }
}
