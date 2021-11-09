using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCompile.Node;


namespace FCompile
{
    public class Parser
    {
        private Lexer lexer;

        private Token token;

        private List<Token> usedTokens;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
            token = lexer.NextToken();
            usedTokens = new List<Token>();
        }


        private Token Eat(TokenType type)
        {
            usedTokens.Add(token);
            if (token.Type != type)
            {
                throw new Exception("Неожиданный токен: " + token.ToString() + " ожидался: " + type);
            }
            token = lexer.NextToken();
            return token;
        }

        public PROG Parse()
        {
            return ParseProgram();
        }


        private List<DEC> ParseDecList()
        {
            List<DEC> decList = new List<DEC>();
            Eat(TokenType.TOKEN_LPAREN);
            while (token.Type != TokenType.TOKEN_RPAREN)
            {
                if (token.Type == TokenType.TOKEN_COMMA)
                    Eat(TokenType.TOKEN_COMMA);
                decList.Add(ParseDec());
            }
            Eat(TokenType.TOKEN_RPAREN);

            return decList;
        }

        //private ID ParseId()
        //{
        //    string value = token.Value;

        //    Eat(TokenType.TOKEN_ID);
        //    ID id = new ID();
        //    id.name = value;

        //    if (token.Type == TokenType.TOKEN_ASSIGN)
        //    {
        //        ParseOper();
        //    }

        //}

        private DEC ParseDec()
        {
            DEC dec = new DEC();

            string value = token.Value;
            Eat(TokenType.TOKEN_TYPE);
            dec.TYPE = new TYPE(value);

            value = token.Value;
            Eat(TokenType.TOKEN_ID);
            dec.ID = new ID(value);

            return dec;
        }

        private EXPRES ParseExpres()
        {
            string value;
            if (token.Type == TokenType.TOKEN_INT)
            {
                value = token.Value;
                Eat(TokenType.TOKEN_INT);
                return new EXPRES(value);
            }

            value = token.Value;
            Eat(TokenType.TOKEN_ID);
            return new EXPRES(value);
        }

        private SG ParseSign()
        {
            string value = token.Value;
            SG sg;
            switch (token.Type)
            {
                case TokenType.TOKEN_EQUAL:
                    sg = new SG(SG_TYPE.EQ);
                    Eat(TokenType.TOKEN_EQUAL);
                    break;
                case TokenType.TOKEN_NOTEQUAL:
                    sg = new SG(SG_TYPE.NOTEQ);
                    Eat(TokenType.TOKEN_NOTEQUAL);
                    break;
                case TokenType.TOKEN_LT:
                    sg = new SG(SG_TYPE.LT);
                    Eat(TokenType.TOKEN_LT);
                    break;
                case TokenType.TOKEN_LTE:
                    sg = new SG(SG_TYPE.LTE);
                    Eat(TokenType.TOKEN_LTE);
                    break;
                case TokenType.TOKEN_GT:
                    sg = new SG(SG_TYPE.GT);
                    Eat(TokenType.TOKEN_GT);
                    break;
                case TokenType.TOKEN_GTE:
                    sg = new SG(SG_TYPE.GTE);
                    Eat(TokenType.TOKEN_GTE);
                    break;
                default: throw new Exception("Неожиданный символ: " + token.ToString() + ", ожидался: " + token.Type);
            }
            return sg;
        }

        private RL ParseRL()
        {
            RL rule = new RL();

            rule.expres1 = ParseExpres();
            rule.SG = ParseSign();
            rule.expres2 = ParseExpres();

            return rule;
        }

        private COND ParseCond()
        {
            COND condition = new COND();
            Eat(TokenType.TOKEN_IF); // if

            Eat(TokenType.TOKEN_LPAREN); // ( ... )
            condition.RL = ParseRL();
            Eat(TokenType.TOKEN_RPAREN);

            condition.operList = ParseOperList();// { ... }

            return condition;
        }

        private ASSING ParseAssign()
        {
            string value = token.Value;
            ASSING assign = new ASSING();

            assign.ID = new ID(value);
            Eat(TokenType.TOKEN_ID); // a = 
            Eat(TokenType.TOKEN_ASSIGN);

            assign.expres = ParseExpres();

            return assign;
        }

        private INPUT ParseInput()
        {
            Eat(TokenType.TOKEN_INPUT);
            Eat(TokenType.TOKEN_REDIRECTED_IN);

            INPUT input = new INPUT();

            input.ID = new ID(token.Value);
            Eat(TokenType.TOKEN_ID);
            return input;
        }

        private OUTPUT ParseOutput()
        {
            Eat(TokenType.TOKEN_OUTPUT);
            Eat(TokenType.TOKEN_REDIRECTED_OUT);

            OUTPUT output = new OUTPUT();
            if (token.Type == TokenType.TOKEN_ID)
            {
                output.value = token.Value;
                Eat(TokenType.TOKEN_ID);
                return output;
            }

            output.value = token.Value;
            Eat(TokenType.TOKEN_INT);
            return output;
        }
        private RETURN ParseReturn()
        {
            Eat(TokenType.TOKEN_RETURN);
            RETURN ret = new RETURN();
            ret.value = token.Value;
            Eat(TokenType.TOKEN_ID);
            return ret;
        }

        private IOPER ParseOper()
        {
            IOPER operation;

            switch (token.Type)
            {
                case TokenType.TOKEN_TYPE:
                    operation = ParseDec();
                    return operation;
                case TokenType.TOKEN_IF:
                    operation = ParseCond();
                    return operation;
                case TokenType.TOKEN_ID:
                    operation = ParseAssign();
                    return operation;
                case TokenType.TOKEN_INPUT:
                    operation = ParseInput();
                    return operation;
                case TokenType.TOKEN_OUTPUT:
                    operation = ParseOutput();
                    return operation;
                case TokenType.TOKEN_RETURN:
                    operation = ParseReturn();
                    return operation;
                default: throw new Exception("Неожиданный токен: " + token + " ожидался токен операции.");
            }
        }

        private List<IOPER> ParseOperList()
        {
            List<IOPER> operList = new List<IOPER>();
            if (token.Type == TokenType.TOKEN_LBRACE)
            {
                Eat(TokenType.TOKEN_LBRACE);
                while (token.Type != TokenType.TOKEN_RBRACE)
                {
                    operList.Add(ParseOper());
                    Eat(TokenType.TOKEN_SEMI);
                }
                Eat(TokenType.TOKEN_RBRACE);
            }
            else ParseOper();

            return operList;
        }

        private FUNC ParseFunc()
        {
            FUNC func = new FUNC();
            func.type = token.Value;
            Eat(TokenType.TOKEN_TYPE);

            func.name = token.Value;
            Eat(TokenType.TOKEN_ID);

            func.DECLIST = ParseDecList();

            if (token.Type == TokenType.TOKEN_LBRACE)
            {
                while (token.Type != TokenType.TOKEN_SEMI)
                {
                    func.OPERLIST = ParseOperList();
                }
            }
            return func;
        }

        private PROG ParseProgram()
        {
            PROG program = new PROG();
            while (token.Type != TokenType.TOKEN_EOF)
            {
                program.FUNCLIST.Add(ParseFunc());
                Eat(TokenType.TOKEN_SEMI);
            }
            return program;
        }
    }
}

