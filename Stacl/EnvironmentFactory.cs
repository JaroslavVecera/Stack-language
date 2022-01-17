using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class EnvironmentFactory
    {
        public void Generate(StaclMachine machine)
        {
            GenerateBIFs(machine);
        }

        void GenerateBIFs(StaclMachine machine)
        {
            GenerateAritmetic(machine);
        }

        void GenerateAritmetic(StaclMachine machine)
        {
            GeneratePlus(machine);
            GenerateMinus(machine);
        }

        void GeneratePlus(StaclMachine machine)
        {
            machine.Environment["+"] = new BuildInFunction(() =>
            {
                if (machine.Exe.Count < 2)
                {
                    int available = machine.ExeCount;
                    machine.Exe.Clear();
                    machine.Exe.Push(Error.ArityError("+", 2, available));
                }
                else
                {
                    Value v1 = machine.Exe.Pop();
                    Value v2 = machine.Exe.Pop();
                    if (v1.Type != ValueType.Number || v2.Type != ValueType.Number)
                        throw new NotImplementedException();
                    machine.Exe.Push(new Integer(((Integer)v1).Value + ((Integer)v2).Value));
                }
            });
        }

        void GenerateMinus(StaclMachine machine)
        {
            machine.Environment["-"] = new BuildInFunction(() =>
            {
                if (machine.Exe.Count < 2)
                {
                    int available = machine.ExeCount;
                    machine.Exe.Clear();
                    machine.Exe.Push(Error.ArityError("-", 2, available));
                }
                else
                {
                    Value v1 = machine.Exe.Pop();
                    Value v2 = machine.Exe.Pop();
                    if (v1.Type != ValueType.Number || v2.Type != ValueType.Number)
                        throw new NotImplementedException();
                    machine.Exe.Push(new Integer(((Integer)v2).Value - ((Integer)v1).Value));
                }
            });
        }

    }
}
