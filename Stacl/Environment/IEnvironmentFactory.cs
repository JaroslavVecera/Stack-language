using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public interface IEnvironmentFactory
    {
        public void Generate(StaclMachine machine);
    }
}
