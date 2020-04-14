using System;
using System.Collections.Generic;
using System.Text;

namespace AiLab4
{
    class Network
    {
        public int NumberOfEdges;
        public int NumberOfNodes { get; set; }

        public Double[,] Matrix;

        public int[] Degrees;

        public Network(int numberOfNodes)
        {
            NumberOfEdges = 0;
            NumberOfNodes = numberOfNodes;
            Matrix = new Double[NumberOfNodes, NumberOfNodes];
            Degrees = new int[NumberOfNodes];
            for (int i = 0; i < NumberOfNodes; i++)
            {
                for (int j = 0; j < NumberOfNodes; j++)
                {
                    Matrix[i, j] = 0;
                }
                Degrees[i] = 0;
            }
        }

        public Double Modularity(int[] comunities)
        {
            Double cost = 0.0;
            int nodCurr = 0;
            for (int i = 0; i < this.NumberOfNodes; i++)
            {
                int next = comunities[i];
                cost += this.Matrix[nodCurr,next];
                nodCurr = next;
            }
            cost += this.Matrix[nodCurr, 0];
            return cost;

        }


    }
}
