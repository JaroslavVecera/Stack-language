using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class Pair : Value, IList
    {
        public Value Head { get; private set; }
        public IList Rest { get; private set; }

        public Pair(Value head, IList rest) : base(ValueType.Pair)
        {
            Head = head;
            Rest = rest;
        }
    }
}
