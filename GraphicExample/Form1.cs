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
        private CurrentState mState;
        private Stack<CurrentState> executionStack;
        private Point targetLocation;

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

            mState = new CurrentState();
            mState.currentLocation = new Point(256, 256);
            mState.currentAngle = 90.0f;

            targetLocation = mState.currentLocation;

            executionStack = new Stack<CurrentState>();

            mDrawingPanel.Paint += mDrawingPanel_Paint;
        }

        private void mDrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            Pen drawingPen = new Pen(Color.Black);

            gfx.DrawLine(drawingPen, mState.currentLocation, targetLocation);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string generatedString = mGen.GenerateSystem(2, "X");
            mGen.TraverseSystem(generatedString);
        }

        private void moveForward()
        {
            double currentAngleRad = (180.0 * Math.PI) * mState.currentAngle;
            int newX = mState.currentLocation.X + (int)(10.0 * Math.Cos(currentAngleRad));
            int newY = mState.currentLocation.Y + (int)(10.0 * Math.Sin(currentAngleRad));
            targetLocation = new Point(newX,newY);

            mDrawingPanel.Refresh();

            mState.currentLocation = targetLocation;
        }

        private void turnLeft()
        {
            mState.currentAngle += 15.0f;
        }

        private void turnRight()
        {
            mState.currentAngle -= 15.0f;
        }

        private void pushToStack()
        {
            executionStack.Push(mState);
        }

        private void popFromStack()
        {
            mState = executionStack.Pop();
        }
    }

    class CurrentState
    {
        public Point currentLocation { get; set; }
        public float currentAngle { get; set; }
    }
}
