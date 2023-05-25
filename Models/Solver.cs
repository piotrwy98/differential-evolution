﻿using System;
using System.Collections.Generic;

namespace DifferentialEvolution.Models
{
    public class Solver
    {
        public Parameters Parameters { get; set; }
        private static int _counter = 1;

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
            Individual best = population.GetBest();

            Console.Write("#" + _counter++ + " Fitness = " + string.Format("0,0000", Math.Round(best.Fitness, 4)));
            Console.WriteLine(string.Format("   Coordinates: ({0})", best.ToString()));
        }
    }
}
