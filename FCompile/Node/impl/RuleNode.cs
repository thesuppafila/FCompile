using System;

namespace FCompile.Node
{
    public class RuleNode: INode
    {
        public ExpressionNode expression1;
        public ExpressionNode expression2;
        public SignNode sign;

        public string ToString(string tab)
        {
            tab += Indent.TAB;
            return String.Format("{0}RULE: {1} {2} {3}\n", tab, expression1, sign, expression2);
        }

        public override string ToString()
        {
            
            return String.Format("{0} {1} {2}\n", expression1, sign, expression2);
        }
    }
}
