using PerCederberg.Grammatica.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllidariRunes
{
    class IllidariTemplates
    {

        public IllidariTemplates(string input)
        {
            Parser parser = null;
            parser = new GrammarParser(new StringReader(input), new IllidariRunes());
            parser.Parse();
        }

    }
}
