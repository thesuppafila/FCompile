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

            while ((token = lexer.NextToken()).Type != TokenType.ENDOFFILE)
            {
                Console.WriteLine(token);
            }

            lexer = new Lexer(source);
            Parser parser = new Parser(lexer);
            ProgramNode prog = parser.Parse();
            Console.WriteLine(prog.ToString(Indent.TAB));

            Console.WriteLine(prog.ToAssString());
        }

        public void Compile()
        {

            while ((token = lexer.NextToken()).Type != TokenType.ENDOFFILE)
            {
                tokens.Add(token);
                Console.WriteLine(token.ToString());
            }
        }
    }
}
