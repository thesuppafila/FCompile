using System;
using System.Collections.Generic;
using FCompile.Node;

namespace FCompile
{

    public class Compiler
    {
        Lexer lexer;

        List<Token> tokens = new List<Token>();

        Token token;

        public Compiler(string source)
        {
            lexer = new Lexer(source);

            List<Token> tokens = new List<Token>();
            Token token;

            while ((token = lexer.NextToken()).Type != TokenType.TOKEN_EOF)
            {
                Console.WriteLine(token);
            }

            lexer = new Lexer(source);
            Parser parser = new Parser(lexer);
            PROG prog = parser.Parse();
        }

        public void Compile()
        {

            while ((token = lexer.NextToken()).Type != TokenType.TOKEN_EOF)
            {
                tokens.Add(token);
                Console.WriteLine(token.ToString());
            }
        }
    }
}
