using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class FlowEnvironmentFactory : IEnvironmentFactory
    {
        public void Generate(StaclMachine machine)
        {
            GenerateNoExe(machine);
        }

        void GenerateNoExe(StaclMachine machine)
        {
            machine.Environment["noex"] = new BuildInFunction(() =>
            {
                machine.Exe.Push(machine.PopCode());
            });
        }
    }
}
