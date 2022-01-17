using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class SyntaxException : Exception
    {
        public int Line { get; set; }
        public int Character { get; set; }

        public SyntaxException(string message, int line, int character, Exception innerException) : base(message, innerException)
        {
            Line = line;
            Character = character;
        }

        public SyntaxException(string message, int line, int character) : base(message)
        {
            Line = line;
            Character = character;
        }

        public override string ToString()
        {
            return string.Format("Syntax error on line {0} character {1}.\n\t", Line, Character) + Message; 
        }
    }
}
