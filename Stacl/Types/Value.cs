using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public enum ValueType
    {
        Number,
        Word,
        Pair,
        Boolean,
        Error,
        BIF
    }

    static class ValueTypeExtensions
    {
        public static string GetLabel(this ValueType type)
        {
            switch (type)
            {
                case ValueType.Number: return "number";
                case ValueType.Word: return "word";
                case ValueType.Pair: return "pair";
                case ValueType.Boolean: return "bool";
                case ValueType.Error: return "error";
                case ValueType.BIF: return "function";
                default: return "";
            }
        }
    }

    public abstract class Value
    {
        public ValueType Type { get; private set; }

        public Value(ValueType type)
        {
            Type = type;
        }

        public virtual bool ToBool()
        {
            return true;
        }
    }
}
