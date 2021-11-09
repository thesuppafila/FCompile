using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class ID: INODE
    {
        public string name;

        public string expres;

        public ID()
        {
        
        }

        public ID(string value)
        {
            this.expres = value;
        }
    }
}
