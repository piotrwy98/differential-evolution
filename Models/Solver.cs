using System;
using System.Collections.Generic;
using System.IO;

namespace DifferentialEvolution.Models
{
    public class Solver
    {
        public Parameters Parameters { get; set; }
        public static int Counter = 1;

        public Solver(Parameters parameters)
        {
            Parameters = parameters;
        }

        public void Run()
        {
            Population population = CreatePopulation();
            DifferentialEvolution DE = new DifferentialEvolution(population, Parameters);

            DE.OnGenerationComplete += DE_OnGenerationComplete;
            DE.OnRunComplete += DE_OnRunComplete;

            DE.Run(Parameters.Iterations, Parameters.CR, Parameters.F);
        }

        public Population CreatePopulation()
        {
            List<Individual> individuals = new List<Individual>();
            for (int i = 1; i < Parameters.AgentsCount; i++)
            {
                Individual individual = new Individual();

                for (int j = 0; j < Parameters.Dimensions; j++)
                {
                    double randomNumber = RandomGenerator.GetDoubleRangeRandomNumber(Parameters.Domain.Item1, Parameters.Domain.Item2);
                    individual.Elements.Add(randomNumber);
                }

                individuals.Add(individual);
            }

            return new Population(individuals);
        }

        private void DE_OnRunComplete(object sender, EventArgs e)
        {
            //Console.WriteLine("Run complete");
        }

        private void DE_OnGenerationComplete(object sender, EventArgs e)
        {
            Population population = (Population)sender;
            var fitness = 0.0;

            switch(Parameters.MutationScheme)
            {
                case MutationScheme.BEST:
                    Individual best = population.GetBest();
                    fitness = Math.Round(best.Fitness, 4);
                    Console.Write($" #{Counter++}\t Fitness = {fitness, -10}\t");
                    Console.WriteLine($"Coordinates: ({best})");
                    break;

                case MutationScheme.RAND:
                    Individual random = population.GetRandom();
                    fitness = Math.Round(random.Fitness, 4);
                    Console.Write($" #{Counter++}\t Fitness = {fitness, -10}\t");
                    Console.WriteLine($"Coordinates: ({random})");
                    break;
                
                default:
                    return;
            }

            try
            {
                if (string.IsNullOrEmpty(Program.Results[Counter - 2]))
                {
                    Program.Results[Counter - 2] = fitness.ToString();
                }
                else
                {
                    Program.Results[Counter - 2] += "\t" + fitness.ToString();
                }
            }
            catch { }
        }
    }
}
