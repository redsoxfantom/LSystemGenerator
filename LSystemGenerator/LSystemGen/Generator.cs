using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSystemGenerator.LSystemGen
{
    public class Generator
    {
        private Dictionary<char, string> mRules;

        public Generator()
        {
            mRules = new Dictionary<char, string>();
        }
        
        public void AddRule(char ruleName, string ruleOutput)
        {
            mRules.Add(ruleName, ruleOutput);
        }

        public string GenerateSystem(int numIters, string input)
        {
            StringBuilder output = new StringBuilder(input);

            for(int i = 0; i < numIters; i++)
            {
                string iterationInput = output.ToString();
                output = new StringBuilder();

                foreach(char elem in iterationInput)
                {
                    if(mRules.ContainsKey(elem))
                    {
                        output.Append(mRules[elem]);
                    }
                    else
                    {
                        output.Append(elem);
                    }
                }
            }

            return output.ToString();
        }
    }
}
