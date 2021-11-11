using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class ProgramNode : INode
    {
        public List<FunctionNode> functionList;

        public ProgramNode()
        {
            functionList = new List<FunctionNode>();
        }

        public ProgramNode(List<FunctionNode> functionList)
        {
            this.functionList = functionList;
        }

        public string ToString(string tab)
        {
            string tree = "PROGRAM \n";
            foreach (FunctionNode func in functionList)
            {
                tree += func.ToString(tab);
            }
            return tree;
        }
    }
}
