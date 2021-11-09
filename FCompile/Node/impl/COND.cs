using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class COND: IOPER, INODE
    {
        public RL RL;

        public List<IOPER> operList;

        public COND()
        {
            operList = new List<IOPER>();
        }
    }
}
