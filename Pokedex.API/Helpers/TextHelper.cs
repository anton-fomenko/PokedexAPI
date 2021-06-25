using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.API.Helpers
{
    public class TextHelper : ITextHelper
    {
        public string Fix(string original)
        {
            // Original text contains special characters, which were used as a hack to separate text.
            // We need to remove them so that the text could be translated and presented correctly.

            throw new NotImplementedException();
        }
    }
}
