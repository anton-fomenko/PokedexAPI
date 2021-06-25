using NUnit.Framework;
using System.Threading.Tasks;

namespace Pokedex.API.UnitTests
{
    [TestFixture]
    public class PokemonServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Get_LegendaryButNotLivesInCave_TranslationShouldNotBeInvoked()
        {

        }

        [Test]
        public async Task Get_NotLegendaryButLivesInCave_TranslationShouldNotBeInvoked()
        {
            
        }

        [Test]
        public async Task GetTranslated_LegendaryPokemonLivesInCave_YodaTranslationUsed()
        {
            
        }

        [Test]
        public async Task GetTranslated_NotLegendaryButLivesInCave_YodaTranslationUsed()
        {
            
        }

        [Test]
        public async Task GetTranslated_NotLegendaryAndNotLivesInCave_ShakespeareTranslationUsed()
        {
            
        }

        [Test]
        public async Task GetTranslated_TranslationError_StandardDescriptionUsed()
        {
            
        }
    }
}