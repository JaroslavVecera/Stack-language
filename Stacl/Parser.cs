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
        public Stack<Value> ParseItems(string expr)
        {
            List<Value> result = new List<Value>();
            int line = 0;
            string buffer = "";
            int character;
            int linChar = 0;
            for (character = 0; character < expr.Length; character++)
            {
                char c = expr[character];
                if (char.IsWhiteSpace(c) || c == '{')
                {
                    if (buffer.Length > 0)
                        result.Add(ParseItem(buffer, linChar - buffer.Length + 1, line + 1));
                    buffer = "";
                    if (c == '\n')
                    {
                        if (expr[character + 1] == '\r')
                        {
                            linChar++;
                            character++;
                        }
                        line++;
                    }
                    else if (c == '\r')
                    {
                        if (expr[character + 1] == '\n')
                        {
                            linChar++;
                            character++;
                        }
                        line++;
                    }
                }
                else
                {
                    buffer += c;
                }
                linChar++;
            }
            if (buffer.Length > 0)
                result.Add(ParseItem(buffer, linChar - buffer.Length + 1, line + 1));
            result.Reverse();
            return new Stack<Value>(result);
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
                return new Boolean(true);
            else if (item == "false")
                return new Boolean(false);
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
