using System;
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

        private List<char> operations = new List<char>()
        {
            '+','-','*','/'
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
                case "if": return new Token(value, TokenType.IF);
                case "else": return new Token(value, TokenType.ELSE);
                case "return": return new Token(value, TokenType.RETURN);
                case "cout": return new Token(value, TokenType.OUTPUT);
                case "cin": return new Token(value, TokenType.INPUT);
            }
            if (types.Contains(value))
                return new Token(value, TokenType.TYPE);
            return new Token(value, TokenType.ID);
        }

        private Token ParseDigit()
        {
            string value = string.Empty;
            while (digits.Contains(CurrentChar))
            {
                value += CurrentChar;
                Advance();
            }
            return new Token(value, TokenType.INT);
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
                    case "!=": return new Token(value, TokenType.NOTEQUAL);
                    case "<=": return new Token(value, TokenType.LESSTHENOREQUAL);
                    case ">=": return new Token(value, TokenType.GREATERTHENOREQUAL);
                    case "==": return new Token(value, TokenType.EQUAL);
                }
            }
            else if ((value += CurrentChar) == ">>")
            {
                Advance();
                return new Token(value, TokenType.REDIRECTED_IN);
            }
            else if ((value += CurrentChar) == "<<")
            {
                Advance();
                return new Token(value, TokenType.REDIRECTED_OUT);
            }

            Cursor--;
            CurrentChar = Source[Cursor];
            switch (CurrentChar)
            {
                case '<': return AdvanceCurrent(TokenType.LESSTHEN);
                case '>': return AdvanceCurrent(TokenType.GREATERTHEN);
                case '=': return AdvanceCurrent(TokenType.ASSIGNMENT);
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
                if (IsOperation(CurrentChar))
                    return AdvanceCurrent(TokenType.OPERATION);
                switch (CurrentChar)
                {
                    case '=': return AdvanceCurrent(TokenType.EQUAL);
                    case '(': return AdvanceCurrent(TokenType.LEFTPAREN);
                    case ')': return AdvanceCurrent(TokenType.RIGHTPAREN);
                    case '{': return AdvanceCurrent(TokenType.LEFTBRACE);
                    case '}': return AdvanceCurrent(TokenType.RIGHTBRACE);
                    case ',': return AdvanceCurrent(TokenType.COMMA);
                    case ';': return AdvanceCurrent(TokenType.SEMICOLON);
                    case '\0': return AdvanceCurrent(TokenType.ENDOFFILE);
                    default: throw new Exception("Unexprected character -> " + CurrentChar);
                }
            }
            return new Token(TokenType.ENDOFFILE);
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

        private bool IsOperation(char symbol)
        {
            if (operations.Contains(symbol))
                return true;
            return false;
        }
    }
}
