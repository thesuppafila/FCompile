using System;

namespace FCompile.Node
{
    public class OutputNode : IOperation, INode
    {
        public IdentifierNode identifier;

        public string value;

        public string ToString(string tab)
        {
            tab += Indent.TAB;
            string tree = String.Format("{0}OUTPUT VALUE: {1}", tab, value);
            return tree;
        }

        public override string ToString()
        {
            return identifier == null ? value : identifier.ToString();
        }
    }
}
