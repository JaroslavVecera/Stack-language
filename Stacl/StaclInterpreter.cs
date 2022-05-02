using System;
using System.Collections.Generic;
using System.IO;

namespace Stacl
{
    public class StaclInterpreter
    {
        StaclMachine StaclMachine { get; set; } = new StaclMachine();
        Parser Parser = new Parser();

        public Value Eval(string expr)
        {
            StaclMachine.Clear();
            StaclMachine.Code = Parser.ParseItemsTest(expr);
            while (StaclMachine.AnyCode)
            {
                Value val = StaclMachine.PopCode();
                Eval(val);
            }
            if (!StaclMachine.AnyExe)
                throw new EmptyStack();
            return StaclMachine.Exe.Pop();
        }

        void Eval(Value v)
        {
            if (StaclMachine.IsError)
            {
                if (v.Type == ValueType.Word && ((Word)v).IsResolveToken)
                    StaclMachine.Exe.Push(((Error)StaclMachine.Exe.Pop()).Name);
                return;
            }
            if (v.Type == ValueType.Number || v.Type == ValueType.Boolean)
                StaclMachine.Exe.Push(v);
            else if (v.Type == ValueType.Word)
                EvalWord((Word)v);
            else if (v.Type == ValueType.Pair)
                EvalList((Pair)v);
        }

        void EvalList(IList p)
        {
            Stack<Value> s = new Stack<Value>();
            while (p.ToBool())
            {
                s.Push(p.Head);
                p = p.Rest;
            }
            while (s.Count > 0)
                StaclMachine.Code = new Pair(s.Pop(), StaclMachine.Code);
        }

        void EvalWord(Word w)
        {
            if (w.IsResolveToken)
                StaclMachine.Exe.Push(new False());
            else
            {
                StaclMachine.Exe.Push(WordBinding(w));
                if (StaclMachine.Exe.Peek().Type == ValueType.BIF)
                {
                    BuildInFunction f = (BuildInFunction)(StaclMachine.Exe.Pop());
                    f.Function.Invoke();
                }
            }
        }

        Value WordBinding(Word w)
        {
            if (StaclMachine.Environment.ContainsKey(w.String))
                return StaclMachine.Environment[w.String];
            else
                return Error.MissingVariable(w.String);
        }
    }
}