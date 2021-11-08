using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{

    public class Compiler
    {
        Lexer lexer;

        Parser parser;

        ASM asm;

        List<Token> tokens = new List<Token>();

        Token token;

        public Compiler(string source)
        {
            lexer = new Lexer(source);
            parser = new Parser(lexer);
            AST_T root = parser.Parse();
            asm = new ASM();
            string s = asm.AS_F(root);
            Console.WriteLine(s);
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
