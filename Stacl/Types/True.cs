using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class True : Value
    {
        public True() : base(ValueType.Boolean) { }

        public override bool ToBool()
        {
            return true;
        }

        public override string ToString()
        {
            return "true";
        }
    }
}
