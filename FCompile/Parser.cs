using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public class Parser
    {
        public Lexer Lexer { get; set; }
        public Token Token { get; set; }

        public Parser(Lexer lexer)
        {
            Lexer = lexer;
            Token = lexer.NextToken();
        }

        public Token Eat(int type)
        {
            if ((int)Token.Type != type)
            {
                throw new Exception("Unexprected character: " + Token.ToString() + "was expecting: " + (TokenType)type);
            }
            Token = Lexer.NextToken();
            return Token;
        }

        public AST_T Parse()
        {
            return ParseCompound();
        }

        private AST_T ParseId()
        {
            string value = Token.Value;
            Eat((int)TokenType.TOKEN_ID);

            AST_T ast;

            if (Token.Type == TokenType.TOKEN_ID) // int a
            {
                ast = new AST_T((int)AST.AST_DEFINITION_TYPE);
                ast.Name = value;
                ast.Value = ParseExpression();
                return ast;
            }

            if (Token.Type == TokenType.TOKEN_LPAREN) // func (
            {
                ast = new AST_T((int)AST.AST_CALL);
                ast.Name = value;
                ast.Value = ParseExpression();
                return ast;
            }

            if (Token.Type == TokenType.TOKEN_EQUALS)
            {
                Eat((int)TokenType.TOKEN_EQUALS);
                ast = new AST_T((int)AST.AST_ASSIGNMENT);
                ast.Name = value;
                ast.Value = ParseExpression();
                return ast;
            }

            ast = new AST_T((int)AST.AST_VARIABLE); // a
            ast.Name = value;
            return ast;

        }

        private AST_T ParseList()
        {
            Eat((int)TokenType.TOKEN_LPAREN); //съесть левую скобку

            AST_T ast = new AST_T((int)AST.AST_COMPOUND); //создать новый узел 

            ast.Children.Add(ParseExpression()); //определить что делать далее

            while (Token.Type == TokenType.TOKEN_COMMA) // если токен запятая
            {
                Eat((int)TokenType.TOKEN_COMMA); // съесть запятую
                ast.Children.Add(ParseExpression()); //определить что делать далее
            }

            Eat((int)TokenType.TOKEN_RPAREN);

            if (Token.Type == TokenType.TOKEN_LBRACE)
            {
                ast.Type = AST.AST_FUNCTION;
                ast.Value = ParseCompound();
            } 
            return ast;
        }

        private AST_T ParseBlock()
        {
            Eat((int)TokenType.TOKEN_LBRACE);

            AST_T ast = new AST_T((int)AST.AST_COMPOUND);

            ast.Children.Add(ParseExpression());

            while (Token.Type == TokenType.TOKEN_COMMA)
            {
                ast.Children.Add(ParseExpression());
            }

            Eat((int)TokenType.TOKEN_RBRACE);

            return ast;
        }

        private AST_T ParseInt()
        {
            int value = int.Parse(Token.Value);
            Eat((int)TokenType.TOKEN_INT);

            AST_T ast = new AST_T((int)AST.AST_INT);
            ast.Type = AST.AST_INT;
            ast.Name = "NUMBER";
            ast.IntValue = value;
            return ast;
        }

        public AST_T ParseExpression()
        {
            switch (Token.Type)
            {
                case TokenType.TOKEN_ID: return ParseId();
                case TokenType.TOKEN_LPAREN: return ParseList();
                case TokenType.TOKEN_LBRACE: return ParseBlock();
                case TokenType.TOKEN_INT: return ParseInt();
                default: throw new Exception("Unexprected character: " + Token.ToString() + "was expecting: " + Token.Type);
            }
        }


        public AST_T ParseCompound()
        {
            bool shouldClose = false;
            if (Token.Type == TokenType.TOKEN_LBRACE)
            {
                Eat((int)TokenType.TOKEN_LBRACE);
                shouldClose = true;
            }

            AST_T compound = new AST_T((int)AST.AST_COMPOUND);

            while (Token.Type != TokenType.TOKEN_EOF && Token.Type != TokenType.TOKEN_RBRACE)
            {
                compound.Children.Add(ParseExpression());

                if (Token.Type == TokenType.TOKEN_SEMI)
                    Eat((int)TokenType.TOKEN_SEMI);
            }

            if (shouldClose)
                Eat((int)TokenType.TOKEN_RBRACE);

            return compound;
        }


        private int TypeNameToInt(string typeName)
        {
            int t = 0;
            for (int i = 0; i < typeName.Length; i++)
                t += typeName[i];
            return t;
        }
    }
}
