using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentialEvolution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var errors = new List<string>();

            if(args.Length != 3)
            {
                Console.WriteLine("Three arguments required:\nNP - population size [>=4]\nCR - crossover probability [0,1]\nF  - differential weight [0,2]");
                return;
            }
            
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

            if (errors.Any())
            {
                Console.WriteLine(string.Join("\n", errors));
                return;
            }

            Evolve(np, cr, f);
        }

        // https://en.wikipedia.org/wiki/Differential_evolution
        static void Evolve(int np, double cr, double f)
        {
            // notatki z zajęć
            // 1. trzeba umożliwić podawanie argumentów (done)
            // 2. pokazać postęp przy każdej iteracji (jakiś output częściowy, nie tylko finalny)
        }
    }
}
