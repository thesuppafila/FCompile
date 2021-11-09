using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public enum TokenType
    {
        TOKEN_ID, // variable
        TOKEN_INT, // number
        TOKEN_EQUAL, // ==
        TOKEN_LPAREN, // (
        TOKEN_RPAREN, // )
        TOKEN_COLON, // :
        TOKEN_COMMA, // ,
        TOKEN_LT, // <
        TOKEN_GT, // >
        TOKEN_LBRACE, // {
        TOKEN_RBRACE, // }
        TOKEN_SEMI, // ;
        TOKEN_IF, // if
        TOKEN_ELSE, // else
        TOKEN_RETURN, // return
        TOKEN_EOF, // \0
        TOKEN_NOTEQUAL, //!=
        TOKEN_LTE, // <=
        TOKEN_GTE, // >=
        TOKEN_ASSIGN, //=
        TOKEN_INPUT, // cin
        TOKEN_OUTPUT, // cout
        TOKEN_REDIRECTED_IN, // >>
        TOKEN_REDIRECTED_OUT, // <<
        TOKEN_TYPE
    }
}
