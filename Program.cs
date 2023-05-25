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

            var errors = new List<string>();

            if(args.Length != 6)
            {
                Console.WriteLine("Required arguments:");
                Console.WriteLine("NP - population size [>=4]");
                Console.WriteLine("CR - crossover probability [0,1]");
                Console.WriteLine("F - differential weight [0,2]");
                Console.WriteLine("I - iterations [>=1]");
                Console.WriteLine("D - dimensions [>=1]");
                Console.WriteLine("FU - function:");
                Console.WriteLine("  SP - Sphere");
                Console.WriteLine("  RA - Rastrigin");
                Console.WriteLine("  MI - Michalewicz");
                Console.WriteLine("  SC - Schwefel");
                Console.WriteLine("  AC - Ackley");
                Console.WriteLine("  GR - Griewank");
                return;
            }
            
            // UWAGA - zakładamy określoną kolejność argumentów

            if (!int.TryParse(args[0], out int np))
            {
                errors.Add("Cannot parse 'NP' to integer");
            }

            if (np < 4)
            {
                errors.Add("'NP' has to be greater than or equal to 4");
            }

            if (!double.TryParse(args[1], out double cr))
            {
                errors.Add("Cannot parse 'CR' to double");
            }

            if (cr < 0 || cr > 1)
            {
                errors.Add("'CR' has to be between 0 and 1");
            }

            if (!double.TryParse(args[2], out double f))
            {
                errors.Add("Cannot parse 'F' to double");
            }

            if (f < 0 || f > 2)
            {
                errors.Add("'F' has to be between 0 and 2");
            }

            if (!int.TryParse(args[3], out int i))
            {
                errors.Add("Cannot parse 'I' to integer");
            }

            if (i < 1)
            {
                errors.Add("'I' has to be greater than or equal to 1");
            }

            if (!int.TryParse(args[4], out int d))
            {
                errors.Add("Cannot parse 'D' to integer");
            }

            if (d < 1)
            {
                errors.Add("'D' has to be greater than or equal to 1");
            }

            FitnessFunction fitnessFunction = null;
            Tuple<double, double> domain = null;

            switch (args[5].ToUpper())
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
                    errors.Add("'FU' value unrecognized");
                    break;
            }

            if (errors.Any())
            {
                Console.WriteLine(string.Join("\n", errors));
                return;
            }

            // Notatki z zajęć
            // 1. Umożliwić podawanie argumentów, także funkcji testowej [done]
            // 2. Pokazać postęp przy każdej iteracji (jakiś output częściowy, nie tylko finalny) [done]

            var parameters = new Parameters
            {
                AgentsCount = np,
                CR = cr,
                F = f,
                Iterations = i,
                Dimensions = d,
                FitnessFunction = fitnessFunction,
                Domain = domain,
            };

            var solver = new Solver(parameters);
            solver.Run();
        }
    }
}
