using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class TYPE: INODE
    {
        public string value;

        public TYPE(string value)
        {
            this.value = value;
        }
    }
}
