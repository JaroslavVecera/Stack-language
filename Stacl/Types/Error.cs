using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class Error : Value
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

        public Error(string name) : base(ValueType.Error)
        {
            Name = new Word(name);
        }

        public Error(string name, string info) : base(ValueType.Error)
        {
            Name = new Word(name);
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

        public static Error TypeError(string operation, int operand, ValueType desiredType, ValueType givenType)
        {
            return new Error("Type Error", $"The operation {operation} takes value of type {desiredType.GetLabel()} as {NthToString(operand)} operand, but" +
                $" value of type {givenType.GetLabel()} was given.");
        }

        public static Error DivisionError(string operation)
        {
            return new Error("Division Error", $"The second operand of operation {operation} can not be 0.");
        }

        static string NthToString(int nth)
        {
            if (nth == 1)
                return "1st";
            else if (nth == 2)
                return "2nd";
            else if (nth == 3)
                return "3rd";
            else return nth + "th";
        }
    }
}
