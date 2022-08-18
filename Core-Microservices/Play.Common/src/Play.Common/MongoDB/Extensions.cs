using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Play.Common.Settings;

namespace Play.Common.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {                    
                services.AddSingleton(serviceprovider =>
                {   
                    var configuration = serviceprovider.GetService<IConfiguration>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var mongodbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                    var mongoClient  = new MongoClient(mongodbSettings.ConnectionString);
                    return mongoClient.GetDatabase(serviceSettings.ServiceName);

                });
                

                return services;
        }

        public static IServiceCollection AddMassTransit(this IServiceCollection services)
        {
                        services.AddMassTransit(x =>
                       {
                            x.AddConsumers(Assembly.GetEntryAssembly());

                           x.UsingRabbitMq((context, configurator) =>
                           {
                               var configuration = context.GetService<IConfiguration>();
                               var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                               var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                               configurator.Host(rabbitMQSettings.Host);
                               configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                           });
                       });
                       return services;
        }

        public static IServiceCollection AddMongoRespository<T>(this IServiceCollection services) where T : IEntity {
            services.AddSingleton<IRepository<T>>(serviceprovider =>
            {
                var database = serviceprovider.GetService<IMongoDatabase>();
                return new ItemsRepository<T>(database);
            }
            );

            return services;
        }
    }
}