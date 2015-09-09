using System;
using System.Collections.Generic;
using System.Net;

namespace Nathrezim
{
    class Daemon
    {
        private string _name;
        private int _loadout = 0;
        private List<string> _methods;
        private DateTime _started;
        private IPEndPoint _location;
    }
}
