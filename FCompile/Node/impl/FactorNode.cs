using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class FactorNode
    {
        public ExpressionNode expression;

        public IdentifierNode identifier;

        public IntegerNode integer;

        public OperationSign operationSign;

        public string sign;

        public FactorNode()
        {

        }

        public FactorNode(IntegerNode integer)
        {
            this.integer = integer;
        }

        public FactorNode(IdentifierNode identifier)
        {
            this.identifier = identifier;
        }

        public FactorNode(ExpressionNode expression, OperationSign operationSign, IdentifierNode identifier)
        {
            this.expression = expression;
            this.operationSign = operationSign;
            this.identifier = identifier;
        }

        public override string ToString()
        {
            if (expression != null && operationSign != null)
                return String.Format("({0}) {1} ", expression, operationSign, identifier);

            return identifier == null ? integer.ToString() : identifier.ToString();
        }
    }
}
