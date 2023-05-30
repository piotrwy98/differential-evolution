using System;
using System.Linq;

namespace DifferentialEvolution.Models
{
    public class Functions
    {
        public static Tuple<double, double> SphereDomain = new(-5.12, 5.12);
        public static Tuple<double, double> RastriginDomain = new(-5.12, 5.12);
        public static Tuple<double, double> MichalewiczDomain = new(0, Math.PI);
        public static Tuple<double, double> SchwefelDomain = new(-500, 500);
        public static Tuple<double, double> AckleyDomain = new(-32.768, 32.768);
        public static Tuple<double, double> GriewankDomain = new(-600, 600);
        public static Tuple<double, double> RosenbrockDomain = new(-2.048, 2.048);
        public static Tuple<double, double> WeierstrassDomain = new(-0.5, 0.5);
        public static Tuple<double, double> SchafferDomain = new(-100, 100);
        public static Tuple<double, double> LevyDomain = new(-10, 10);
        public static Tuple<double, double> ShubertDomain = new(-10, 10);
        public static Tuple<double, double> HappyCatDomain = new(-2, 2);

        /// <summary>
        /// min is at f(0,0) = 0
        /// </summary>
        public static double SphereFunction(Individual individual, int dimensions)
        {
            double sum = 0;

            foreach (double x in individual.Elements)
            {
                sum += Math.Pow(x, 2);
            }
            return sum;
        }

        /// <summary>
        /// min is at f(0,0) = 0
        /// </summary>
        public static double RastriginFunction(Individual individual, int dimensions)
        {
            double sum = 0;

            foreach (double x in individual.Elements)
            {
                sum += Math.Pow(x, 2) - 10 * Math.Cos(2 * Math.PI * x);
            }
            return 10 * dimensions + sum;
        }

        /// <summary>
        /// min is at  f(2.20, 1.57) = -1.8013
        /// </summary>
        public static double MichalewiczFunction(Individual individual, int dimensions)
        {
            double outcome = 0;
            double sum = 0;
            double m = 10;

            for (int i = 0; i < dimensions; i++)
            {
                sum += Math.Sin(individual.Elements[i])
                    *
                    Math.Pow(
                            Math.Sin(((i + 1) * Math.Pow(individual.Elements[i], 2))
                                    / Math.PI)
                            , 2 * m);
            }

            outcome = -1 * sum;

            return outcome;
        }

        /// <summary>
        /// min is at f(420.9687,420.9687) = 0
        /// </summary>
        public static double SchwefelFunction(Individual individual, int dimensions)
        {
            double outcome = 0;
            double sum = 0;
            outcome = 418.9829 * dimensions;

            for (int i = 0; i < dimensions; i++)
            {
                sum += individual.Elements[i] * (Math.Sin(Math.Sqrt(Math.Abs(individual.Elements[i]))));
            }

            outcome -= sum;
            return outcome;
        }

        /// <summary>
        /// min is at f(X) = 0 at X = (0,...,0)
        /// </summary>
        public static double AckleyFunction(Individual individual, int dimensions)
        {
            double a = 20;
            double b = 0.2;
            double c = 2 * Math.PI;

            double firstSum = 0;
            double secondSum = 0;

            for (int i = 0; i < dimensions; i++)
            {
                firstSum += Math.Pow(individual.Elements[i], 2);
            }

            for (int i = 0; i < dimensions; i++)
            {
                secondSum += Math.Cos(c * individual.Elements[i]);
            }

            double firstPart = Math.Exp(-b * Math.Sqrt((1 / dimensions) * firstSum));
            double secondPart = Math.Exp((1 / dimensions) * secondSum);

            return -a * firstPart - secondPart + a + Math.Exp(1);
        }

        /// <summary>
        /// min is at f(X) = 0 at X = (0,...,0)
        /// </summary>
        public static double GriewankFunction(Individual individual, int dimensions)
        {
            double firstSum = 0;
            double prod = 1;

            for (int i = 0; i < dimensions; i++)
            {
                firstSum += (Math.Pow(individual.Elements[i], 2) / 4000.0);
            }

            for (int i = 0; i < dimensions; i++)
            {
                prod *= Math.Cos(individual.Elements[i] / Math.Sqrt(i + 1));
            }

            return (firstSum - prod) + 1;
        }

        public static double RosenbrockFunction(Individual individual, int dimensions)
        {
            double sum = 0;
            for (int i = 0; i < dimensions - 1; i++)
            {
                double xi = individual.Elements[i];
                double xiPlus1 = individual.Elements[i + 1];
                sum += 100 * Math.Pow(xiPlus1 - Math.Pow(xi, 2), 2) + Math.Pow(xi - 1, 2);
            }

            return sum;
        }

        public static double WeierstrassFunction(Individual individual, int dimensions)
        {
            double a = 0.5;
            double b = 3;
            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j <= 20; j++)
                {
                    sum1 += Math.Pow(a, j) * Math.Cos(2 * Math.PI * Math.Pow(b, j) * (individual.Elements[i] + 0.5));
                }
            }

            for (int j = 0; j <= 20; j++)
            {
                sum2 += Math.Pow(a, j) * Math.Cos(2 * Math.PI * Math.Pow(b, j) * 0.5);
            }

            return sum1 - dimensions * sum2;
        }

        public static double SchafferFunction(Individual individual, int dimensions)
        {
            double sum = 0;
            for (int i = 0; i < dimensions - 1; i++)
            {
                double xi = individual.Elements[i];
                double xiPlus1 = individual.Elements[i + 1];
                double term = Math.Pow(Math.Sin(Math.Sqrt(xi * xi + xiPlus1 * xiPlus1)), 2) - 0.5;
                sum += 0.5 + term / (1 + 0.001 * (xi * xi + xiPlus1 * xiPlus1));
            }

            return sum;
        }

        public static double LevyFunction(Individual individual, int dimensions)
        {
            double w = 1 + (individual.Elements[0] - 1) / 4;
            double sum = Math.Pow(Math.Sin(Math.PI * w), 2);
            for (int i = 0; i < dimensions - 1; i++)
            {
                double xi = individual.Elements[i];
                double xiPlus1 = individual.Elements[i + 1];
                double term = Math.Pow(w - 1, 2) * (1 + 10 * Math.Pow(Math.Sin(Math.PI * xiPlus1), 2));
                sum += term;
            }

            double term2 = Math.Pow(Math.Sin(2 * Math.PI * individual.Elements[dimensions - 1]), 2);
            return sum + term2;
        }

        public static double ShubertFunction(Individual individual, int dimensions)
        {
            double sum1 = 0;
            double sum2 = 0;
            for (int i = 0; i < dimensions; i++)
            {
                double xi = individual.Elements[i];
                sum1 += i * Math.Cos((i + 1) * xi + i);
                sum2 += i * Math.Sin((i + 1) * xi + i);
            }

            return sum1 * sum2;
        }

        public static double HappyCatFunction(Individual individual, int dimensions)
        {
            double sumSq = individual.Elements.Sum(xi => xi * xi);
            double sumFour = individual.Elements.Sum(xi => Math.Pow(xi, 4));
            double term1 = Math.Pow(sumSq - dimensions, 2);
            double term2 = Math.Pow(0.5 * sumSq + sumFour, 0.25);
            double term3 = 0.5 * (1 + 0.001 * (term1 + term2));
            return term3;
        }
    }
}
