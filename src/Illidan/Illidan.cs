using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Illidan
{
    class Illidan
    {
       // private List<Listener> _listeners = new List<Listener>();
        private List<Minon> _minions = new List<Minon>();
        IllidanMessages _messages = new IllidanMessages("127.0.0.1");

        public Illidan(int port)
        {
            _messages.setName("Illidan");
            _messages.setCallback(this.onMessageRecieved);
            _messages.init();
        }

        private void onMessageRecieved(int id, string message, JObject payload)
        {
            List<string> events = new List<string>();
            switch (message)
            {
                case "REGISTER":
                    System.Console.WriteLine("Registration!");
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), (int)payload["port"]);
                    string name = payload["name"].ToString();
                    foreach(JValue val in payload["methods"])
                    {
                       events.Add(val.ToString());
                    }
                    _minions.Add(new Minon(endpoint, name, events));
                    break;
                default:
                    System.Console.WriteLine("Routing");
                    foreach(Minon min in _minions)
                    {
                        if (min.hasEvent(message))
                        {
                            _messages.setEndpoint(min.Endpoint);
                            _messages.Send(id, message, payload);
                        }
                    }
                    break;
            }
        }

        /*public void onIncomingMessage(IPEndPoint endpoint, int id, string message, string payload = "")
        {
            System.Console.WriteLine("Message recieved!");
            switch (message)
            {
                case "REGISTER":
                    System.Console.WriteLine("Registration!");
                    var load = payload.Split('#');
                    // var events = load[1].Split(',');
                    _minions.Add(new Minon(endpoint, load[0], load[1]));
                    break;
                default:
                    foreach(Minon minion in _minions)
                    {
                        if (minion.hasEvent(message))
                        {
                            System.Console.WriteLine("Send " + payload + " to " + minion.Name);
                            minion.sendEvent(id, message, payload);
                            break;
                        }
                    }
                    break;
            }
            
            
        }*/
    }
}
