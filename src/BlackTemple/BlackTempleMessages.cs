using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BlackTemple
{
    class BlackTempleMessages : Demonic
    {
        public BlackTempleMessages(string ipadress) : base(ipadress) { }

        public override void onMessageRecieved(int id, string message, JObject payload)
        {
            base.onMessageRecieved(id, message, payload);
        }
    }
}
