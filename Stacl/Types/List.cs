using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class Pair : Value
    {
        public Value Head { get; private set; }
        public Value Rest { get; private set; }

        public Pair(Value head, Value rest) : base(ValueType.Pair)
        {
            Head = head;
            Rest = rest;
        }
    }
}
