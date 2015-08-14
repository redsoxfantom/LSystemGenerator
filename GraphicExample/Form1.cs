using LSystemGenerator.LSystemGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicExample
{
    public partial class Form1 : Form
    {
        private Generator mGen;

        public Form1()
        {
            InitializeComponent();

            mGen = new Generator();
            mGen.AddRule('X', "F-[[X]+X]+F[+FX]-X");
            mGen.AddRule('F', "FF");

            mGen.AddAction('F', moveForward);
            mGen.AddAction('+', turnLeft);
            mGen.AddAction('-', turnRight);
            mGen.AddAction('[', pushToStack);
            mGen.AddAction(']', popFromStack);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void moveForward()
        {

        }

        private void turnLeft()
        {

        }

        private void turnRight()
        {

        }

        private void pushToStack()
        {

        }

        private void popFromStack()
        {

        }
    }
}
