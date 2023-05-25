using System;
using System.Collections.Generic;
using System.Text;

namespace DifferentialEvolution.Models
{
    public class Individual
    {
        public double Fitness { get; set; }
        public int Dimensions { get { return Elements.Count; } }
        public List<double> Elements;

        public Individual(List<double> Elements)
        {
            this.Elements = Elements;
        }

        public Individual()
        {
            Elements = new List<double>();
        }

        public double Evaluate(FitnessFunction fitnessFunction)
        {
            Fitness = fitnessFunction.Invoke(this, Dimensions);
            return Fitness;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (var item in Elements)
            {
                var rounded = Math.Round(item, 4).ToString();
                if (rounded == "-0")
                {
                    rounded = "0";
                }
                s.Append(rounded + ", ");
            }
            s.Remove(s.Length - 2, 2);
            return s.ToString();
        }
    }
}
