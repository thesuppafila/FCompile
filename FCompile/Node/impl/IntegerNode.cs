namespace FCompile.Node
{
    public class IntegerNode : INode
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
    }
}
