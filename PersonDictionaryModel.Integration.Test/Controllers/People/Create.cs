using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Model.Models;
using PersonDictionaryModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PersonDictionaryModel.Integration.Test.Controllers.People
{
    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Create(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidCreateTodoItemCommand_ReturnsSuccessCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreatePersonDto
            {
                FirstName = "Jhon",
                LastName = "Doe",
                BirthDate = DateTime.Now,
                Gender = Gender.Male,
                PersonalNumber = "0518238777",
                CityId = 1
            };

            var content = IntegrationTestHelper.GetRequestContent(command);

            var response = await client.PostAsync($"/api/peop", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidCreateTodoItemCommand_ReturnsBadRequest()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreateTodoItemCommand
            {
                Title = "This description of this thing will exceed the maximum length. This description of this thing will exceed the maximum length. This description of this thing will exceed the maximum length. This description of this thing will exceed the maximum length."
            };

            var content = IntegrationTestHelper.GetRequestContent(command);

            var response = await client.PostAsync($"/api/todoitems", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
