using System.Threading.Tasks;

namespace Pokedex.API.Clients
{
    public interface ITranslatorClient
    {
        Task<string> TranslateAsync(string text, string lang);
    }
}
