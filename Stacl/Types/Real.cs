using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class Real : Number
    {
        public double Value { get; private set; }

        public Real(double i) : base(NumberType.Real)
        {
            Value = i;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override double GetValue()
        {
            return Value;
        }
    }
}
