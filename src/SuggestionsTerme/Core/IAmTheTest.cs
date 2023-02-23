using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuggestionsTerme.Core
{
    public interface IAmTheTest
    {
        IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions);
    }
}
