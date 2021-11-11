using System;

namespace FCompile.Node
{
    public class ReturnNode : INode, IOperation
    {
        public IdentifierNode identifier;

        public string value;

        public ReturnNode()
        {

        }

        public ReturnNode(string value)
        {
            this.value = value;
        }

        public ReturnNode(IdentifierNode identifier)
        {
            this.identifier = identifier;
        }

        public string ToString(string tab)
        {
            tab += Indent.TAB;
            string tree = String.Format("{0}RETURN VALUE: {1}", tab, value);
            return tree;
        }

        public override string ToString()
        {
            return identifier == null ? value : identifier.ToString();
        }
    }
}
