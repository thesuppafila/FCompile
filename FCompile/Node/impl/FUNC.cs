using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class FUNC: INODE
    {
        public string type;

        public string name;

        public List<DEC> DECLIST;

        public List<IOPER> OPERLIST;

        public FUNC()
        {
            DECLIST = new List<DEC>();
            OPERLIST = new List<IOPER>();
        }
    }
}
