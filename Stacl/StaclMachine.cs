using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class StaclMachine
    {
        public Stack<Value> Code { get; set; } = new Stack<Value>();
        public Stack<Value> Exe { get; } = new Stack<Value>();
        public Dictionary<string, Value> Environment { get; set; } = new Dictionary<string, Value>();

        public int CodeCount { get { return Code.Count; } }
        public int ExeCount { get { return Code.Count; } }
        public bool AnyCode { get { return Code.Count > 0; } } 
        public bool AnyExe { get { return Exe.Count > 0; } }
        public bool IsError { get { return AnyExe && Exe.Peek().Type == ValueType.Error; } }

        public void Clear()
        {
            Code.Clear();
            Exe.Clear();

            var factory = new EnvironmentFactory();
            factory.Generate(this);
        }
    }
}
