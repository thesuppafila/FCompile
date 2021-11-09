﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    public class Lexer
    {
        string Source { get; set; }

        char CurrentChar { get; set; }

        int Cursor { get; set; }

        public List<Token> tokens = new List<Token>();

        private List<char> digits = new List<char>()
        {
            '0', '1','2','3','4','5','6','7','8','9'
        };

        private List<char> opers = new List<char>()
        {
            '+','-','*','/'
        };

        private List<char> sg = new List<char>()
        {
            '<', '>', '=', '!'
        };

        private List<char> symbols = new List<char>()
        {
            'A','a','B','b','C','c','D','d','E','e','F','f','G','g',
            'H','h','I','i','J','j','K','k','L','l','M','m','N','n',
            'O','o','P','p','Q','q','R','r','S','s','T','t','U','u',
            'V','v','W','w','X','x','Y','y','Z','z'
        };

        private List<string> keywords = new List<string>()
        {
            "if",
            "else",
            "return"
        };

        private List<string> types = new List<string>()
        {
            "int"
        };


        public Lexer(string source)
        {
            Source = source += '\0';
            Cursor = 0;
            CurrentChar = Source[Cursor];
        }

        public void Advance()
        {
            if (Cursor < Source.Length && CurrentChar != '\0')
            {
                this.Cursor++;
                CurrentChar = Source[Cursor];
            }
        }

        private Token AdvanceWith(Token token)
        {
            Advance();
            return token;
        }

        private Token AdvanceCurrent(TokenType type)
        {
            Token currentToken = new Token(CurrentChar.ToString(), type);
            Advance();
            return currentToken;
        }

        public void SkipWhiteSpace()
        {
            while (CurrentChar == ' ' || CurrentChar == '\t' || CurrentChar == '\n' || CurrentChar == '\r')
                Advance();
        }


        private Token ParseId()
        {
            string value = string.Empty;
            while (symbols.Contains(CurrentChar))
            {
                value += CurrentChar;
                Advance();
            }

            switch (value)
            {
                case "if": return new Token(value, TokenType.TOKEN_IF);
                case "else": return new Token(value, TokenType.TOKEN_ELSE);
                case "return": return new Token(value, TokenType.TOKEN_RETURN);
                case "cout": return new Token(value, TokenType.TOKEN_OUTPUT);
                case "cin": return new Token(value, TokenType.TOKEN_INPUT);
            }
            if (types.Contains(value))
                return new Token(value, TokenType.TOKEN_TYPE);
            return new Token(value, TokenType.TOKEN_ID);
        }

        private Token ParseDigit()
        {
            string value = string.Empty;
            while (digits.Contains(CurrentChar))
            {
                value += CurrentChar;
                Advance();
            }
            return new Token(value, TokenType.TOKEN_INT);
        }

        private Token ParseSign()
        {
            string value = CurrentChar.ToString();
            Advance();
            if (CurrentChar == '=')
            {
                value += CurrentChar;
                Advance();
                switch (value)
                {
                    case "!=": return new Token(value, TokenType.TOKEN_NOTEQUAL);
                    case "<=": return new Token(value, TokenType.TOKEN_LTE);
                    case ">=": return new Token(value, TokenType.TOKEN_GTE);
                    case "==": return new Token(value, TokenType.TOKEN_EQUAL);
                }
            }
            else if ((value += CurrentChar) == ">>")
            {
                Advance();
                return new Token(value, TokenType.TOKEN_REDIRECTED_IN);
            }
            else if ((value += CurrentChar) == "<<")
            {
                Advance();
                return new Token(value, TokenType.TOKEN_REDIRECTED_OUT);
            }

            Cursor--;
            CurrentChar = Source[Cursor];
            switch (CurrentChar)
            {
                case '<': return AdvanceCurrent(TokenType.TOKEN_LT);
                case '>': return AdvanceCurrent(TokenType.TOKEN_GT);
                case '=':return AdvanceCurrent(TokenType.TOKEN_ASSIGN);
                default: throw new Exception("Unexprected character -> " + CurrentChar);
            }
        }

        public Token NextToken()
        {
            while (CurrentChar != '\0')
            {
                SkipWhiteSpace();
                if (IsSymbol(CurrentChar))
                    return ParseId();
                if (IsDigit(CurrentChar))
                    return ParseDigit();
                if (IsSign(CurrentChar))
                    return ParseSign();
                switch (CurrentChar)
                {
                    case '=': return AdvanceCurrent(TokenType.TOKEN_EQUAL);
                    case '(': return AdvanceCurrent(TokenType.TOKEN_LPAREN);
                    case ')': return AdvanceCurrent(TokenType.TOKEN_RPAREN);
                    case '{': return AdvanceCurrent(TokenType.TOKEN_LBRACE);
                    case '}': return AdvanceCurrent(TokenType.TOKEN_RBRACE);
                    case ',': return AdvanceCurrent(TokenType.TOKEN_COMMA);
                    case ';': return AdvanceCurrent(TokenType.TOKEN_SEMI);
                    case '\0': return AdvanceCurrent(TokenType.TOKEN_EOF);
                    default: throw new Exception("Unexprected character -> " + CurrentChar);
                }
            }
            return new Token(TokenType.TOKEN_EOF);
        }

        private bool IsSymbol(char symbol)
        {
            if (symbols.Contains(symbol))
                return true;
            return false;
        }

        private bool IsDigit(char symbol)
        {
            if (digits.Contains(symbol))
                return true;
            return false;
        }

        private bool IsSign(char symbol)
        {
            if (sg.Contains(symbol))
                return true;
            return false;
        }
    }
}
