﻿using CommandLine;

namespace DifferentialEvolution.Models
{
    internal class ProgramOptions
    {
        [Option('n', "populationsize", Required = true, HelpText = "Population size [>=4]")]
        public int PopulationSize { get; set; }

        [Option('c', "crossoverprobability", Required = true, HelpText = "Crossover probability [0,1]")]
        public double CrossoverProbability { get; set; }

        [Option('f', "differentialweight", Required = true, HelpText = "Differential weight [0,2]")]
        public double DifferentialWeight { get; set; }

        [Option('i', "iterations", Required = true, HelpText = "Iterations [>=1]")]
        public int Iterations { get; set; }

        [Option('d', "dimensions", Required = true, HelpText = "Dimensions [>=1]")]
        public int Dimensions { get; set; }

        [Option('o', "function", Required = true, HelpText = "Function:\nSP - Sphere \nRA - Rastrigin \nMI - Michalewicz \nSC - Schwefel \nAC - Ackley \nGR - Griewank \nRO - Rosenbrock \nWE - Weierstrass \nSF - Schaffer \nLE - Levy \nSH - Shubert \nHA - HappyCat")]
        public string Function { get; set; }

        [Option('m', "mutationscheme", Required = true, HelpText = "Mutation scheme {BEST, RAND}")]
        public string MutationScheme { get; set; }

        [Option('k', "numberofvectors", Required = true, HelpText = "Number of vectors {1, 2}")]
        public int NumberOfVectors { get; set; }
    }
}
