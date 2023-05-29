using CommandLine;

using DifferentialEvolution.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentialEvolution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // https://en.wikipedia.org/wiki/Differential_evolution

            List<string> errors = new();

            Parameters parameters = Parser.Default.ParseArguments<ProgramOptions>(args).MapResult(x =>
            {
                if (x.PopulationSize < 4)
                    errors.Add("'-n (Population size)' has to be greater than or equal to 4");

                if (x.CrossoverProbability < 0 || x.CrossoverProbability > 1)
                    errors.Add("'-c (Crossover probability)' has to be between 0 and 1");

                if (x.DifferentialWeight < 0 || x.DifferentialWeight > 2)
                    errors.Add("'-f (Differential weight)' has to be between 0 and 2");

                if (x.Iterations < 1)
                    errors.Add("'-i (Iterations)' has to be greater than or equal to 1");

                if (x.Dimensions < 1)
                    errors.Add("'-d (Dimensions)' has to be greater than or equal to 1");

                FitnessFunction fitnessFunction = null;
                Tuple<double, double> domain = null;

                switch (x.Function.ToUpper())
                {
                    case "SP":
                        fitnessFunction = Functions.SphereFunction;
                        domain = Functions.SphereDomain;
                        break;

                    case "RA":
                        fitnessFunction = Functions.RastriginFunction;
                        domain = Functions.RastriginDomain;
                        break;

                    case "MI":
                        fitnessFunction = Functions.MichalewiczFunction;
                        domain = Functions.MichalewiczDomain;
                        break;

                    case "SC":
                        fitnessFunction = Functions.SchwefelFunction;
                        domain = Functions.SchwefelDomain;
                        break;

                    case "AC":
                        fitnessFunction = Functions.AckleyFunction;
                        domain = Functions.AckleyDomain;
                        break;

                    case "GR":
                        fitnessFunction = Functions.GriewankFunction;
                        domain = Functions.GriewankDomain;
                        break;

                    default:
                        errors.Add("'-o (Function)' option unrecognized");
                        break;
                }

                return new Parameters()
                {
                    AgentsCount = x.PopulationSize,
                    CR = x.CrossoverProbability,
                    F = x.DifferentialWeight,
                    Iterations = x.Iterations,
                    Dimensions = x.Dimensions,
                    FitnessFunction = fitnessFunction,
                    Domain = domain,
                };
            }, x => null);

            if (parameters == null)
                return;

            if (errors.Any())
            {
                Console.WriteLine("Errors:");
                Console.WriteLine(string.Join("\n", errors));
                return;
            }

            // Notatki z zajęć
            // 1. Umożliwić podawanie argumentów, także funkcji testowej [done]
            // 2. Pokazać postęp przy każdej iteracji (jakiś output częściowy, nie tylko finalny) [done]

            Solver solver = new(parameters);
            solver.Run();
        }
    }
}
