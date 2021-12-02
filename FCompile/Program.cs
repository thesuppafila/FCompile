using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCompile
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Пожалуйста, укажите входной файл");
            }

            string source = string.Empty;

            using (StreamReader sr = new StreamReader(args[0]))
            {
                source = sr.ReadToEnd();
                sr.Close();
            }

            string path = "SyntaxGrammar.txt";
            Grammar grammar = new Grammar(path);



            Compiler compiler = new Compiler(source);
            compiler.Compile();
            Console.ReadKey();
        }
    }
}
