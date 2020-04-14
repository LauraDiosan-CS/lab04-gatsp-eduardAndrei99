using System;


namespace AiLab4
{
    class Program
    {

        static Network network;
        static void Read()
        {
            int NumberOfNodes = 0;
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Edy\source\repos\AiLab4\AiLab4\graf.txt");
            NumberOfNodes = Int32.Parse(lines[0]);
            network = new Network(NumberOfNodes);

            for (int i = 1; i <= NumberOfNodes; i++)
            {
                string[] numbers = lines[i].Split(",");
                for(int j = 0; j < NumberOfNodes; j++)
                {
                    network.Matrix[i - 1, j] = Int32.Parse(numbers[j]); 
                }
            }           
        }
        static void Main(string[] args)
        {
            Read();
            GA ga = new GA(100, 1000, network);
            ga.initialisation();
            Chromosome bestChromosome = ga.bestChromosome();
            Console.WriteLine("Generation 0 best chromosome : " + bestChromosome.fitness);
            for (int i = 1; i <= ga.numberOfGenerations; i++)
            {
                ga.oneGeneration();
                if (bestChromosome.fitness > ga.bestChromosome().fitness)
                {
                    bestChromosome = ga.bestChromosome();
                }
                Console.WriteLine("Generation " + i + " best chromosome : " + bestChromosome.fitness);
            }
            System.IO.File.WriteAllText(@"output.txt", "");
            using (System.IO.StreamWriter file =
                           new System.IO.StreamWriter(@"output.txt", true))
            {
                for (int i = 0; i < network.NumberOfNodes; i++)
                {
                    file.Write(bestChromosome.repres[i] + " ");
                }
                file.Write("0");
            }

        }
    }
}
