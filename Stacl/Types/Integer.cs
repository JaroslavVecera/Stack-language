using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class Integer : Number
    {
        public int Value { get; private set; }

        public Integer(int i) : base(NumberType.Integer)
        {
            Value = i;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override double GetValue()
        {
            return Value;
        }
    }
}
