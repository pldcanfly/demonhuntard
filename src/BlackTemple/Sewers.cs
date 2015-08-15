using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlackTemple.Messages;

namespace BlackTemple
{
    class Sewers
    {
        HttpListener _listener;
        Sender _messages;

        public Sewers()
        {
            _listener = new HttpListener();
            string prefix = "http://*:81/";
            _listener.Prefixes.Add(prefix);
            _messages = new Sender();

            try
            {
               _listener.Start();
                while (true)
                {
                    System.Console.WriteLine("Listening");
                    IAsyncResult result = _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
                    result.AsyncWaitHandle.WaitOne();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListenerContext context = _listener.EndGetContext(result);

            try
            {
                HttpListenerRequest request = context.Request;
                this._messages.Send("CONNECTION", context.Request.RawUrl);
                System.Console.WriteLine(context.Request.RawUrl);

                HttpListenerResponse response = context.Response;
                response.Headers.Set("Server", "Demonhunter");
                response.KeepAlive = true;

                response.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
