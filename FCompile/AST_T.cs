using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public class AST_T
    {
        public AST Type;

        public List<AST_T> Children;

        public string Name;

        public AST_T Value;

        public int DataType;

        public int IntValue;

        public AST_T(int type)
        {
            Type = (AST)type;

            if (Type == AST.AST_COMPOUND)
                Children = new List<AST_T>();
        }

        public string ToString(int index)
        {
            if (Children != null)
            {
                foreach (AST_T child in Children)
                    child.ToString(index++);
            }
            String str = "  ";
            if (Value != null)
            {
                for (int i = 0; i < index; i++)
                    str += str;
                str += Value.ToString(index);
            }
            return str;
        }

        public override string ToString()
        {
            return String.Format("<AST_TYPE = {0}>, <NAME = {1}>, <INT_VALUE = {2}>", Type, Name, IntValue);
        }
    }

}
