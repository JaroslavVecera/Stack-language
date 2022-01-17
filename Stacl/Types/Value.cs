using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public enum ValueType
    {
        Number,
        Word,
        Pair,
        Boolean,
        Error,
        BIF
    }

    public abstract class Value
    {
        public ValueType Type { get; private set; }

        public Value(ValueType type)
        {
            Type = type;
        }

        public virtual bool ToBool()
        {
            return true;
        }
    }
}
