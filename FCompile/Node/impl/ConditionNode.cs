using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile.Node
{
    public class ConditionNode : IOperation, INode
    {
        public RuleNode rule;

        public List<IOperation> operationList;

        public ConditionNode()
        {
            operationList = new List<IOperation>();
        }

        public ConditionNode(RuleNode rule, List<IOperation> operationList)
        {
            this.rule = rule;
            this.operationList = operationList;
        }

        public string ToString(string tab)
        {
            tab += Indent.TAB;
            string tree = String.Format("{0}CONDITION\n{1}", tab, rule.ToString(tab));
            tab += Indent.TAB;
            if (operationList.Count > 0)
            {
                tree += String.Format("{0}OPERATION:\n", tab);
                foreach (IOperation oper in operationList)
                {
                    tree += oper.ToString(tab);
                }
            }
            return tree;
        }

        public override string ToString()
        {
            string result = String.Format("if ({0})\n", rule);
            foreach (IOperation oper in operationList)
            {
                result += oper.ToString() + "\n";
            }
            return result;
        }
    }
}
