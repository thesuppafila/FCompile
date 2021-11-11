using System;

namespace FCompile.Node
{
    public class ExpressionNode : INode
    {
        public ExpressionNode expression;

        public TermNode term;

        public OperationSign operationSign;

        public string sign;

        public ExpressionNode()
        {
        }

        public ExpressionNode(ExpressionNode expression)
        {
            this.expression = expression;
        }

        public ExpressionNode(TermNode term)
        {
            this.term = term;
        }

        public ExpressionNode(ExpressionNode expression, OperationSign operationSign, TermNode term)
        {
            this.expression = expression;
            this.operationSign = operationSign;
            this.term = term;
        }

        public override string ToString()
        {
            if (expression != null)
                return String.Format("{0} {1} {2}", expression, operationSign, term);
            return term.ToString();
        }
    }
}
