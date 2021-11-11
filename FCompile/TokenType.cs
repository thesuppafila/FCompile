using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public enum TokenType
    {
        ID, // variable
        INT, // number
        EQUAL, // ==
        LEFTPAREN, // (
        RIGHTPAREN, // )
        COLON, // :
        COMMA, // ,
        LESSTHEN, // <
        GREATERTHEN, // >
        LEFTBRACE, // {
        RIGHTBRACE, // }
        SEMICOLON, // ;
        IF, // if
        ELSE, // else
        RETURN, // return
        ENDOFFILE, // \0
        NOTEQUAL, //!=
        LESSTHENOREQUAL, // <=
        GREATERTHENOREQUAL, // >=
        ASSIGNMENT, //=
        INPUT, // cin
        OUTPUT, // cout
        REDIRECTED_IN, // >>
        REDIRECTED_OUT, // <<
        TYPE, // int 
        OPERATION // + - * /
    }
}
