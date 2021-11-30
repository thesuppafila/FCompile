using System;

namespace FCompile.Node
{
    public class ExpressionNode : INode
    {
        public IExp root;

        public ExpressionNode(IExp root)
        {
            this.root = root;
        }

        public override string ToString()
        {
            //if (operationSign != null)
            //    return String.Format("{0} {1} {2}", expression, operationSign, term);
            //return term.ToString();
            return root.ToString();
        }
    }
}
