using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Messages;
using Newtonsoft.Json.Linq;

namespace BlackTemple
{
    class Sewers
    {
        HttpListener _listener;
        Demonic _messages;
        int _i = 0;
        Dictionary<int, string> _html = new Dictionary<int, string>();

        public Sewers()
        {
            _listener = new HttpListener();
            string prefix = "http://*:81/";
            _listener.Prefixes.Add(prefix);
            _messages = new Demonic("127.0.0.1");
            _messages.setName("Black Temple");
            _messages.setPort(11201);
            _messages.addMethod("RESPONSE");
            _messages.setCallback(onMessageRecieved);
            _messages.init();
            

            try
            {
                _listener.Start();
                while (true)
                {
                    System.Console.WriteLine("Listening");
                    IAsyncResult result = _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
                    result.AsyncWaitHandle.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }

        private void onMessageRecieved(int id, string message, JObject payload)
        {
            switch(message){
                case "RESPONSE":
                    _html[id] = payload["content"].ToString();
                    break;
            }
        }

       

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListenerContext context = _listener.EndGetContext(result);

            try
            {
                _i++;
                HttpListenerRequest request = context.Request;
                JObject payload = new JObject(new JProperty("url", request.RawUrl));
                _messages.Send(_i, "GETHTML", payload);

                //System.Console.WriteLine(context.Request.RawUrl);

                HttpListenerResponse response = context.Response;
                response.Headers.Set("Server", "Demonhunter");
                response.KeepAlive = true;
                while (!_html.ContainsKey(_i))
                {
                    // Waiting a bit ...
                }
                StreamWriter sw = new StreamWriter(response.OutputStream);
                sw.AutoFlush = true;
                sw.Write(_html[_i]);
                _html.Remove(_i);
                response.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
