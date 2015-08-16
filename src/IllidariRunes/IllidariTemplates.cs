using Messages;
using PerCederberg.Grammatica.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IllidariRunes
{
    class IllidariTemplates
    {
        Demonic messages = new Demonic("127.0.0.1");
        string _html = "<html><head><title>Hello</title></head><body>World!</body></html>";

        public IllidariTemplates()
        {
            messages.setName("Illidari Runes");
            messages.addMethod("GETHTML");
            messages.setPort(11200);
            messages.setCallback(onMessageRecieved); 
            messages.init();
        }

        private void onMessageRecieved(int id, string message, JObject payload)
        {
            switch (message)
            {
                case "GETHTML":
                    JObject response = new JObject(
                        new JProperty("content", _html));

                    messages.Send(id, "RESPONSE", response);
                    break;
            }
        }
    }
}
