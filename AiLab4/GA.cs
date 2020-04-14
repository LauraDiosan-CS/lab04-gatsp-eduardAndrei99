using AiLab4;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiLab4
{
    class GA
    {
        public Network network { get; set; }
        public int popSize { get; set; }
        public int numberOfGenerations { get; set; }
        private Chromosome[] population;

        public GA(int popSize, int numberOfGen, Network network)
        {
            this.numberOfGenerations = numberOfGen;
            this.popSize = popSize;
            this.network = network;
            population = new Chromosome[popSize];
        }

        public void initialisation()
        {
            for (int i = 0; i < popSize; i++)
            {
                Chromosome c = new Chromosome(network);
                population[i] = c;
            }
            evaluation();
        }

        private void evaluation()
        {
            for (int i = 0; i < this.popSize; i++)
            {
                population[i].fitness = network.Modularity(population[i].repres);
            }
        }

        public Chromosome bestChromosome()
        {
            Chromosome best = this.population[0];
            foreach (Chromosome c in population)
            {
                if (c.fitness < best.fitness)
                {
                    best = c;
                }
            }
            return best;
        }

        private int worstChromosome()
        {
            int worst = 0;
            for (int i = 0; i < this.popSize; i++)
            {
                if (population[i].fitness > population[worst].fitness)
                {
                    worst = i;
                }
            }
            return worst;
        }

        private int selection()
        {
            Random rnd = new Random();
            int pos1 = rnd.Next(0, popSize - 1);
            int pos2 = rnd.Next(0, popSize - 1);
            if (population[pos1].fitness > population[pos2].fitness)
            {
                return pos2;
            }
            else
            {
                return pos1;
            }
        }

        public void oneGeneration()
        {
            Chromosome[] newPop = new Chromosome[this.popSize];
            for (int i = 0; i < this.popSize; i++)
            {
                Chromosome p1 = this.population[this.selection()];
                Chromosome p2 = this.population[this.selection()];
                Chromosome off = p1.crossover(p2);
                off.mutation();
                newPop[i] = off;
            }
            this.population = newPop;
            this.evaluation();
        }

        public void oneGenerationElitism()
        {
            Chromosome[] newPop = new Chromosome[this.popSize];
            newPop[0] = this.bestChromosome();
            for (int i = 1; i < this.popSize; i++)
            {
                Chromosome p1 = this.population[this.selection()];
                Chromosome p2 = this.population[this.selection()];
                Chromosome off = p1.crossover(p2);
                off.mutation();
                newPop[i] = off;
            }
            this.population = newPop;
            evaluation();
        }

        public void oneGenerationSteadyState()
        {
            for (int i = 0; i < this.popSize; i++)
            {
                Chromosome p1 = this.population[this.selection()];
                Chromosome p2 = this.population[this.selection()];
                Chromosome off = p1.crossover(p2);
                off.mutation();
                off.fitness = this.network.Modularity(off.repres);
                int worst = this.worstChromosome();
                if (off.fitness < population[worst].fitness)
                {
                    population[worst] = off;
                }
            }
        }



    }
}
