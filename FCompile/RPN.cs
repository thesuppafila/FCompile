using FCompile.Node;
using System;
using System.Collections.Generic;

namespace FCompile
{
    [Serializable]
    public class RPN
    {

        public RPN()
        {

        }

        static public IExp Build(List<IExp> input)
        {
            List<IExp> output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
            IExp root = GetTree(output);
            return root; //Возвращаем результат
        }

        static public List<IExp> GetExpression(List<IExp> input)
        {
            List<IExp> output = new List<IExp>(); //Строка для хранения выражения
            Stack<IExp> operStack = new Stack<IExp>(); //Стек для хранения операторов

            for (int i = 0; i < input.Count; i++) //Для каждого символа в входной строке
            {
                //Если символ - цифра, то считывем все число
                if (!IsOperator(input[i].GetValue()))
                {
                    output.Add(input[i]);//Дописываем после числа пробел в строку с выражением
                }

                //Если символ - оператор
                if (IsOperator(input[i].ToString())) //Если оператор
                {
                    if (input[i].GetValue() == "(") //Если символ - открывающая скобка
                        operStack.Push(input[i]); //Записываем её в стек
                    else if (input[i].GetValue() == ")") //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в лист
                        IExp s = operStack.Pop();

                        while (s.GetValue() != "(")
                        {
                            output.Add(s);
                            s = operStack.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоретет нашего оператора меньше или равен приоретету оператора на вершине стека
                                output.Add(operStack.Pop()); //То добавляем последний оператор из стека в строку с выражением

                        operStack.Push(input[i]); //Если стек пуст, или же приоретет оператора выше - добавляем оператов на вершину стека

                    }
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output.Add(operStack.Pop());

            return output; //Возвращаем выражение в постфиксной записи
        }

        //Метод возвращает приоритет оператора
        static private byte GetPriority(IExp node)
        {
            switch (node.GetValue())
            {
                case "*": return 3;
                case "/": return 3;
                case "+": return 2;
                case "-": return 2;
                case "(": return 1;
                case ")": return 1;
                default: return 0;
            }
        }

        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(string с)
        {
            var operators = new string[] { "(", ")", "+", "-", "*", "/" };
            if (Array.IndexOf(operators, с) != -1)
                return true;
            return false;
        }

        //Метод возвращает строит дерево выражения и возвращает корень
        static IExp GetTree(List<IExp> express)
        {
            var temp = new Stack<IExp>();

            for (int i = 0; i < express.Count; i++)
            {
                if (!IsOperator(express[i].GetValue()))
                    temp.Push(express[i]);

                else if (IsOperator(express[i].GetValue()))
                {
                    var a = temp.Pop();
                    var b = temp.Pop();
                    var node = new OperationSign(express[i].GetValue());
                    node.left = a;
                    node.right = b;
                    temp.Push(node);
                }
            }

            return temp.Peek();
        }
    }
}
