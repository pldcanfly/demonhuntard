using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BlackTemple.Messages
{
    class Sender
    {
        public void Send(string name, string payload)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 1337);

            Stream s = client.GetStream();
            StreamWriter sw = new StreamWriter(s);
            DemonHunterMessage message = new DemonHunterMessage();
            message.Id = 1234;
            message.Name = name;
            message.Payload = payload;
            sw.AutoFlush = true;
            Serializer.Serialize(sw.BaseStream, message);

            client.Close();
        }
    }
}
