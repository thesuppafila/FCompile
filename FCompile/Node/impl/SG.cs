using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class SG
    {
        public SG_TYPE value;

        public SG(SG_TYPE type)
        {
            this.value = type;
        }
    }

    public enum SG_TYPE
    {
        LT,
        GT,
        LTE,
        GTE,
        NOTEQ,
        EQ
    }
}
