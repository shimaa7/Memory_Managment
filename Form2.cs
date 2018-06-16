using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Managment
{
    public partial class Form2 : Form
    {
     float factor = (float)950 / Variables.limit;
        static Bitmap drawingSurface = new Bitmap(1000, 1000);
        Graphics GFX = Graphics.FromImage(drawingSurface);
        Pen penMem = new Pen(Color.Black, 10);
        Pen pen = new Pen(Color.Red, 7);
        Pen penProcess = new Pen(Color.Blue, 7);
        Font myFont = new Font("Arial", 15);
        int num,btn = 0;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if(Variables.ProcessCounter > 0)
            {
                label5.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        public void drawing()
        {
            //pictureBox1.Image = null;
            for (int i = 0; i < Variables.ProcessCounter; i++)
            {
                num = Variables.processes[i].holeNumber;
                if (num > -1)
                {
                    // Variables.holes[num].size = Variables.holes[num].size - Variables.processes[i].size;
                    GFX.DrawRectangle(penProcess, 10, (Variables.processes[i].startingAddress - Variables.srart_address) * factor, 490, Variables.processes[i].size * factor); //xywh
                    GFX.DrawString((Variables.processes[i].startingAddress).ToString(), myFont, Brushes.Red, 515, (Variables.processes[i].startingAddress - Variables.srart_address) * factor);
                    GFX.DrawString(Variables.processes[i].processName, myFont, Brushes.Red, 200, (Variables.processes[i].startingAddress - Variables.srart_address + Variables.processes[i].size / 2) * factor);
                    GFX.DrawString((Variables.processes[i].startingAddress + Variables.processes[i].size - 1).ToString(), myFont, Brushes.Red, 540, (Variables.processes[i].startingAddress + Variables.processes[i].size - 1 - Variables.srart_address) * factor);
                }
                else
                {
                    GFX.DrawString(Variables.processes[i].processName + " Not Allocated", myFont, Brushes.Red, 550, 200);
                }
            }
            // pictureBox1.Image = drawingSurface;
            for (int i = 0; i < Variables.holeCounter; i++)
            {
                if (Variables.holes[i].size != 0) {
                    GFX.DrawRectangle(pen, 10, (Variables.holes[i].startingAddress - Variables.srart_address) * factor, 490, Variables.holes[i].size * factor); //xywh
                    GFX.DrawString((Variables.holes[i].startingAddress).ToString(), myFont, Brushes.Red, 515, (Variables.holes[i].startingAddress - Variables.srart_address) * factor);
                    GFX.DrawString("HOLE " + i.ToString(), myFont, Brushes.Red, 200, (Variables.holes[i].startingAddress - Variables.srart_address + Variables.holes[i].size / 2) * factor);
            GFX.DrawString((Variables.holes[i].startingAddress + Variables.holes[i].size - 1).ToString(), myFont, Brushes.Red, 540, (Variables.holes[i].startingAddress + Variables.holes[i].size - 1 - Variables.srart_address) * factor);
        } }
            pictureBox1.Image = drawingSurface;
        }

        public void reArrange()
        {
            int[] pro = new int[50];
            Variables.tempHole = Variables.holeCounter;
            for (int i = 0; i < Variables.holeCounter; i++)
            {
                Variables.reArrangeCounter = 0;
                for (int k = 0; k < Variables.ProcessCounter; k++)
                {if (i == Variables.processes[k].holeNumber){ pro[Variables.reArrangeCounter] = k; Variables.reArrangeCounter++; }}
                if(Variables.reArrangeCounter > 1)
                {
                    for(int t = 0; t < Variables.reArrangeCounter - 1; t++) {
                        Variables.holes[Variables.tempHole + t].size = 0;
                        Variables.holes[Variables.tempHole + t].startingAddress = Variables.processes[pro[t]].startingAddress + Variables.processes[pro[t]].size;
                        Variables.processes[pro[t]].holeNumber = Variables.tempHole + t;
                        Variables.tempHole++;
                    }
                }
            }
            Variables.holeCounter = Variables.tempHole;
        }
        public void swap(int del)
        {
            /*int flag = 0;
            for(int i = 0; i < Variables.ProcessCounter; i++)
            {
                if ( flag ==0 &&Variables.processes[i].holeNumber >= d && i != del && Variables.processes[del].size >= Variables.processes[i].size)
                {
                    flag = 1;
                    int h = Variables.processes[i].holeNumber;
                    Variables.holes[h].startingAddress = Variables.processes[i].startingAddress;
                    Variables.holes[h].size = Variables.holes[h].size + Variables.processes[i].size;
                 //   Variables.processes[i].holeNumber = d;
                 //   Variables.processes[i].startingAddress = Variables.processes[del].startingAddress;
                    Variables.holes[d].startingAddress = Variables.processes[del].startingAddress;
                    Variables.holes[d].size = Variables.holes[d].size + Variables.processes[d].size;
                    Variables.processes[i] = Variables.firstFit(Variables.holes, Variables.holeCounter, Variables.processes[i], 1);
                }
            }*/
            for (int i = 0; i < Variables.ProcessCounter && i!=del; i++)
            {
                int h = Variables.processes[i].holeNumber;
                Variables.holes[h].startingAddress = Variables.processes[i].startingAddress;
                Variables.holes[h].size = Variables.holes[h].size + Variables.processes[i].size;
                Variables.processes[i].holeNumber = -2;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            drawingSurface = null;
            pictureBox1.Image = null;
            drawingSurface = new Bitmap(1000, 1000);
            GFX = Graphics.FromImage(drawingSurface);
            GFX.DrawRectangle(penMem,0,0,510,950); //xywh
            GFX.DrawString(Variables.srart_address.ToString(),myFont, Brushes.Black,515,0);
            GFX.DrawString((Variables.srart_address+Variables.limit-1).ToString(), myFont, Brushes.Black, 515, 950);
            pictureBox1.Top = 0;
            pictureBox1.Image = drawingSurface;
            if (Variables.ProcessCounter == 0)
            {
                for (int i = 0; i < Variables.holeCounter; i++)
                {
                    
                    GFX.DrawRectangle(pen, 10, (Variables.holes[i].startingAddress - Variables.srart_address) * factor, 490, Variables.holes[i].size * factor); //xywh
                    GFX.DrawString((Variables.holes[i].startingAddress).ToString(), myFont, Brushes.Red, 515, (Variables.holes[i].startingAddress - Variables.srart_address) * factor);
                    GFX.DrawString("HOle " + i.ToString(), myFont, Brushes.Red, 200, (Variables.holes[i].startingAddress - Variables.srart_address + Variables.holes[i].size / 2) * factor);
                    GFX.DrawString((Variables.holes[i].startingAddress + Variables.holes[i].size - 1).ToString(), myFont, Brushes.Red, 540, (Variables.holes[i].startingAddress + Variables.holes[i].size - 1 - Variables.srart_address) * factor);
                }
                pictureBox1.Image = drawingSurface;
            }
            else if (Variables.ProcessCounter > 0)
            {
                switch (Variables.typeOfAllocation)
                {
                    case 0: //fist fit
                        if (Variables.processes[Variables.ProcessCounter - 1].holeNumber == -2) {
                            Variables.processes[Variables.ProcessCounter - 1] = Variables.firstFit(Variables.holes, Variables.holeCounter, Variables.processes[Variables.ProcessCounter - 1], 1);
                             num = Variables.processes[Variables.ProcessCounter - 1].holeNumber;
                            if (num > -1)
                            {
                                Variables.processes[Variables.ProcessCounter - 1].startingAddress = Variables.holes[num].startingAddress;
                                Variables.holes[num].startingAddress = Variables.processes[Variables.ProcessCounter - 1].startingAddress + Variables.processes[Variables.ProcessCounter - 1].size;
                            } }
                        drawing();
                       if(num>-1) reArrange();
                        break;
                    case 1: //best fit
                        if (Variables.processes[Variables.ProcessCounter - 1].holeNumber == -2)
                        {
                            Variables.processes[Variables.ProcessCounter - 1] = Variables.bestFit(Variables.holes, Variables.holeCounter, Variables.processes[Variables.ProcessCounter - 1], 1);
                            num = Variables.processes[Variables.ProcessCounter - 1].holeNumber;
                            if (num > -1)
                            {
                                Variables.processes[Variables.ProcessCounter - 1].startingAddress = Variables.holes[num].startingAddress;
                                Variables.holes[num].startingAddress = Variables.processes[Variables.ProcessCounter - 1].startingAddress + Variables.processes[Variables.ProcessCounter - 1].size;
                            }
                        }
                        drawing();
                        if (num > -1) reArrange();
                        break;
                    case 2: //worst fit
                        if (Variables.processes[Variables.ProcessCounter - 1].holeNumber == -2)
                        {
                            Variables.processes[Variables.ProcessCounter - 1] = Variables.worstFit(Variables.holes, Variables.holeCounter, Variables.processes[Variables.ProcessCounter - 1], 1);
                            num = Variables.processes[Variables.ProcessCounter - 1].holeNumber;
                            if (num > -1)
                            {
                                Variables.processes[Variables.ProcessCounter - 1].startingAddress = Variables.holes[num].startingAddress;
                                Variables.holes[num].startingAddress = Variables.processes[Variables.ProcessCounter - 1].startingAddress + Variables.processes[Variables.ProcessCounter - 1].size;
                            }
                        }
                        drawing();
                        if (num > -1) reArrange();
                        break;
                    case 3: //compuction
                      //  Variables.firstFit(Variables.holes, Variables.holeCounter, Variables.processes[1], Variables.numOfFilling, Variables.ProcessCounter);
                        break;
                    default:
                        if (Variables.processes[Variables.ProcessCounter - 1].holeNumber == -2)
                        {
                            Variables.processes[Variables.ProcessCounter - 1] = Variables.firstFit(Variables.holes, Variables.holeCounter, Variables.processes[Variables.ProcessCounter - 1], 1);
                            num = Variables.processes[Variables.ProcessCounter - 1].holeNumber;
                            if (num > -1)
                            {
                                Variables.processes[Variables.ProcessCounter - 1].startingAddress = Variables.holes[num].startingAddress;
                                Variables.holes[num].startingAddress = Variables.processes[Variables.ProcessCounter - 1].startingAddress + Variables.processes[Variables.ProcessCounter - 1].size;
                            }
                        }
                        drawing();
                        if (num > -1) reArrange();
                        break;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = 0; i < Variables.ProcessCounter; i++)
            {
                if (Variables.processes[i].processName == textBox1.Text) index = i;
            }
            int h = Variables.processes[index].holeNumber;
            Variables.holes[h].startingAddress = Variables.processes[index].startingAddress;
            Variables.holes[h].size = Variables.holes[h].size + Variables.processes[index].size;
            //swap(index);
            //label1.Text = "";
            Variables.deleteProcess(index, Variables.processes, Variables.ProcessCounter);
            Variables.ProcessCounter--;
            /*int temp = Variables.ProcessCounter;
            Variables.ProcessCounter = 0;
            for(int i = 0; i < temp; i++)
            {
                button1_Click(sender, e);
                Variables.ProcessCounter++;
            }*/
           // Variables.FirstFit(Variables.holes,Variables.holeCounter,Variables.processes,Variables.ProcessCounter);
            button1_Click(sender,e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
