using System;

namespace FCompile.Node
{
    public class DeclarationNode : INode, IOperation, IDeclaration
    {
        public TypeNode type;

        public IdentifierNode identifier;

        public DeclarationNode()
        {

        }

        public DeclarationNode(TypeNode type, IdentifierNode identifier)
        {
            this.type = type;
            this.identifier = identifier;
        }

        public string ToString(string tab)
        {
            tab += "    ";
            return String.Format("{0}VARIABLE TYPE: {1} ID: {2}\n", tab, type, identifier);
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", type, identifier);
        }

        public string ToAssString()
        {
            return String.Format("{0} db 0\n", identifier.name);
        }
    }
}
