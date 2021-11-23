namespace FCompile.Node
{
    public class IntegerNode : INode, IExp
    {
        public string value;

        public IntegerNode()
        {

        }

        public IntegerNode(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public string GetValue()
        {
            return value;
        }

        public TokenType GetTokenType()
        {
            return TokenType.INT;
        }
    }
}
