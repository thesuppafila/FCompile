using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class IdentifierNode : INode, IExp
    {
        public string name; 

        public ExpressionNode expression; // если i = a + b;

        public string value; //если i = 5;

        public IdentifierNode()
        {

        }

        public IdentifierNode(string name)
        {
            this.name = name;
        }

        public IdentifierNode(string name, ExpressionNode expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public IdentifierNode(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return name;
        }

        public string GetValue()
        {
            return this.value;
        }

        public TokenType GetTokenType()
        {
            return TokenType.ID;
        }
    }
}
