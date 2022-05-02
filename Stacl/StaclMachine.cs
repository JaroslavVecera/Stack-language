using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class StaclMachine
    {
        public IList Code { get; set; }
        public Stack<Value> Exe { get; } = new Stack<Value>();
        public Dictionary<string, Value> Environment { get; set; } = new Dictionary<string, Value>();

        public int ExeCount { get { return Exe.Count; } }
        public bool AnyCode { get { return Code.ToBool(); } } 
        public bool AnyExe { get { return Exe.Count > 0; } }
        public bool IsError { get { return AnyExe && Exe.Peek().Type == ValueType.Error; } }

        public void Clear()
        {
            Code = null;
            Exe.Clear();

            GenerateEnvironment();
        }

        void GenerateEnvironment()
        {
            List<IEnvironmentFactory> factories = new List<IEnvironmentFactory>()
            {
                new AritmeticEnvironmentFactory(),
                new FlowEnvironmentFactory(),
                new ListEnvironmentFactory()
            };
            factories.ForEach(f => f.Generate(this));
        }

        public void RaiseError(Error e)
        {
            Exe.Clear();
            Exe.Push(e);
        }

        public Value PopCode()
        {
            Value head = Code.Head;
            Code = Code.Rest;
            return head;
        }
    }
}
