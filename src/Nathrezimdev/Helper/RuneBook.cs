using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneBookLogger
{
    class RuneBook
    {
        private string _name;

        public RuneBook(string name)
        {
            _name = name;
        }

        public void log(string message)
        {

            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + _name + ": " + message );
        }
    }
}
