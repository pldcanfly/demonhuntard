using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using ProtoBuf;
using System.Net;

namespace Illidan.Messages
{
    class Listener
    {
        private int _port;
        private TcpListener _listener;
        private Thread _thread;
        private MessageCallback _callback;

        public Listener(MessageCallback callback, int port = 1337)
        {
            this._callback = callback;
            this._port = port;
            this._thread = new Thread(new ThreadStart(Run));
            //Program.controlpanel.writeToConsole("Initialize Listener...");
        }

        public void openConnection()
        {
            this._thread.Start();
        }


        public void closeConnection()
        {
            this._thread.Abort();
        }


        private void Run()
        {
            this._listener = new TcpListener(IPAddress.Parse("127.0.0.1"),  this._port);
            this._listener.Start();
            while (true)
            {
                System.Console.WriteLine("Listening");
                Socket soc = this._listener.AcceptSocket();
                //Program.controlpanel.writeToConsole("Listening on " + this._port.ToString() + "...");

                try
                {
                    Stream s = new NetworkStream(soc);
                    StreamReader sr = new StreamReader(s);
                    //StreamWriter sw = new StreamWriter(s);
                    //sw.AutoFlush = true;

                    DemonHunterMessage message = Serializer.Deserialize<DemonHunterMessage>(sr.BaseStream);

                    this._callback(message.Id, message.Name, message.Payload);

                    /*string input = sr.ReadLine();
                    sw.WriteLine(input); */
                    s.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("EXCEPTION: " + e.Message);
                }

                soc.Close();
            }
        }
    }
}
