using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class EmptyStack : Exception
    {
        public EmptyStack() : base()
        {
        }

        public override string ToString()
        {
            return "Execution error:\n\t Execution stack is empty, no result can be returned.";
        }
    }
}
