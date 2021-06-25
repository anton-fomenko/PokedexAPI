using Moq;
using NUnit.Framework;
using PokeApiNet;
using Pokedex.API.Clients;
using Pokedex.API.Constants;
using Pokedex.API.Helpers;
using Pokedex.API.Resources;
using Pokedex.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.API.UnitTests
{
    [TestFixture]
    public class PokemonServiceTests
    {
        private Mock<IPokeClient> _pokeClientMock;
        private Mock<ITranslatorClient> _translatorClientMock;
        private Mock<ITextHelper> _textHelper;

        [SetUp]
        public void Setup()
        {
            _pokeClientMock = new Mock<IPokeClient>();
            _translatorClientMock = new Mock<ITranslatorClient>();
            _textHelper = new Mock<ITextHelper>();

            Pokemon pokemon = new Pokemon()
            {
                Species = new NamedApiResource<PokemonSpecies>()
            };

            _pokeClientMock.Setup(x => x.GetResourceAsync<Pokemon>(It.IsAny<string>()))
                .Returns(Task.FromResult(pokemon));
        }

        [Test]
        public async Task Get_LegendaryButNotLivesInCave_TranslationShouldNotBeInvoked()
        {
            // Arrange
            SetupSpeciesMock(isLegendary: true, habitat: Habitats.Fire);
            IPokemonService pokemonService = new PokemonService(_pokeClientMock.Object, _translatorClientMock.Object, _textHelper.Object);

            // Act
            PokemonResource pokemonResource = await pokemonService.Get("ho-oh");

            // Assert
            _translatorClientMock.Verify(x => x.TranslateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Get_NotLegendaryButLivesInCave_TranslationShouldNotBeInvoked()
        {
            // Arrange
            SetupSpeciesMock(isLegendary: false, habitat: Habitats.Cave);
            IPokemonService pokemonService = new PokemonService(_pokeClientMock.Object, _translatorClientMock.Object, _textHelper.Object);

            // Act
            PokemonResource pokemonResource = await pokemonService.Get("ho-oh");

            // Assert
            _translatorClientMock.Verify(x => x.TranslateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetTranslated_LegendaryPokemonNotInCave_YodaTranslationUsed()
        {
            // Arrange
            SetupSpeciesMock(isLegendary: true, habitat: Habitats.Fire);
            IPokemonService pokemonService = new PokemonService(_pokeClientMock.Object, _translatorClientMock.Object, _textHelper.Object);

            // Act
            await pokemonService.GetTranslatedAsync("ho-oh");

            // Assert
            _translatorClientMock.Verify(x => x.TranslateAsync(It.IsAny<string>(), Languages.Fun.Yoda));
        }

        [Test]
        public async Task GetTranslated_NotLegendaryButLivesInCave_YodaTranslationUsed()
        {
            // Arrange
            SetupSpeciesMock(isLegendary: false, habitat: Habitats.Cave);
            IPokemonService pokemonService = new PokemonService(_pokeClientMock.Object, _translatorClientMock.Object, _textHelper.Object);

            // Act
            await pokemonService.GetTranslatedAsync("ho-oh");

            // Assert
            _translatorClientMock.Verify(x => x.TranslateAsync(It.IsAny<string>(), Languages.Fun.Yoda));
        }

        [Test]
        public async Task GetTranslated_NotLegendaryAndNotLivesInCave_ShakespeareTranslationUsed()
        {
            // Arrange
            SetupSpeciesMock(isLegendary: false, habitat: Habitats.Fire);
            IPokemonService pokemonService = new PokemonService(_pokeClientMock.Object, _translatorClientMock.Object, _textHelper.Object);

            // Act
            await pokemonService.GetTranslatedAsync("ho-oh");

            // Assert
            _translatorClientMock.Verify(x => x.TranslateAsync(It.IsAny<string>(), Languages.Fun.Shakespeare));
        }

        [Test]
        public async Task GetTranslated_TranslationError_StandardDescriptionUsed()
        {
            // Arrange
            string description = "This Pokemon is a fire-type Pokemon and closely resembles an elk. It has lava stone legs, a smoldering snout and pointy ears.";
            SetupSpeciesMock(isLegendary: false, habitat: Habitats.Fire);
            _translatorClientMock.Setup(x => x.TranslateAsync(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            _textHelper.Setup(x => x.Fix(It.IsAny<string>())).Returns(description);
            IPokemonService pokemonService = new PokemonService(_pokeClientMock.Object, _translatorClientMock.Object, _textHelper.Object);

            // Act
            PokemonResource pokemonResource = await pokemonService.GetTranslatedAsync("ho-oh");

            // Assert
            Assert.AreEqual(description, pokemonResource.Description);
        }

        private void SetupSpeciesMock(bool isLegendary, string habitat)
        {
            PokemonSpecies pokemonSpecies = GetSpecies(isLegendary, habitat);

            _pokeClientMock.Setup(x => x.GetResourceAsync<PokemonSpecies>(It.IsAny<string>()))
                .Returns(Task.FromResult(pokemonSpecies));
        }

        private PokemonSpecies GetSpecies(bool isLegendary, string habitat)
        {
            string description = "This Pokemon is a fire-type Pokemon and closely resembles an elk. It has lava stone legs, a smoldering snout and pointy ears.";

            PokemonSpecies pokemonSpecies = new PokemonSpecies()
            {
                IsLegendary = isLegendary,
                Habitat = new NamedApiResource<PokemonHabitat>()
                {
                    Name = habitat
                },
                FlavorTextEntries = new List<PokemonSpeciesFlavorTexts>()
                        {
                            new PokemonSpeciesFlavorTexts
                            {
                                Language = new NamedApiResource<Language>()
                                {
                                    Name = Languages.English
                                },
                                FlavorText = description
                            }
                        }
            };

            return pokemonSpecies;
        }
    }
}