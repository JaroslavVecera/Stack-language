using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class REPL
    {
        public string ExpressionHead { get; set; } = ">>> ";
        public string LineHead { get; set; } = "  > ";
        public string ExpresionEnd { get; set; } = ";";

        public void Run()
        {
            StaclInterpreter i = new StaclInterpreter();
            while (true)
            {
                string expr = Read();
                Value v;
                try
                {
                    v = i.Eval(expr);
                }
                catch (SyntaxException e)
                {
                    PrintError(e.ToString());
                    continue;
                }
                catch (EmptyStack e)
                {
                    PrintError(e.ToString());
                    continue;
                }
                PrintResult(v);
            }
        }

        string Read()
        {
            bool finished = false;
            string res = "";
            Console.Write(ExpressionHead);
            while (!finished)
            {
                string line = Console.ReadLine() + Environment.NewLine;
                if (line.Contains(ExpresionEnd))
                    finished = true;
                else
                    Console.Write(LineHead);
                res += line.Split(ExpresionEnd)[0];
            }
            return res;
        }

        void PrintResult(Value v)
        {
            if (v.Type != ValueType.Error)
            {
                Console.WriteLine();
                Console.WriteLine(v);
                Console.WriteLine();
            }
            else
            {
                PrintError(v.ToString());
            }
        }

        void PrintError(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(s);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
