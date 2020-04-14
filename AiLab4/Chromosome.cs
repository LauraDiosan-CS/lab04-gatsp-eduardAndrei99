using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiLab4
{
    class Chromosome
    {
        public Network network;
        public int[] repres { get; set; }
        public Double fitness;

        public void Shuffle(int[] array)
        {
            Random random = new Random();
            int n = array.Count();
            array[0] = 0;
            while (n > 2)
            {
                n--;
                int i = random.Next(2,n);
                int temp = array[i];
                array[i] = array[n];
                array[n] = temp;
            }
        }

        public Chromosome(Network net)
        {
            this.network = net;
            fitness = 0;
            repres = new int[network.NumberOfNodes];
            for(int i = 0; i < network.NumberOfNodes; i++)
            {
                this.repres[i] = i;
            }
            Shuffle(repres);
        }

        public Chromosome crossover(Chromosome c)
        {
            Random rnd = new Random();
            int poz1 = 0; int poz2 = 0;
            while (poz1 == poz2) { 
            poz1 = rnd.Next(1, this.network.NumberOfNodes - 1);
            poz2 = rnd.Next(1, this.network.NumberOfNodes - 1);
            }   
            if (poz2 < poz1)
            {
                int aux = poz1;
                poz1 = poz2;
                poz2 = aux;
            }
            bool[] ap = new bool[this.network.NumberOfNodes];
            for (int i = 0; i < ap.Length; i++)
            {
                ap[i] = false;
            }
            Chromosome nextChromosome = new Chromosome(network);
            //Console.WriteLine("pos1 = " + poz1);
            //Console.WriteLine("pos2 = " + poz2);
            int nrP = poz2 - poz1 + 1;
            for( int i = poz1; i <= poz2; i++)
            {
                nextChromosome.repres[i] = this.repres[i];
                ap[this.repres[i]] = true;
            }

            int index = 1;
            for (int i = 1; i < poz1; i++)
            {
                while(ap[c.repres[index]] == true) {
                    index++;
                }
                nextChromosome.repres[i] = c.repres[index];
                ap[c.repres[index]] = true;
            }

            for (int i = poz2+1; i <this.network.NumberOfNodes; i++)
            {
                while (ap[c.repres[index]] == true)
                {
                    index++;
                }
                nextChromosome.repres[i] = c.repres[index];
                ap[c.repres[index]] = true;
            }

            return nextChromosome;

        }

        public void mutation()
        {
            Random rnd = new Random();
            int a = rnd.Next(1, network.NumberOfNodes - 1);
            int b = rnd.Next(1, network.NumberOfNodes - 1);
            int aux = this.repres[a];
            repres[a] = repres[b];
            repres[b] = aux;
        }



    }
}
