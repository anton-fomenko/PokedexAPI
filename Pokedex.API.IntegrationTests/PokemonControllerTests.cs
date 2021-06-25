using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Pokedex.API.IntegrationTests
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private WebApplicationFactory<Startup> _factory;
        
        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [Test]
        [TestCase("/pokemon/mewtwo")]
        [TestCase("/pokemon/ho-oh")]
        [TestCase("/pokemon/translated/mewtwo")]
        public async Task Get_ExistingName_ReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.AreEqual("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Test]
        [TestCase("/pokemon/1asdf")]
        [TestCase("/pokemon/translated/1asdf")]
        public async Task Get_NameNotFound_ReturnsNotFoundCode(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        [TestCase("/pokemon/mewtwo", "Responses/mewtwo.json")]
        [TestCase("/pokemon/translated/mewtwo", "Responses/mewtwo_translated.json")]
        public async Task Get_MewTwo_ExpectedJsonReceived(string url, string fileName)
        {
            // Arrange
            var client = _factory.CreateClient();

            string expectedBody = File.ReadAllText(fileName);
            JObject expectedJson = JObject.Parse(expectedBody);

            // Act
            var response = await client.GetAsync(url);

            string actualBody = await response.Content.ReadAsStringAsync();
            JObject actualJson = JObject.Parse(actualBody);

            // Assert
            Assert.True(JToken.DeepEquals(expectedJson, actualJson),
                $"Expected:\n{expectedJson}\nActual:\n{actualJson}");
        }
    }
}