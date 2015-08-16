using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Net;

namespace Illidan
{
    class IllidanMessages : Demonic
    {
        public IllidanMessages(string ipadress) : base(ipadress) { }

        public new void init()
        {
            Thread thread = new Thread(new ThreadStart(Listen));
            thread.Start();
        }

        public void setEndpoint(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        }
        
        /* public override void onMessageRecieved(int id, string message, JObject payload)
        {
            switch (message)
            {
                case "REGISTER":
                    System.Console.WriteLine("Registration");
                    break;
                default:
                    break;
            }

            base.onMessageRecieved(id, message, payload);
        } */

    }
}
