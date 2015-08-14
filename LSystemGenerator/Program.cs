using LSystemGenerator.LSystemGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSystemGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator gen = new Generator();
            gen.AddRule('A',"AB");
            gen.AddRule('B', "BA");

            Console.WriteLine(gen.GenerateSystem(10,"A"));
            Console.ReadKey();
        }
    }
}
