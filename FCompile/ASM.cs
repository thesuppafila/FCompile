using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public class ASM
    {

        public ASM()
        {

        }

        private string AS_F_compound(AST_T ast)
        {
            string value = string.Empty;
            for (int i = 0; i < ast.Children.Count; i++)
            {
                AST_T child_ast = ast.Children[i];
                string next_value = AS_F(child_ast);
                value += next_value;
            }
            return value;
        }

        //private string AS_F_variable(AST_T ast)
        //{
        //    string example = "";
        //}

        private string AS_F_function(AST_T ast)
        {
            string template = string.Empty;
            if (ast.Value.Type == AST.AST_FUNCTION)
            {
                template = String.Format("\n.globl {0}\n{0}:\n", ast.Name);

                template += AS_F(ast.Value);
            }

            return template;
        }
        private string AS_F_call(AST_T ast)
        {
            string template = string.Empty;
            if (!ast.Name.Equals("return"))
            { //если вызываемая функция - return
                AST_T first_arg;
                if (ast.Value.Children.Count > 0) //и у нее есть аргумент
                {
                    first_arg = ast.Value.Children[0];
                    if (first_arg.Type == AST.AST_INT) //если аргументы - число
                    {
                        template = String.Format("mov {0}, eax\nret\n", first_arg.IntValue);
                    }
                }

            }

            return template;
        }

        private string AS_F_definition_type(AST_T ast)
        {

        }
        //private string AS_F_int(AST_T ast) { }


        public string AS_F(AST_T ast)
        {
            string nextValue = string.Empty;
            string value = string.Empty;
            switch (ast.Type)
            {
                case AST.AST_COMPOUND: nextValue = AS_F_compound(ast); break;
                //case AST.AST_VARIABLE: nextValue = AS_F_variable(ast); break;
                case AST.AST_FUNCTION: nextValue = AS_F_function(ast); break;
                case AST.AST_CALL: nextValue = AS_F_call(ast); break;
                //case AST.AST_INT: nextValue = AS_F_int; break;
                case AST.AST_DEFINITION_TYPE: nextValue = AS_F_definition_type(ast); break;
                default: throw new Exception("ASM: no template for AST of type " + ast.Type);
            }

            value += nextValue;
            return value;
        }

    }
}
