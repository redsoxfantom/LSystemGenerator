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
        private double stepLength = 10.0;
        private float turnAngle = 90.0f;
        Bitmap drawingBitmap;

        public Form1()
        {
            InitializeComponent();

            mGen = new Generator();
            mGen.AddRule('F', "FFF+F+FFF+F+F+FF-F-FFF-F-F");
            mGen.AddAction('F', moveForward);
            mGen.AddAction('+', turnLeft);
            mGen.AddAction('-', turnRight);
            mGen.AddAction('[', pushToStack);
            mGen.AddAction(']', popFromStack);
            mGen.TraversalComplete += (() => { mDrawingPanel.Refresh(); });

            mState = new CurrentState();
            mState.currentLocation = new Point(256, 256);
            mState.currentAngle = 90.0f;

            targetLocation = mState.currentLocation;

            executionStack = new Stack<CurrentState>();

            drawingBitmap = new Bitmap(mDrawingPanel.Width, mDrawingPanel.Height);

            mDrawingPanel.Paint += mDrawingPanel_Paint;
        }

        private void mDrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(drawingBitmap, new Point(0, 0));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numIters = Int32.Parse(textBox1.Text);
            string generatedString = mGen.GenerateSystem(numIters, "F");
            mGen.TraverseSystem(generatedString);
        }

        private void moveForward()
        {
            double currentAngleRad = (Math.PI / 180.0) * mState.currentAngle;
            int newX = mState.currentLocation.X - (int)(stepLength * Math.Cos(currentAngleRad));
            int newY = mState.currentLocation.Y - (int)(stepLength * Math.Sin(currentAngleRad));
            targetLocation = new Point(newX,newY);
            regenerateBitmap();
            mState.currentLocation = targetLocation;
        }

        private void turnLeft()
        {
            mState.currentAngle -= turnAngle;
        }

        private void turnRight()
        {
            mState.currentAngle += turnAngle;
        }

        private void pushToStack()
        {
            CurrentState stateToPush = new CurrentState();
            stateToPush.currentAngle = mState.currentAngle;
            stateToPush.currentLocation = new Point(mState.currentLocation.X, mState.currentLocation.Y);
            executionStack.Push(stateToPush);
        }

        private void popFromStack()
        {
            mState = executionStack.Pop();
        }

        private void regenerateBitmap()
        {
            using (Graphics gfx = Graphics.FromImage(drawingBitmap))
            {
                Pen drawingPen = new Pen(Color.Black);
                gfx.DrawLine(drawingPen, mState.currentLocation, targetLocation);
            }
        }
    }

    class CurrentState
    {
        public Point currentLocation { get; set; }
        public float currentAngle { get; set; }
    }
}
