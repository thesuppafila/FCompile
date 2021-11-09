using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public enum AST
    {
        AST_COMPOUND,
        AST_FUNCTION,
        AST_ASSIGNMENT,
        AST_CALL,
        AST_KEYWORD,
        AST_VARIABLE,
        AST_STATEMENT,
        AST_NOOP,
        AST_INT,
        AST_DEFINITION_TYPE
    }
}

