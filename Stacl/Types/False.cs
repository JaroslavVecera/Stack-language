using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class False : Value, IList
    {
        public False() : base(ValueType.Boolean) { }
        public Value Head { get { return this; } }
        public IList Rest { get { return this; } }

        public override bool ToBool()
        {
            return false;
        }

        public override string ToString()
        {
            return "false";
        }
    }
}
