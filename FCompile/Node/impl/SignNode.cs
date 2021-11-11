using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class SignNode
    {
        public SG_TYPE value;

        public SignNode(SG_TYPE type)
        {
            this.value = type;
        }

        public override string ToString()
        {
            return value.ToString();
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
