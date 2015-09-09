using Newtonsoft.Json.Linq;
using ProtoBuf;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using RuneBookLogger;

namespace Nathrezim
{
    public delegate void recieveMessage(int id, string message, JObject payload); // CALLBACk

    class Nathrezim
    {
        private recieveMessage _callback;
        private IPEndPoint _masterEndpoint;
        private TcpClient _toMaster;
        private NetworkStream _toMasterStream;
        private IPEndPoint _masterAdress;
        private RuneBook _log = new RuneBook("Nathrezim");
        private JObject _config;
        private Master _ownMaster;
        private int _runningId = 0;
        private MessageQueue _outboundQueue = new MessageQueue();
        private MessageQueue _inboundQueue = new MessageQueue();
        private Thread _handlerThread;
        private Thread _writerThread;
        private Thread _listenThread;

        public Nathrezim()
        {
            string config = System.IO.File.ReadAllText(@"./config.json");
            _config = JObject.Parse(config);

            // Try to connect to the master to find if it is reachable, else be the master.
            if (_config["master"] != null && _config["master"]["ip"] != null && _config["master"]["port"] != null)
            {
                _masterAdress = new IPEndPoint(IPAddress.Parse(_config["master"]["ip"].ToString()), (int)_config["master"]["port"]);

                TcpClient tcp = new TcpClient();

                _log.log("Trying to connect to " + _masterAdress.ToString());
                try
                {
                    tcp.Connect(_masterAdress);
                    _log.log("Master found");
                    registerWithMaster(tcp);                  
                    
                }
                catch (SocketException e)
                {
                    _log.log("No Master found. Becoming Master");
                    _ownMaster = new Master(_masterAdress);
                }

                _handlerThread = new Thread(new ThreadStart(handlerThread));
                _handlerThread.Start();

                _writerThread = new Thread(new ThreadStart(writerThread));
                _writerThread.Start();

                _listenThread = new Thread(new ThreadStart(writerThread));
                _listenThread.Start();

            }
            else
            {
                _log.log("Config error!");
            }



        }

        private void registerWithMaster(TcpClient tcp)
        {
            _log.log("Registering with Master");
            Stream stream = tcp.GetStream();


            StreamWriter sw = new StreamWriter(stream);

            DemonicMessage registerMessage = new DemonicMessage();
            registerMessage.Id = 1;
            registerMessage.Name = "register";
            registerMessage.Payload = new JObject(
                new JProperty("methods", "gethtml, test")
                ).ToString();

            Serializer.Serialize<DemonicMessage>(sw.BaseStream, registerMessage);
            StreamReader sr = new StreamReader(stream);
            DemonicMessage message = Serializer.Deserialize<DemonicMessage>(sr.BaseStream);
            sr.Close();
            if (message.Name.Equals("register"))
            {
                _masterEndpoint = new IPEndPoint(_masterAdress.Address, (int)JObject.Parse(message.Payload)["port"]);
            }

            _log.log("Registered, got " + _masterEndpoint.ToString());
            _toMaster = new TcpClient();
            _toMaster.Connect(_masterEndpoint);
            _toMaster.Client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.KeepAlive, true);
            _toMasterStream = _toMaster.GetStream();
        }

        public void setCallback(recieveMessage callback)
        {
            _callback = callback;
        }

        private void handlerThread()
        {
            while (true)
            {
                if (_inboundQueue.hasElements())
                {
                    DemonicMessage message = _inboundQueue.Dequeue();
                    _callback(message.Id, message.Name, JObject.Parse(message.Payload));
                }
                Thread.Sleep(1);
            }
        }

        private void listenThread()
        {
            while (true)
            {
                checkConnection();
                Stream stream = _toMasterStream;
                StreamReader sr = new StreamReader(stream);
                DemonicMessage message = Serializer.Deserialize<DemonicMessage>(sr.BaseStream);
                stream.Close();

                _inboundQueue.Enqueue(message);
                Thread.Sleep(1);
            }
        }

        private void writerThread()
        {
            while (true)
            {
                if (_outboundQueue.hasElements())
                {
                    checkConnection();
                    Stream stream = _toMasterStream;
                    StreamWriter sw = new StreamWriter(stream);
                    Serializer.Serialize<DemonicMessage>(sw.BaseStream, _outboundQueue.Dequeue());
                    stream.Close();
                }
                Thread.Sleep(1);
            }
        }

        private void checkConnection()
        {
            if(_ownMaster == null)
            {
                if (!_toMaster.Connected)
                {
                    _log.log("Connection lost, reconnecting");
                    _toMaster.Connect(_masterAdress);
                    _toMasterStream = _toMaster.GetStream();
                }
            }
        }


        private int SendMessage(string method, JObject payload, bool urgent = false)
        {
            int id = _runningId++;
            DemonicMessage message = new DemonicMessage();
            message.Id = id;
            message.Name = method;
            message.Payload = payload.ToString();
            message.Urgent = urgent;

            _outboundQueue.Enqueue(message);

            return id;
        }
    }
}
