using System;

namespace FCompile.Node
{
    public class InputNode : IOperation, INode
    {
        public IdentifierNode identifier;

        public InputNode()
        {

        }

        public InputNode(IdentifierNode identifier)
        {
            this.identifier = identifier;
        }

        public string ToString(string tab)
        {
            tab += Indent.TAB;
            string tree = String.Format("{0}INPUT ID: {1}\n", tab, identifier);
            return tree;
        }

        public override string ToString()
        {
            return String.Format("cin >> {0}", identifier);
        }
    }
}
