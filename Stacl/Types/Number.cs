using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public enum NumberType
    {
        Integer, Real
    }

    public abstract class Number : Value
    {
        public NumberType NumberType { get; private set; }

        public Number(NumberType type) : base(ValueType.Number)
        {
            NumberType = type;
        }

        public abstract double GetValue();
    }
}
