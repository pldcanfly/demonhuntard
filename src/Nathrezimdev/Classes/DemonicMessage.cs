using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nathrezim
{
    [ProtoContract]
    class DemonicMessage
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public bool Urgent { get; set; }
        [ProtoMember(4)]
        public string Payload { get; set; }
        
    }
}
