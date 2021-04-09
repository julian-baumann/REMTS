using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteExecuter.Entities
{
    public class ConsoleResult
    {
        public int Index { get; set; }
        public ConsoleResultStates State { get; set; }
        public RemotePcInfo Pc { get; set; }
        public ConsoleResultItem[] Data { get; set; }
    }
}
