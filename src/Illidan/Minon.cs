using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Illidan
{
    class Minon
    {
        private string _name;
        private string _events;
        private DateTime _registered;
        private IPEndPoint _endpoint;
        

        public string Name
        {
            get { return _name; }
        }

        public Minon(IPEndPoint endpoint, string name) : this(endpoint, name,"")
        {
            this._endpoint = endpoint;
            this._name = name;
            this._registered = DateTime.Now;
        }

        public Minon(IPEndPoint endpoint, string name, string events)
        {
            this._endpoint = endpoint;
            this._name = name;
            this._registered = DateTime.Now;
            this._events = events;
        }
    }
}
