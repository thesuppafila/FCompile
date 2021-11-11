using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class TermNode : INode
    {
        public TermNode term;

        public FactorNode factor;

        public string sign;

        public TermNode(FactorNode factor)
        {
            this.factor = factor;
        }

        public override string ToString()
        {
            if (term != null)
                return String.Format("{0} {1} {2}", term, sign, factor);
            return factor.ToString();
        }
    }
}
