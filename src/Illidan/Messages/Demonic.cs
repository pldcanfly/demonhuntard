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

namespace Messages
{
    public delegate void messageCallback(int id, string message, JObject payload);

    class Demonic
    {

        protected IPEndPoint _endpoint;
        protected bool _connected;
        protected List<DemonicMessage> _que = new List<DemonicMessage>();
        protected TcpListener _listener;
        protected JArray _methods = new JArray();
        protected string _name = "";
        protected int _port = 1337;
        protected messageCallback _callback;


        public Demonic(string ipadress)
        {
            _endpoint = new IPEndPoint(IPAddress.Parse(ipadress), 1337);
        }

        public void setName(string name)
        {
            _name = name;
        }

        public void addMethod(string name)
        {
            _methods.Add(new JValue(name));
        }
        public void setPort(int port)
        {
            _port = port;
        }

        public void setCallback(messageCallback mcb)
        {
            _callback = mcb;
        }

        public void init()
        {
            System.Console.WriteLine("Connecting to Illidan");
            TcpClient client = new TcpClient();
            client.Connect(_endpoint);

            JObject payload = new JObject(
                new JProperty("name", _name),
                new JProperty("methods", _methods),
                new JProperty("port", _port));


            Stream s = client.GetStream();
            StreamWriter sw = new StreamWriter(s, Encoding.UTF8);

            StreamReader sr = new StreamReader(s);
            DemonicMessage message = new DemonicMessage();
            message.Id = 1;
            message.Name = "REGISTER";
            message.Payload = payload.ToString();
            //sw.AutoFlush = true;
            System.Console.WriteLine("Registering to Illidan");
            Serializer.Serialize(sw.BaseStream, message);
            client.Close();
            //DemonicMessage res = Serializer.Deserialize<DemonicMessage>(sr.BaseStream);
            //JObject response = JObject.Parse(res.Payload);
            // client.Close();

            //  System.Console.WriteLine("Got Illidan-Port " + response["port"].ToString());
            //  _endpoint = new IPEndPoint(IPAddress.Parse(response["ip"].ToString()), (int)response["port"]);

            //  Thread thread = new Thread(new ThreadStart(Listen));
            //  thread.Start();
        }

        public void Send(int id, string message, JObject payload)
        {
            DemonicMessage msg = new DemonicMessage();
            msg.Id = id;
            msg.Name = message;
            msg.Payload = payload.ToString();

            TcpClient tcp = new TcpClient();
            tcp.Connect(_endpoint);
            Stream s = tcp.GetStream();
            StreamWriter sw = new StreamWriter(s);

            Serializer.Serialize(s, msg);
            tcp.Close();
            System.Console.WriteLine("Sent Message");
        }

        public void Listen()
        {
            this._listener = new TcpListener(_endpoint);
            this._listener.Start();
            while (true)
            {
                System.Console.WriteLine("Listening for Messages");
                Socket soc = this._listener.AcceptSocket();
                try
                {
                    Stream s = new NetworkStream(soc);
                    StreamReader sr = new StreamReader(s);
                    DemonicMessage message = Serializer.Deserialize<DemonicMessage>(sr.BaseStream);
                    s.Close();
                    onMessageRecieved(message.Id, message.Name, JObject.Parse(message.Payload));
                }
                catch (Exception e)
                {
                    Console.WriteLine("EXCEPTION: " + e.Message);
                }
            }
        }

        public virtual void onMessageRecieved(int id, string message, JObject payload)
        {
            if(_callback != null)
            {
                _callback(id, message, payload);
            }
        }
    }

    [ProtoContract]
    class DemonicMessage
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Payload { get; set; }
    }
}
