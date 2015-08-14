using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSystemGenerator.LSystemGen
{
    public class Generator
    {
        public delegate void NodeVisitor();

        private Dictionary<char, string> mRules;
        private Dictionary<char, NodeVisitor> mActions;

        public Generator()
        {
            mRules = new Dictionary<char, string>();
            mActions = new Dictionary<char, NodeVisitor>();
        }
        
        public void AddRule(char ruleName, string ruleOutput)
        {
            mRules.Add(ruleName, ruleOutput);
        }

        public void AddAction(char nodeName, NodeVisitor actionToPerform)
        {
            mActions.Add(nodeName, actionToPerform);
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

        public void TraverseSystem(string generatedSystem)
        {
            foreach(char node in generatedSystem)
            {
                if(mActions.ContainsKey(node))
                {
                    mActions[node]();
                }
            }
        }
    }
}
