using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class ParserData
    {
        public int Line { get; set; } = 0;
        public string Buffer { get; set; } = "";
        public int Character { get; set; }
        public int LinChar { get; set; } = 0;
        public int Depth { get; set; } = 0;
    }
}
