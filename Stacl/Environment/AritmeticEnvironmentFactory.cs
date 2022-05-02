using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class AritmeticEnvironmentFactory : IEnvironmentFactory
    {
        public void Generate(StaclMachine machine)
        {
            GeneratePlus(machine);
            GenerateMinus(machine);
            GenerateMultiply(machine);
            GenerateDivide(machine);
        }

        void GeneratePlus(StaclMachine machine)
        {
            GenerateOperation(machine, "+", 2, l => l[0] + l[1], l => l[0] + l[1], null);
        }

        void GenerateMinus(StaclMachine machine)
        {
            GenerateOperation(machine, "-", 2, l => l[0] - l[1], l => l[0] - l[1], null);
        }

        void GenerateMultiply(StaclMachine machine)
        {
            GenerateOperation(machine, "*", 2, l => l[0] * l[1], l => l[0] * l[1], null);
        }

        void GenerateDivide(StaclMachine machine)
        {
            GenerateOperation(machine, "/", 2, l => l[0] / l[1], l => l[0] / l[1], new List<Func<double, bool>>() {
                i => true,
                i => {
                    if (i == 0)
                        machine.RaiseError(Error.DivisionError("/"));
                    return i != 0;
                }});
        }

        void GenerateOperation(StaclMachine machine, string token, int arity, Func<List<int>, int> intOp, Func<List<double>, double> floatOp, List<Func<double, bool>> customeErrors)
        {
            if (customeErrors != null && customeErrors.Count != arity)
                throw new Exception();
            machine.Environment[token] = new BuildInFunction(() =>
            {
                if (machine.ExeCount < arity)
                {
                    int available = machine.ExeCount;
                    machine.RaiseError(Error.ArityError(token, arity, available));
                }
                else
                {
                    List<Value> values = new List<Value>();
                    for (int i = 0; i < arity; i++)
                        values.Add(machine.Exe.Pop());
                    if (values.Any(v => v.Type != ValueType.Number))
                    {
                        machine.RaiseError(Error.TypeError(token, (values.FindIndex(v => v.Type != ValueType.Number)),
                            ValueType.Number, values.First(v => v.Type != ValueType.Number).Type));
                        return;
                    }
                    List<Number> numbers = values.Cast<Number>().ToList();
                    if (customeErrors != null)
                    {
                        for (int i = 0; i < arity; i++)
                        {
                            if (!customeErrors[i].Invoke(numbers[i].GetValue()))
                                return;
                        }
                    }
                    if (numbers.Any(n => n.NumberType == NumberType.Real))
                        machine.Exe.Push(new Real(floatOp(numbers.Select(n => n.GetValue()).ToList())));
                    else
                        machine.Exe.Push(new Integer(intOp(numbers.Cast<Integer>().Select(i => i.Value).ToList())));
                }
            });
        }
    }
}
