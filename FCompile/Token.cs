using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public class Token
    {
        public string Value { get; set; }

        public TokenType Type { get; set; }

        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }

        public Token(TokenType type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return String.Format("<type={0}>, <int_type={1}>, <value={2}>", Type, (int)Type, Value);
        }
    }


}
