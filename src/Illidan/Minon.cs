using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Illidan
{
    class Minon
    {
        private string _name;
        private List<string> _events;
        private DateTime _registered;
        private IPEndPoint _endpoint;
        

        public string Name
        {
            get { return _name; }
        }

        public IPEndPoint Endpoint
        {
            get { return _endpoint; }
        }

        public Minon(IPEndPoint endpoint, string name) : this(endpoint, name, new List<string>())
        {
            this._endpoint = endpoint;
            this._name = name;
            this._registered = DateTime.Now;
        }

        public Minon(IPEndPoint endpoint, string name, List<string> events)
        {
            this._endpoint = endpoint;
            this._name = name;
            this._registered = DateTime.Now;
            this._events = events;
        }

        public bool hasEvent(string name)
        {
            return _events.Contains<string>(name);
        }

    }
}
