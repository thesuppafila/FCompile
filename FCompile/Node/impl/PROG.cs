using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class PROG: INODE
    {
        public List<FUNC> FUNCLIST;

        public PROG()
        {
            FUNCLIST = new List<FUNC>();
        }
    }
}
