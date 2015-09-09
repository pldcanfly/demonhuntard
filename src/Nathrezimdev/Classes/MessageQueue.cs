using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nathrezim
{
    class MessageQueue
    {
        private Queue<DemonicMessage> _urgent = new Queue<DemonicMessage>();
        private Queue<DemonicMessage> _queue = new Queue<DemonicMessage>();

        public void Enqueue(DemonicMessage message)
        {
            if (message.Urgent)
            {
                _urgent.Enqueue(message);
            } else
            {
                _queue.Enqueue(message);
            }
        }

        public DemonicMessage Dequeue()
        {
            if(_urgent.Count > 0)
            {
                return _urgent.Dequeue();
            }else if(_queue.Count > 0)
            {
                return _queue.Dequeue();
            }

            return null;
        }

        public bool hasElements()
        {
            if (_urgent.Count > 0 || _queue.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
