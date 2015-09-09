using Newtonsoft.Json.Linq;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nathrezim
{
    class Demon
    {
        private string _name;
        private int _loadout = 0;
        private List<string> _methods;
        private DateTime _started;
        private IPEndPoint _location;

        private TcpListener _listener;
        private recieveMessage _callback;
        //private Thread _thread;

        Thread _readerThread;
        Thread _writerThread;

        public Demon(IPEndPoint endpoint, string name, string methods)
        {
            _location = endpoint;
            _name = name;
            if (methods.Length < 0)
            {
                string[] mets = methods.Split(',');
                foreach (string method in mets)
                {
                    _methods.Add(methods);
                }
            }
            _started = DateTime.Now;

        }

        public void sendMessage(JObject message)
        {

        }

        public bool hasMethod(string name)
        {
            return _methods.Contains(name);
        }

        /*private void initListener(TcpListener listener, recieveMessage callback)
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
            if (_thread != null)
            {
                // thread killen
            }

            _callback = callback;
            _listener = listener;
            _listener.Start();
            _thread = new Thread(new ThreadStart(acceptMessages));
            _thread.Start();
        }

        private void acceptMessages()
        {
            Socket socket = _listener.AcceptSocket();
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.KeepAlive, true);
            Stream ns = new NetworkStream(socket);
            StreamReader sr = new StreamReader(ns);

            while (true)
            {
                DemonicMessage message = Serializer.Deserialize<DemonicMessage>(sr.BaseStream);

            } 
            
        }*/
    }
}
