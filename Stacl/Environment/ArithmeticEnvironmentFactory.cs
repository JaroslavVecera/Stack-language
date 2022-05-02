using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    class ArithmeticEnvironmentFactory : IEnvironmentFactory
    {
        public void Generate(StaclMachine machine)
        {
            GeneratePlus(machine);
            GenerateMinus(machine);
            GenerateMultiple(machine);
            GenerateDivide(machine);
            GenerateModulo(machine);
        }

        void GeneratePlus(StaclMachine machine)
        {
            machine.Environment["+"] = new BuildInFunction(() =>
            {
                if (machine.ExeCount < 2)
                {
                    int available = machine.ExeCount;
                    machine.RaiseError(Error.ArityError("+", 2, available));
                }
                else
                {
                    List<Value> values = new List<Value>() { machine.Exe.Pop(), machine.Exe.Pop() };
                    if (values.Any(v => v.Type != ValueType.Number))
                    {
                        machine.RaiseError(Error.TypeError("+", values.FindIndex(v => v.Type != ValueType.Number),
                            ValueType.Number, values.First(v => v.Type != ValueType.Number).Type));
                        return;
                    }
                    List<Number> numbers = values.Cast<Number>().ToList();
                    if (numbers.Any(n => n.NumberType == NumberType.Real))
                        machine.Exe.Push(new Real(numbers[1].GetValue() + numbers[0].GetValue()));
                    else
                        machine.Exe.Push(new Integer(((Integer)numbers[1]).Value + ((Integer)numbers[0]).Value));
                }
            });
        }

        void GenerateMinus(StaclMachine machine)
        {
            machine.Environment["-"] = new BuildInFunction(() =>
            {
                if (machine.ExeCount < 2)
                {
                    int available = machine.ExeCount;
                    machine.RaiseError(Error.ArityError("-", 2, available));
                }
                else
                {
                    List<Value> values = new List<Value>() { machine.Exe.Pop(), machine.Exe.Pop() };
                    if (values.Any(v => v.Type != ValueType.Number))
                    {
                        machine.RaiseError(Error.TypeError("-", values.FindIndex(v => v.Type != ValueType.Number),
                            ValueType.Number, values.First(v => v.Type != ValueType.Number).Type));
                        return;
                    }
                    List<Number> numbers = values.Cast<Number>().ToList();
                    if (numbers.Any(n => n.NumberType == NumberType.Real))
                        machine.Exe.Push(new Real(numbers[1].GetValue() - numbers[0].GetValue()));
                    else
                        machine.Exe.Push(new Integer(((Integer)numbers[1]).Value - ((Integer)numbers[0]).Value));
                }
            });
        }

        void GenerateMultiple(StaclMachine machine)
        {
            machine.Environment["*"] = new BuildInFunction(() =>
            {
                if (machine.ExeCount < 2)
                {
                    int available = machine.ExeCount;
                    machine.RaiseError(Error.ArityError("*", 2, available));
                }
                else
                {
                    List<Value> values = new List<Value>() { machine.Exe.Pop(), machine.Exe.Pop() };
                    if (values.Any(v => v.Type != ValueType.Number))
                    {
                        machine.RaiseError(Error.TypeError("*", values.FindIndex(v => v.Type != ValueType.Number),
                            ValueType.Number, values.First(v => v.Type != ValueType.Number).Type));
                        return;
                    }
                    List<Number> numbers = values.Cast<Number>().ToList();
                    if (numbers.Any(n => n.NumberType == NumberType.Real))
                        machine.Exe.Push(new Real(numbers[1].GetValue() * numbers[0].GetValue()));
                    else
                        machine.Exe.Push(new Integer(((Integer)numbers[1]).Value * ((Integer)numbers[0]).Value));
                }
            });
        }

        void GenerateDivide(StaclMachine machine)
        {
            machine.Environment["/"] = new BuildInFunction(() =>
            {
                if (machine.ExeCount < 2)
                {
                    int available = machine.ExeCount;
                    machine.RaiseError(Error.ArityError("/", 2, available));
                }
                else
                {
                    List<Value> values = new List<Value>() { machine.Exe.Pop(), machine.Exe.Pop() };
                    if (values.Any(v => v.Type != ValueType.Number))
                    {
                        machine.RaiseError(Error.TypeError("/", values.FindIndex(v => v.Type != ValueType.Number),
                            ValueType.Number, values.First(v => v.Type != ValueType.Number).Type));
                        return;
                    }
                    List<Number> numbers = values.Cast<Number>().ToList();
                    if (numbers[0].GetValue() == 0)
                    {
                        machine.RaiseError(Error.DivisionError("/"));
                        return;
                    }
                    if (numbers.Any(n => n.NumberType == NumberType.Real))
                        machine.Exe.Push(new Real(numbers[1].GetValue() / numbers[0].GetValue()));
                    else
                        machine.Exe.Push(new Integer(((Integer)numbers[1]).Value / ((Integer)numbers[0]).Value));
                }
            });
        }

        void GenerateModulo(StaclMachine machine)
        {
            machine.Environment["%"] = new BuildInFunction(() =>
            {
                if (machine.ExeCount < 2)
                {
                    int available = machine.ExeCount;
                    machine.RaiseError(Error.ArityError("%", 2, available));
                }
                else
                {
                    List<Value> values = new List<Value>() { machine.Exe.Pop(), machine.Exe.Pop() };
                    if (values.Any(v => v.Type != ValueType.Number))
                    {
                        machine.RaiseError(Error.TypeError("%", values.FindIndex(v => v.Type != ValueType.Number),
                            ValueType.Number, values.First(v => v.Type != ValueType.Number).Type));
                        return;
                    }
                    List<Number> numbers = values.Cast<Number>().ToList();
                    if (numbers[0].GetValue() == 0)
                    {
                        machine.RaiseError(Error.DivisionError("%"));
                        return;
                    }
                    if (numbers.Any(n => n.NumberType == NumberType.Real))
                    {
                        double x = numbers[1].GetValue();
                        double y = numbers[0].GetValue();
                        machine.Exe.Push(new Real(x - (int)(x / y) * y));
                    }
                    else
                        machine.Exe.Push(new Integer(((Integer)numbers[1]).Value % ((Integer)numbers[0]).Value));
                }
            });
        }
    }
}
