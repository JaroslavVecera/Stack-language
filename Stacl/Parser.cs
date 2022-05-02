using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacl
{
    public class Parser
    {
        ParserData Data { get; set; }

        public IList ParseItemsTest(string expression)
        {
            Data = new ParserData();
            IList p = ParseList(expression + "}");
            if (Data.Depth != -1)
                throw new NotImplementedException();
            else
                return p;
        }

        IList ParseList(string expr)
        {
            int startDepth = Data.Depth;
            Stack<Value> listItems = new Stack<Value>();
            for (; Data.Character < expr.Length; Data.Character++)
            {
                char c = expr[Data.Character];
                if (char.IsWhiteSpace(c) || c == '{')
                {
                    if (Data.Buffer.Length > 0)
                        listItems.Push(ParseItem(Data.Buffer, Data.LinChar - Data.Buffer.Length + 1, Data.Line + 1));
                    Data.Buffer = "";
                    if (c == '{')
                    {
                        Data.Depth++;
                        Data.Character++;
                        Data.LinChar++;
                        listItems.Push((Value)ParseList(expr));
                    }
                    else if (c == '\n')
                    {
                        if (expr[Data.Character + 1] == '\r')
                        {
                            Data.LinChar++;
                            Data.Character++;
                        }
                        Data.Line++;
                    }
                    else if (c == '\r')
                    {
                        if (expr[Data.Character + 1] == '\n')
                        {
                            Data.LinChar++;
                            Data.Character++;
                        }
                        Data.Line++;
                    }
                }
                else if (c == '}')
                {
                    if (Data.Buffer.Length > 0)
                        listItems.Push(ParseItem(Data.Buffer, Data.LinChar - Data.Buffer.Length + 1, Data.Line + 1));
                    Data.Buffer = "";
                    Data.Depth--;
                    if (Data.Depth + 1 == startDepth)
                        break;
                }
                else
                    Data.Buffer += c;
                Data.LinChar++;
            }
            IList tail = new False();
            while (listItems.Any())
                tail = new Pair(listItems.Pop(), tail);
            return tail;
        }

        Value ParseItem(string item, int character, int line)
        {
            if (char.IsDigit(item[0]) || item[0] == '.')
                return ParseNumber(item, character, line);
            if (item[0] == '{')
                throw new NotImplementedException();
            if (item[0] == '}')
                throw new NotImplementedException();
            else
                return ParseBoolOrWord(item, character, line);
        }

        Value ParseBoolOrWord(string item, int character, int line)
        {
            if (item == "true")
                return new True();
            else if (item == "false")
                return new False();
            else
                return new Word(item);
        }

        Value ParseNumber(string item, int character, int line)
        {
            if (item.Contains('.'))
                return ParseReal(item, character, line);
            else
                return ParseInteger(item, character, line);
        }

        Value ParseReal(string item, int character, int line)
        {
            try
            {
                return new Real(double.Parse(item, CultureInfo.InvariantCulture));
            }
            catch (FormatException e)
            {
                throw new SyntaxException(string.Format("Real value was expected, got {0} instead", item), line, character);
            }
        }

        Value ParseInteger(string item, int character, int line)
        {
            try
            {
                return new Integer(int.Parse(item));
            }
            catch (FormatException e)
            {
                throw new SyntaxException(string.Format("Integer value was expected, got {0} instead", item), line, character);
            }
        }
    }
}
