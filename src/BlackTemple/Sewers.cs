using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlackTemple
{
    class Sewers
    {
        HttpListener _listener;

        public Sewers()
        {
            _listener = new HttpListener();
            string prefix = "http://*:81/";
            _listener.Prefixes.Add(prefix);

            try
            {
               _listener.Start();
                
                while (true)
                {
                    IAsyncResult result = _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
                    result.AsyncWaitHandle.WaitOne();
                    System.Console.WriteLine("Listening");
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
            //Logger.logMsg("Context: End; " + context.Request.RemoteEndPoint.Address + " requesting " + context.Request.RawUrl, 2);


            try
            {
                HttpListenerRequest request = context.Request;

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
