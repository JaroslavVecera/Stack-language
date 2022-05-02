using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class ListEnvironmentFactory : IEnvironmentFactory
    {
        public void Generate(StaclMachine machine)
        {
            GenerateFirst(machine);
        }

        void GenerateFirst(StaclMachine machine)
        {
            machine.Environment["first"] = new BuildInFunction(() =>
            {
                if (machine.ExeCount < 1)
                {
                    int available = machine.ExeCount;
                    machine.Exe.Clear();
                    machine.Exe.Push(Error.ArityError("first", 1, available));
                }
                else
                {
                    Value v = machine.Exe.Pop();
                    if (v.Type != ValueType.Pair && v.ToBool())
                    {
                        machine.Exe.Clear();
                        throw new NotImplementedException();
                    }
                    machine.Exe.Push(((IList)v).Head);
                }
            });
        }
    }
}
