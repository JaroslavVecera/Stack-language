using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public interface IList
    {
        public bool ToBool();
        public Value Head { get; }
        public IList Rest { get; }
    }
}
