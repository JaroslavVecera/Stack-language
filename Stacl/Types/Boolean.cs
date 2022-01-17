using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class Boolean : Value
    {
        public bool Value { get; private set; }

        public Boolean(bool b) : base(ValueType.Boolean)
        {
            Value = b;
        }

        public override bool ToBool()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public Boolean(Value b) : base(ValueType.Boolean)
        {
            Value = b.ToBool();
        }
    }
}
