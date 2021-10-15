using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Domain.Models;
using PersonDictionaryModel.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonDictionaryModel.Integration.Test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    // Add a database context using an in-memory 
                    // database for testing.
                    services.AddDbContext<PersonDictionaryModelContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    services.AddScoped(provider => provider.GetService<PersonDictionaryModelContext>());

                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<PersonDictionaryModelContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    context.Database.EnsureCreated();

                    try
                    {
                        SeedSampleData(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with sample data. Error: {ex.Message}.");
                    }
                })
                .UseEnvironment("Test");
        }

        public static void SeedSampleData(PersonDictionaryModelContext context)
        {
            

            context.People.AddRange(
               new Person()
               {
                   Id = 1,
                   FirstName = "Jhon",
                   LastName = "Doe",
                   BirthDate = DateTime.Now,
                   Gender = Gender.Male,
                   PersonalNumber = "0518238777",
                   CityId = 1,
                   Photo = "somephotopath",
               },
                new Person()
                {
                    Id = 2,
                    FirstName = "Ella",
                    LastName = "Doe",
                    BirthDate = DateTime.Now,
                    Gender = Gender.Female,
                    PersonalNumber = "0518258777",
                    CityId = 2,
                    Photo = "somephotopath",
                },
                new Person()
                {
                    Id = 3,
                    FirstName = "Marta",
                    LastName = "Bella",
                    BirthDate = DateTime.Now,
                    Gender = Gender.Female,
                    PersonalNumber = "0518256777",
                    CityId = 3,
                    Photo = "somephotopath",
                }
            );

            context.SaveChanges();
        }
    }
}
