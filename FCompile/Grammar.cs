using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FCompile
{
    public class Grammar
    {
        List<Rule> rules = new List<Rule>();

        HashSet<TokenType> terminal = new HashSet<TokenType>();

        HashSet<TokenType> nonterminal = new HashSet<TokenType>();

        HashSet<TokenType> all = new HashSet<TokenType>();

        Dictionary<TokenType, HashSet<TokenType>> leftU = new Dictionary<TokenType, HashSet<TokenType>>();

        Dictionary<TokenType, HashSet<TokenType>> rightU = new Dictionary<TokenType, HashSet<TokenType>>();

        Dictionary<Tuple<TokenType, TokenType>, List<int>> table = new Dictionary<Tuple<TokenType, TokenType>, List<int>>();

        public Grammar(string path)
        {
            rules = ReadFromFile(path);
            FindTerminal();
            FindNonTerminal();
            LeftU();
            RightU();
            FullSet();
            FillTable();
        }

        private List<Rule> ReadFromFile(string path)
        {

            rules = new List<Rule>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    rules.Add(ParseRule(sr.ReadLine()));
                }
            }

            return rules;
        }

        private Rule ParseRule(string line)
        {
            string[] preparse = line.Split('~');

            TokenType left = GetTokenType(preparse[0]);

            List<TokenType> right = GetTokenTypes(preparse[1]);

            return new Rule(left, right);
        }

        private TokenType GetTokenType(string value)
        {
            return (TokenType)Enum.Parse(typeof(TokenType), value);
        }

        private List<TokenType> GetTokenTypes(string line)
        {
            line = line.Substring(1, line.Length - 1);
            string[] value = line.Split(' ');

            List<TokenType> right = new List<TokenType>();

            for (int i = 0; i < value.Length; i++)
            {
                right.Add(GetTokenType(value[i]));
            }

            return right;
        }

        private void FindTerminal()
        {
            List<TokenType> left = new List<TokenType>();

            foreach (Rule rule in rules)
            {
                left.Add(rule.leftPart);
            }

            foreach (Rule rule in rules)
            {
                foreach (TokenType type in rule.rightPart)
                {
                    if (!left.Contains(type))
                        terminal.Add(type);
                }
            }
        }

        private void FindNonTerminal()
        {
            foreach (Rule rule in rules)
            {
                nonterminal.Add(rule.leftPart);
            }
        }

        private void LeftU()
        {
            foreach (TokenType type in nonterminal)
            {
                leftU.Add(type, AddLeft(type));
            }
        }


        List<Rule> usedRule = new List<Rule>();

        private HashSet<TokenType> AddLeft(TokenType type)
        {
            HashSet<TokenType> set = new HashSet<TokenType>();

            foreach (Rule rule in rules) //по каждому правилу
            {
                if (!usedRule.Contains(rule)) {
                    if (rule.leftPart == type) //если текущий есть левая часть правила
                    {
                        set.Add(rule.rightPart.First()); //добавляем левый элемент в сет
                        usedRule.Add(rule);
                        set.UnionWith(AddLeft(rule.rightPart.First())); //вызываем эту же функцию
                    }
                }
            }
            if(usedRule.Count > 0)
            usedRule.Remove(usedRule.Last());
            return set;
        }

        private void RightU()
        {
            foreach (TokenType type in nonterminal)
            {
                rightU.Add(type, AddRight(type));
            }
        }

        private HashSet<TokenType> AddRight(TokenType type)
        {
            HashSet<TokenType> set = new HashSet<TokenType>();

            foreach (Rule rule in rules) //по каждому правилу
            {
                if (!usedRule.Contains(rule))
                {
                    if (rule.leftPart == type) //если текущий есть левая часть правила
                    {
                        set.Add(rule.rightPart.Last()); //добавляем левый элемент в сет
                        usedRule.Add(rule);
                        set.UnionWith(AddLeft(rule.rightPart.Last())); //вызываем эту же функцию
                    }
                }
            }
            if (usedRule.Count > 0)
                usedRule.Remove(usedRule.Last());
            return set;
        }

        private void FillTable()
        {
            foreach(TokenType type in all)
            {
                foreach(TokenType type1 in all)
                {
                    table.Add(Tuple.Create(type, type1), new List<int>());
                }
            }

            foreach(Rule rule in rules)
            {
                if(rule.rightPart.Count > 1)
                {
                    for(int i = 0; i < rule.rightPart.Count-1; i++)
                    {
                        table[Tuple.Create(rule.rightPart[i], rule.rightPart[i + 1])].Add(1);
                    }
                }
            }

            foreach(TokenType type in all)
            {
                foreach(Rule rule in rules)
                {
                    if(rule.rightPart.Count > 1)
                    {

                    }
                }
            }
        }

        private void FullSet()
        {
            all.UnionWith(nonterminal);
            all.UnionWith(terminal);            
        }
    }
}
