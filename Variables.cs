using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Memory_Managment
{
    public struct Hole
    {
        public int startingAddress;
        public int size;
    }
    public struct Process
    {
        public String processName;
        public int startingAddress;
        public int size;
        public int holeNumber ;
    }
    class Variables
    {
        public static  Hole[] holes = new Hole[100];
        public static Process[] processes = new Process[100];
        public static Button[] removeProcess = new Button[100];
        public static int holeCounter = 0, ProcessCounter = 0,typeOfAllocation = 0, numOfFilling = 0 ,reArrangeCounter = 0;
        public static int srart_address = 0, limit = 0,tempHole = 0;
        public static void exchange(int[] data, int m, int n)
        {
            int temporary;
            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
        public static void exchange(String[] data, int m, int n)
        {
            String temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
        public static void IntArrayBubbleSort(int[] data, int[] data1, string[] data2 = null)
        {
            int i, j;
            int N = data.Length;

            for (j = N - 1; j > 0; j--)
            {
                for (i = 0; i < j; i++)
                {
                    if (data[i] > data[i + 1])
                    {
                        exchange(data, i, i + 1);
                        exchange(data1, i, i + 1);
                        if (data2 != null) exchange(data2, i, i + 1);
                    }
                }
            }
        }
        public static void deleteProcess(int i, Process[] processes , int n )
        {
            for(int k = i;k< n-1; k++)
            {
                processes[k] = processes[k+1];
            }
        }
        public static Process firstFit(Hole[] blockSize, int m,
                                   Process processSize ,int n)
        {
            // Stores block id of the block 
            // allocated to a process
            int[] allocation = new int[n];

            // Initially no block is assigned to any process
            for (int i = 0; i < allocation.Length; i++)
                allocation[i] = -1;

            // pick each process and find suitable blocks
            // according to its size ad assign to it
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (blockSize[j].size >= processSize.size )
                    {
                        // allocate block j to p[i] process
                        allocation[i] = j;

                        // Reduce available memory in this block.
                        blockSize[j].size -= processSize.size;

                        break;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (allocation[i] != -1)
                {
                     processSize.holeNumber = allocation[i] ;
                }
                else
                {
                    processSize.holeNumber = -1;
                }
            }
            return processSize;
        }
 
        public static Process bestFit(Hole[] blockSize, int m,
                                   Process processSize, int n)
        {

            // Stores block id of the block 
            // allocated to a process
            int[] allocation = new int[n];

            // Initially no block is assigned to
            // any process
            for (int i = 0; i < allocation.Length; i++)
                allocation[i] = -1;

            // pick each process and find suitable
            // blocks according to its size ad
            // assign to it
            for (int i = 0; i < n; i++)
            {

                // Find the best fit block for
                // current process
                int bestIdx = -1;
                for (int j = 0; j < m; j++)
                {
                    if (blockSize[j].size >= processSize.size)
                    {
                        if (bestIdx == -1)
                            bestIdx = j;
                        else if (blockSize[bestIdx].size
                                       > blockSize[j].size)
                            bestIdx = j;
                    }
                }

                // If we could find a block for
                // current process
                if (bestIdx != -1)
                {

                    // allocate block j to p[i] 
                    // process
                    allocation[i] = bestIdx;

                    // Reduce available memory in
                    // this block.
                    blockSize[bestIdx].size -= processSize.size;
                }
            }

            for (int i = 0; i < n; i++)
            {
 
                if (allocation[i] != -1)
                    processSize.holeNumber = allocation[i] ;
                else
                    processSize.holeNumber = -1;
            }
            return processSize;
        }
        public static Process worstFit(Hole[] blockSize, int m,
                                   Process processSize, int n)
        {
            // Stores block id of the block allocated to a
            // process
            int[] allocation = new int[n];

            // Initially no block is assigned to
            // any process
            for (int i = 0; i < allocation.Length; i++)
                allocation[i] = -1;

            // pick each process and find suitable blocks
            // according to its size ad assign to it
            for (int i = 0; i < n; i++)
            {
                // Find the best fit block for current process
                int wstIdx = -1;
                for (int j = 0; j < m; j++)
                {
                    if (blockSize[j].size >= processSize.size)
                    {
                        if (wstIdx == -1)
                            wstIdx = j;
                        else if (blockSize[wstIdx].size < blockSize[j].size)
                            wstIdx = j;
                    }
                }

                // If we could find a block for current process
                if (wstIdx != -1)
                {
                    // allocate block j to p[i] process
                    allocation[i] = wstIdx;

                    // Reduce available memory in this block.
                    blockSize[wstIdx].size -= processSize.size;
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (allocation[i] != -1)
                    processSize.holeNumber = allocation[i] ;
                else
                    processSize.holeNumber = -1;
            }
            return processSize;
        }
    }

}
