using System;
using System.Threading;

namespace Stacl
{
    class Program
    {
        static void Main(string[] args)
        {
            REPL repl = new REPL();
            repl.Run();
        }
    }
}
