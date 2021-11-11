using System;
using System.Collections.Generic;
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

        public ProgramNode Parse()
        {
            return ParseProgram();
        }

        private List<DeclarationNode> ParseDecList() // eat: ( , )
        {
            List<DeclarationNode> decList = new List<DeclarationNode>();
            Eat(TokenType.LEFTPAREN);
            while (token.Type != TokenType.RIGHTPAREN)
            {
                if (token.Type == TokenType.COMMA)
                    Eat(TokenType.COMMA);
                decList.Add(ParseDeclaration());
            }
            Eat(TokenType.RIGHTPAREN);

            return decList;
        }

        private IOperation ParseIdentifier() // eat: id =
        {
            IdentifierNode identifier = new IdentifierNode(token.Value);
            Eat(TokenType.ID);
            
            if(token.Type == TokenType.OPERATION) //можно добавить классы для каждой операции
            {
                OperationSign operation = new OperationSign(token.Value);
                Eat(TokenType.OPERATION);
                ExpressionNode = new ExpressionNode(identifier,)
            }
            Eat(TokenType.ASSIGNMENT);
            AssignmentNode assignment = new AssignmentNode();
            assignment.identifier = new IdentifierNode(identifierName);
            assignment.expression = ParseExpression();
            return assignment;
        }

        private DeclarationNode ParseDeclaration() // eat: type id
        {
            DeclarationNode dec = new DeclarationNode();

            string value = token.Value;
            Eat(TokenType.TYPE);
            dec.type = new TypeNode(value);

            value = token.Value;
            Eat(TokenType.ID);
            dec.identifier = new IdentifierNode(value);

            return dec;
        }

        private TermNode ParseTerm()
        {
            string value = token.Value;
            if (token.Type == TokenType.ID)
        }

        private ExpressionNode ParseExpression() // eat: integer | id
        {
            if (token.Type == TokenType.INT)
            {
                IntegerNode integer = new IntegerNode(token.Value);
                Eat(TokenType.INT);
                return new ExpressionNode(ParseTerm());
                //return new ExpressionNode(ParseTerm(new FactorNode(integer)));
            }
            
            value = token.Value;
            Eat(TokenType.ID);
            return new ExpressionNode(value);
        }

        private SignNode ParseSign() // eat: sign
        {
            string value = token.Value;
            SignNode sg;
            switch (token.Type)
            {
                case TokenType.EQUAL:
                    sg = new SignNode(SG_TYPE.EQ);
                    Eat(TokenType.EQUAL);
                    break;
                case TokenType.NOTEQUAL:
                    sg = new SignNode(SG_TYPE.NOTEQ);
                    Eat(TokenType.NOTEQUAL);
                    break;
                case TokenType.LESSTHEN:
                    sg = new SignNode(SG_TYPE.LT);
                    Eat(TokenType.LESSTHEN);
                    break;
                case TokenType.LESSTHENOREQUAL:
                    sg = new SignNode(SG_TYPE.LTE);
                    Eat(TokenType.LESSTHENOREQUAL);
                    break;
                case TokenType.GREATERTHEN:
                    sg = new SignNode(SG_TYPE.GT);
                    Eat(TokenType.GREATERTHEN);
                    break;
                case TokenType.GREATERTHENOREQUAL:
                    sg = new SignNode(SG_TYPE.GTE);
                    Eat(TokenType.GREATERTHENOREQUAL);
                    break;
                default: throw new Exception("Неожиданный символ: " + token.ToString() + ", ожидался: " + token.Type);
            }
            return sg;
        }

        private RuleNode ParseRule()
        {
            RuleNode rule = new RuleNode();

            rule.expression1 = ParseExpression();
            rule.sign = ParseSign();
            rule.expression2 = ParseExpression();

            return rule;
        }

        private ConditionNode ParseCondition() // eat: if ( rule ) { }
        {
            ConditionNode condition = new ConditionNode();
            Eat(TokenType.IF); // if

            Eat(TokenType.LEFTPAREN); // ( ... )
            condition.rule = ParseRule();
            Eat(TokenType.RIGHTPAREN);

            condition.operationList = ParseOperList();// { ... }

            return condition;
        }

        private AssignmentNode ParseAssignment() // eat: id = expression
        {
            string value = token.Value;
            AssignmentNode assign = new AssignmentNode();

            assign.identifier = new IdentifierNode(value);
            Eat(TokenType.ID); // a = 
            Eat(TokenType.ASSIGNMENT);

            assign.expression = ParseExpression();

            return assign;
        }

        private InputNode ParseInput() // eat: cin >> id
        {
            Eat(TokenType.INPUT);
            Eat(TokenType.REDIRECTED_IN);

            InputNode input = new InputNode();

            input.identifier = new IdentifierNode(token.Value);
            Eat(TokenType.ID);
            return input;
        }

        private OutputNode ParseOutput() // eat: cout << id | integer
        {
            Eat(TokenType.OUTPUT);
            Eat(TokenType.REDIRECTED_OUT);

            OutputNode output = new OutputNode();
            if (token.Type == TokenType.ID)
            {
                output.value = token.Value;
                Eat(TokenType.ID);
                return output;
            }

            output.value = token.Value;
            Eat(TokenType.INT);
            return output;
        }

        private ReturnNode ParseReturn() // eat: return id
        {
            Eat(TokenType.RETURN);
            ReturnNode ret = new ReturnNode();
            ret.value = token.Value;
            Eat(TokenType.ID);
            return ret;
        }

        private IOperation ParseOperation()
        {
            IOperation operation;

            switch (token.Type)
            {
                case TokenType.TYPE:
                    operation = ParseDeclaration();
                    return operation;
                case TokenType.IF:
                    operation = ParseCondition();
                    return operation;
                case TokenType.ID:
                    operation = ParseIdentifier();
                    return operation;
                case TokenType.INPUT:
                    operation = ParseInput();
                    return operation;
                case TokenType.OUTPUT:
                    operation = ParseOutput();
                    return operation;
                case TokenType.RETURN:
                    operation = ParseReturn();
                    return operation;
                default: throw new Exception("Неожиданный токен: " + token + " ожидался токен операции.");
            }
        }

        private List<IOperation> ParseOperList() // eat: { operation ; operation ; }
        {
            List<IOperation> operList = new List<IOperation>();
            if (token.Type == TokenType.LEFTBRACE)
            {
                Eat(TokenType.LEFTBRACE);
                while (token.Type != TokenType.RIGHTBRACE)
                {
                    operList.Add(ParseOperation());
                    Eat(TokenType.SEMICOLON);
                }
                Eat(TokenType.RIGHTBRACE);
            }
            else ParseOperation();

            return operList;
        }

        private FunctionNode ParseFunction()
        {
            FunctionNode func = new FunctionNode();
            func.type = token.Value;
            Eat(TokenType.TYPE);

            func.name = token.Value;
            Eat(TokenType.ID);

            func.declarationList = ParseDecList();

            if (token.Type == TokenType.LEFTBRACE)
            {
                while (token.Type != TokenType.SEMICOLON)
                {
                    func.operationList = ParseOperList();
                }
            }
            return func;
        }

        private ProgramNode ParseProgram()
        {
            ProgramNode program = new ProgramNode();
            while (token.Type != TokenType.ENDOFFILE)
            {
                program.functionList.Add(ParseFunction());
                Eat(TokenType.SEMICOLON);
            }
            return program;
        }
    }
}

