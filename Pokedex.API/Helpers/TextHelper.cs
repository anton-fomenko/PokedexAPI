using System.Text.RegularExpressions;

namespace Pokedex.API.Helpers
{
    public class TextHelper : ITextHelper
    {
        public string Fix(string original)
        {
            // Original text contains special characters, which were used as a hack to separate text.
            // We need to remove them so that the text could be translated and presented correctly.

            // This is a list of the necessary changes:

            // Page breaks are treated just like newlines.
            // Soft hyphens followed by newlines vanish.
            // Hard hyphens followed by newlines become just hard hyphens, to preserve real
            // hyphenation.
            // Any other newline becomes a space.

            return Regex.Replace(original, "\f", "\n")
                        .Replace("\u00ad\n", "")
                        .Replace(" -\n", " - ")
                        .Replace("-\n", "-")
                        .Replace("\n", " ");
        }
    }
}
