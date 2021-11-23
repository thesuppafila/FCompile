using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class OperationSign: INode, IExp
    {
        public string value;

        public IExp left;

        public IExp right;

        public OperationSign()
        {

        }

        public OperationSign(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public string GetValue()
        {
            return value;
        }

        public TokenType GetTokenType()
        {
            return TokenType.OPERATION;
        }

    }
}
