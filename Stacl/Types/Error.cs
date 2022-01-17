using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class Error : Value
    {
        public Word Name { get;  private set; }
        public string Info { get; private set; } = "";

        public Error(Word name) : base(ValueType.Error)
        {
            Name = name;
        }

        public Error(Word name, string info) : base(ValueType.Error)
        {
            Name = name;
            Info = info;
        }

        public override string ToString()
        {
            return $"{Name.String}:\n\t{Info}";
        }

        public static Error MissingVariable(string name)
        {
            return new Error(new Word("Variable Error"), $"The value for the variable \"{name}\" is missing.");
        }

        public static Error ArityError(string operation, int required, int available)
        {
            return new Error(new Word("Arity Error"), $"The operation {operation} requires {required} arguments but {available} {(available > 1 ? "are" : "is")} available.");
        }
    }
}
