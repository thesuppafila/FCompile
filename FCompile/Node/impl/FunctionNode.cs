using System;
using System.Collections.Generic;

namespace FCompile.Node
{
    public class FunctionNode : INode
    {
        public string type;

        public string name;

        public List<DeclarationNode> declarationList;

        public List<IOperation> operationList;

        public FunctionNode()
        {
            declarationList = new List<DeclarationNode>();
            operationList = new List<IOperation>();
        }

        public string ToString(string tab)
        {
            string tree = String.Format("{0}FUNCTION TYPE: {1} NAME: {2}\n", tab, type, name);
            tab += Indent.TAB;
            if (declarationList.Count > 0)
            {
                tree += String.Format("{0}PARAMS:\n", tab);
                foreach (DeclarationNode dec in declarationList)
                {
                    tree += dec.ToString(tab);
                }
            }

            if (operationList.Count > 0)
            {
                tree += String.Format("{0}OPERATIONS:\n", tab);
                foreach (IOperation oper in operationList)
                {
                    tree += oper.ToString(tab);
                }
            }

            return tree;
        }


        public override string ToString()
        {
            string result = String.Format("{0} {1}\n", type, name);

            foreach (DeclarationNode declaration in declarationList)
            {
                result += declaration.ToString() + "\n";
            }

            foreach (IOperation oper in operationList)
            {
                result += oper.ToString() + "\n";
            }
            return result;
        }

        public string ToAssString()
        {
            string name;

            if (this.name == "main")
                name = "_start";
            else name = this.name;

            string result = String.Format("{0}:\n push rbp \n mov rbp, rsp \n", name);

            List<IDeclaration> declarations = new List<IDeclaration>();

            if (declarationList.Count > 0)
                foreach (DeclarationNode declaration in declarationList)
                {
                    declarations.Add(declaration);
                }

            foreach (IOperation operation in operationList)
            {
                if (typeof(DeclarationNode).IsInstanceOfType(operation))
                {
                    declarations.Add((DeclarationNode)operation);
                }
            }

            if (declarations.Count > 0)
            {
                result += "section .data:\n";
                foreach (IDeclaration declaration in declarations)
                {
                    result += declaration.ToAssString();
                }
            }

            return result;
        }
    }
}
