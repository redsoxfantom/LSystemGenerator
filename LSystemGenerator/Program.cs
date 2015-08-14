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
            gen.AddAction('A',()=>{
                Console.WriteLine("Visited A");
            });
            gen.AddAction('B', () =>
            {
                Console.WriteLine("Visited B");
            });

            string generatedSystem = gen.GenerateSystem(3,"A");
            Console.WriteLine("Generated System: " + generatedSystem);

            gen.TraverseSystem(generatedSystem);


            Console.ReadKey();
        }
    }
}
