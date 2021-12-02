using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public class Rule
    {
        public TokenType leftPart;

        public List<TokenType> rightPart;

        public Rule(TokenType leftPart, List<TokenType> rightPart)
        {
            this.leftPart = leftPart;
            this.rightPart = rightPart;
        }

        public Rule(TokenType leftPart)
        {
            this.leftPart = leftPart;
        }

        public override string ToString()
        {
            string result = string.Empty;
            foreach (TokenType type in rightPart)
            {
                result += type.ToString() + ' ';
            }
            return String.Format("{0} -> {1}", leftPart.ToString(), result);
        }
    }
}
