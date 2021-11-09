using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class EXPRES: INODE
    {
        public string value;

        public EXPRES(string value)
        {
            this.value = value;
        }
    }
}
