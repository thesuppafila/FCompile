using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class OperationSign
    {
        string value;

        public OperationSign()
        {

        }

        public OperationSign(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
