using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class BuildInFunction : Value
    {
        public Action Function { get; private set; }

        public BuildInFunction(Action function) : base(ValueType.BIF)
        {
            Function = function;
        }
    }
}
