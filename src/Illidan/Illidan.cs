using Illidan.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Illidan
{
    public delegate void MessageCallback(int id, string message, string payload = "");

    class Illidan
    {
        private List<Listener> _listeners = new List<Listener>();
        private List<Minon> _minions = new List<Minon>();

        public Illidan(int port)
        {
            MessageCallback callback = new MessageCallback(this.onIncomingMessage);
            this._listeners.Add(new Listener(callback, port));
            foreach(Listener lst in this._listeners)
            {
                lst.openConnection();
            }
        }

        public void onIncomingMessage(int id, string message, string payload = "")
        {
            System.Console.WriteLine("CALLBACK TRIGGERED");
            System.Console.WriteLine(id);
            System.Console.WriteLine(message);
            System.Console.WriteLine(payload);
            
        }
    }
}
