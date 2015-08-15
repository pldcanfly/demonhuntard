using PerCederberg.Grammatica.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllidariRunes
{
    class IllidariRunes : GrammarAnalyzer
    {
        public override Node ExitDocument(Production node)
        {
            System.Console.ReadKey();
            return base.ExitDocument(node);
        }

        public override Node ExitText(Production node)
        {
            Node child = null;
            for (int i = 0; i < node.GetChildCount(); i++)
            {
                child = node.GetChildAt(i);
                System.Console.WriteLine(child.GetValue(0));
            }

            return base.ExitText(node);
        }

        public override Node ExitNumber(Token node)
        {
            node.AddValue(Int32.Parse(node.Image));
            return base.ExitNumber(node);
        }

        public override Node ExitCharacter(Token node)
        {
            node.AddValue(node.Image.ToString());
            return base.ExitCharacter(node);
        }

        public override Node ExitEverything(Token node)
        {
            node.AddValue(node.Image.ToString());
            return base.ExitEverything(node);
        }

        public override Node ExitWhitespace(Token node)
        {
            node.AddValue(node.Image.ToString());
            return base.ExitWhitespace(node);
        }


        /* public override Node ExitSetp(Production node)
       {
           Node var = null;
           Dictionary<string, object> variables = new Dictionary<string, object>();
           string varname = "";
           object value = null;
           for (int i = 0; i < node.GetChildCount(); i++)
           {
               var = node.GetChildAt(i);
               switch(var.GetName()){
                   case "IDENTIFIER":
                       varname = (string)var.GetValue(0);
                       break;
                   case "NUMBER":
                       value = (int)var.GetValue(0);
                       break;
               }
           }

           System.Console.WriteLine("%Set " + varname + " to " + value.ToString() + "%");

           System.Console.ReadKey();
           return base.ExitSetp(node);
       }
       */
    }
}
