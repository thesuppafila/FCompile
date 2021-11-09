using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class ASSING: IOPER, INODE
    {
        public ID ID;
        public EXPRES expres;
    }
}
