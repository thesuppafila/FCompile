using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class AssignmentNode : IOperation, INode
    {
        public IdentifierNode identifier;
        public ExpressionNode expression;

        public AssignmentNode()
        {
        }

        public AssignmentNode(IdentifierNode identifier, ExpressionNode expression)
        {
            this.identifier = identifier;
            this.expression = expression;
        }

        public string ToString(string tab)
        {
            tab += Indent.TAB;
            string tree = String.Format("{0}ASSIGN ID: {1}, EXPRESSION: {2}\n", tab, identifier, expression);
            return tree;
        }

        public override string ToString()
        {
            return String.Format("{0} = {1}", identifier, expression);
        }
    }
}
