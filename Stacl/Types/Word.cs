using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class Word : Value
    {
        public static string ResolveToken { get; } = "resolve";
        public string String { get; private set; }
        public bool IsResolveToken { get { return String == ResolveToken; } }

        public Word(string str) : base(ValueType.Word)
        {
            String = str;
        }

        public override string ToString()
        {
            return $"\"{String}\"";
        }
    }
}
