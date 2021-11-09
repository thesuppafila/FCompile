using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class ID: INODE
    {
        public string value;

        public ID(string value)
        {
            this.value = value;
        }
    }
}
