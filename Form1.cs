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
    
    public partial class memoryManagment : Form
    {

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButtonFirstFit.Checked == true) Variables.typeOfAllocation = 0;
            else if (radioButtonBestFit.Checked == true) Variables.typeOfAllocation = 1;
            else if (radioButtonWorstFit.Checked == true) Variables.typeOfAllocation = 2;
            else if(radioButtonComputex.Checked == true) Variables.typeOfAllocation = 3;
            Form2 f2 = new Form2();
            f2.ShowDialog(); // Shows Form2
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hole hole;
            domainUpDownHoleStarting.Items.Add(domainUpDownHoleStarting.Text);
            domainUpDownHoleSize.Items.Add(domainUpDownHoleSize.Text);
            string temp = domainUpDownHoleSize.Text;
            int inttemp = Int32.Parse(temp);
            hole.size = inttemp;
            temp = domainUpDownHoleStarting.Text;
            inttemp = Int32.Parse(temp);
            hole.startingAddress = inttemp;
            Variables.holes[Variables.holeCounter] = hole;
            Variables.holeCounter++;
            labelNumberOfHoles.Text = Variables.holeCounter.ToString();
        }

    /*    public void allocate_for_the_first_time()
        {

            int address = 0;
            fullTableCounter = Variables.ProcessCounter;
            freeTableCounter = 1;
            Allocation = separator + "0" + "\r\n";
            for (int i = 0; i < Variables.ProcessCounter; i++)
            {
                FullTable_st[i] = address;
                FullTable_limit[i] = ProcessSize[i];
                FullTable_pr[i] = ProcessName[i];
                Allocation += FullTable_pr[i] + "\r\n";
                Allocation += separator;
                address += FullTable_limit[i];
                Allocation += address + "\r\n";
            }
            FreeTable_st[0] = address;
            Allocation += "\r\n" + separator;
            FreeTable_limit[0] = address - Variables.limit;
            Allocation += Variables.limit;
            Variables.IntArrayBubbleSort(FullTable_limit, FullTable_st, FullTable_pr);
        }*/

        private void button3_Click(object sender, EventArgs e)
        {
            string temp = textBoxStartingAddress.Text;
            Variables.srart_address = Int32.Parse(temp);
            temp = textBoxMemorySize.Text;
            Variables.limit = Int32.Parse(temp);
        }

        public memoryManagment()
        {
            InitializeComponent();
        }

        private void REMOVE_Click(object sender, EventArgs e)
        {
            Variables.holeCounter = 0;
            Variables.ProcessCounter = 0;
            Variables.srart_address = 0;
            Variables.limit = 0;
            domainUpDownHoleSize.Items.Clear();
            domainUpDownHoleStarting.Items.Clear();
            domainUpDownProcessName.Items.Clear();
            domainUpDownProcessSize.Items.Clear();
            labelNumber.Text = "0";
            labelNumberOfHoles.Text = "0";
            textBoxMemorySize.Text = "";
            textBoxStartingAddress.Text = "";
            domainUpDownHoleSize.Text = "";
            domainUpDownHoleStarting.Text = "";
            domainUpDownProcessName.Text = "";
            domainUpDownProcessSize.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Variables.ProcessCounter = 0;
            domainUpDownProcessName.Items.Clear();
            domainUpDownProcessSize.Items.Clear();
            domainUpDownProcessName.Text = "";
            domainUpDownProcessSize.Text = "";
            labelNumber.Text = "0";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Variables.holeCounter = 0;
            domainUpDownHoleSize.Items.Clear();
            domainUpDownHoleStarting.Items.Clear();
            domainUpDownHoleSize.Text = "";
            domainUpDownHoleStarting.Text = "";
            labelNumberOfHoles.Text = "0";
        }

        private void DELETE_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonALLOCATE_Click(object sender, EventArgs e)
        {

        }

        private void buttonADD_Click(object sender, EventArgs e)
        {
            domainUpDownProcessName.Items.Add(domainUpDownProcessName.Text);
            domainUpDownProcessSize.Items.Add(domainUpDownProcessSize.Text);
            Variables.processes[Variables.ProcessCounter].processName = domainUpDownProcessName.Text; 
            string temp = domainUpDownProcessSize.Text;
            int inttemp = Int32.Parse(temp);
            Variables.processes[Variables.ProcessCounter].size = inttemp;
            Variables.processes[Variables.ProcessCounter].holeNumber = -2;
            Variables.ProcessCounter++;
            labelNumber.Text = Variables.ProcessCounter.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            labelNumber.Text = Variables.ProcessCounter.ToString();
            labelNumberOfHoles.Text = Variables.holeCounter.ToString();
            domainUpDownProcessName.Text = "";
            domainUpDownProcessSize.Text = "";
            domainUpDownProcessName.Items.Clear();
            domainUpDownProcessSize.Items.Clear();
            for(int i = 0; i < Variables.ProcessCounter; i++)
            {
                domainUpDownProcessName.Items.Add(Variables.processes[i].processName);
                domainUpDownProcessSize.Items.Add(Variables.processes[i].size.ToString());
            }
            domainUpDownHoleStarting.Items.Clear();
            domainUpDownHoleSize.Items.Clear();
            for(int i = 0; i < Variables.holeCounter; i++)
            {
                domainUpDownHoleSize.Items.Add(Variables.holes[i].size.ToString());
                domainUpDownHoleStarting.Items.Add(Variables.holes[i].startingAddress.ToString());
            }
        }
    }
}
