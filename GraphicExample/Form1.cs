﻿using LSystemGenerator.LSystemGen;
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
            mState.currentLocation = new Point(256, 512);
            mState.currentAngle = 90.0f;

            executionStack = new Stack<CurrentState>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string generatedString = mGen.GenerateSystem(2, "X");
            mGen.TraverseSystem(generatedString);
        }

        private void moveForward()
        {
            Point targetLocation = new Point();
            
            draw(targetLocation);
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

        private void draw(Point targetLocation)
        {
            Graphics gfx = mDrawingPanel.CreateGraphics();
            Pen drawingPen = new Pen(Color.Black);

            gfx.DrawLine(drawingPen,mState.currentLocation,targetLocation);
        }
    }

    class CurrentState
    {
        public Point currentLocation { get; set; }
        public float currentAngle { get; set; }
    }
}
