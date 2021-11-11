using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class TypeNode: INode
    {
        public string value;

        public TypeNode(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
