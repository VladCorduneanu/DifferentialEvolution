using System;
using System.Collections.Generic;
using System.Text;

namespace EvolutieDiferentialaIA
{
    public class DifferentialEvolution
    {
        int c_SolutionDimension = 2;
        int c_NoIndividuals = 10;
        double c_LowerBound = -5.12;
        double c_UpperBound = 5.12;
        double c_MutationProbability = 0.02;
        double c_CrossProbability = 0.9;
        double c_AcceptedError = 10e-6;
        int c_MaxEpoch = 50000;
        double c_DifferentialFactor = 0.8;

        List<Individual> Individuals;
        int CurrentEpoch = 0;
        Equation EquationToSolve;


        private Individual Elitism()
        {
            Individual elite = Individuals[0];

            for (int i = 0; i < Individuals.Count; ++i)
            {
                Individual curr = Individuals[i];
                if (curr.GetError() < elite.GetError())
                    elite = curr;
            }

            return elite;
        }

        internal void SetEquation(Equation eq)
        {
            EquationToSolve = eq;
        }

        internal void ListPopulation()
        {
            for (int i = 0; i < c_NoIndividuals; ++i)
            {
                Console.WriteLine(Individuals[i].ToString());
            }
        }

        private List<Individual> Selection()
        {
            List<Individual> selectedIndividuals = new List<Individual>();
            Random rand = new Random();
            Individual c1, c2;

            for (int i = 0; i < c_NoIndividuals / 2; ++i)
            {
                c1 = Individuals[rand.Next(Individuals.Count)];
                c2 = Individuals[rand.Next(Individuals.Count)];
                if (c1.GetError() < c2.GetError())
                {
                    selectedIndividuals.Add(c1);
                }
                else
                {
                    selectedIndividuals.Add(c2);
                }
            }

            return selectedIndividuals;
        }

        private List<Individual> Crossing(List<Individual> selectedPopulation)
        {
            List<Individual> crossedPopulation = new List<Individual>();
            Random rand = new Random();
            Individual p1, p2, c1, c2;

            while (crossedPopulation.Count < c_NoIndividuals)
            {
                p1 = selectedPopulation[rand.Next(selectedPopulation.Count)];
                p2 = selectedPopulation[rand.Next(selectedPopulation.Count)];
                c1 = new Individual(p1.m_List,EquationToSolve);
                c2 = new Individual(p2.m_List,EquationToSolve);

                if (rand.Next(100) / 100.0f < c_MutationProbability)
                    for (int i = 0; i < p1.m_List.Length; i++)
                    {
                        // Incrucisare uniforma
                        // Copiii rezultati din 2 parinti vor fi total opusi
                        if (rand.Next(100) + 1 < 50)
                        {
                            c1.m_List[i] = p2.m_List[i];
                            c2.m_List[i] = p1.m_List[i];
                        }
                    }
                crossedPopulation.Add(c1);
                crossedPopulation.Add(c2);
            }

            return crossedPopulation;
        }

        private List<Individual> Mutate(List<Individual> crossedPopulation)
        {
            Random rand = new Random();
            List<Individual> mutatedList = new List<Individual>();
            for (int i = 0; i < crossedPopulation.Count; ++i)
            {
                Individual individual = crossedPopulation[i];
                mutatedList.Add(new Individual(individual.m_List,individual.m_Equation));

                for (int j = 0; j < individual.m_List.Length; j++)
                {
                    if (rand.Next(100) / 100.0f < c_MutationProbability)
                    {
                        mutatedList[i].m_List[j] = (rand.NextDouble() * (c_UpperBound - c_LowerBound)) + c_LowerBound;
                    }
                }


            }
            return mutatedList;
        }

        internal double[] Execute()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Generare Populatie Initiala! nr pop:" + c_NoIndividuals);
            Console.WriteLine("Mutation prob: " + c_MutationProbability);
            Console.WriteLine("Cross prob: " + c_CrossProbability);

            Individuals = new List<Individual>();
            Random rand = new Random();

            // Creare Populatie
            for (int i = 0; i < c_NoIndividuals; ++i)
            {
                double[] vect = new double[c_SolutionDimension];
                for (int j = 0; j < c_SolutionDimension; ++j)
                {
                    vect[j] = (rand.NextDouble() * (c_UpperBound - c_LowerBound)) + c_LowerBound;
                }

                Individuals.Add(new Individual(vect, EquationToSolve));
            }

            List<Individual> selectedPopulation;
            List<Individual> crossedPopulation;
            List<Individual> mutantPopulation;
            Individual elite = Elitism();

            CurrentEpoch = 0;
            while (CurrentEpoch < c_MaxEpoch)
            {
                // Increment epoch
                CurrentEpoch++;

                //Select Population for crossing
                selectedPopulation = Selection();

                //Cross Population
                crossedPopulation = Crossing(selectedPopulation);

                //Mutate Population
                mutantPopulation = Mutate(crossedPopulation);

                Individuals = mutantPopulation;

                Individuals.Add(elite);
                //Use elitism
                elite = Elitism();

            }
            Console.WriteLine("Epoca: " + CurrentEpoch + "\n");
            Console.WriteLine("Elita este:" + elite);
            Console.WriteLine("Cu eroarea: " + elite.GetError());

            return elite.m_List;
        }

        internal double[] ExecuteDifferential()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Generare Populatie Initiala! nr pop:" + c_NoIndividuals);
            Console.WriteLine("Cross prob: " + c_CrossProbability);
            Console.WriteLine("Differential parameter: " + c_DifferentialFactor);

            Individuals = new List<Individual>();
            Random rand = new Random();

            // Creare Populatie
            for (int i = 0; i < c_NoIndividuals; ++i)
            {
                double[] vect = new double[c_SolutionDimension];
                for (int j = 0; j < c_SolutionDimension; ++j)
                {
                    vect[j] = (rand.NextDouble() * (c_UpperBound - c_LowerBound)) + c_LowerBound;
                }

                Individuals.Add(new Individual(vect,EquationToSolve));
            }

            Individual elite = Elitism();

            CurrentEpoch = 0;
            while (elite.GetError() > c_AcceptedError && CurrentEpoch < c_MaxEpoch)
            {
                // Increment epoch
                CurrentEpoch++;

                // Pentru fiecare individ
                for (int i = 0; i < Individuals.Count; ++i)
                {
                    Individual a;
                    Individual b;
                    Individual c;

                    Individual x = Individuals[i];
                    Individual y = new Individual(x.m_List,x.m_Equation);

                    do
                    {
                        a = Individuals[rand.Next(Individuals.Count)];
                    }
                    while (Individuals[i] == a);

                    do
                    {
                        b = Individuals[rand.Next(Individuals.Count)];
                    }
                    while (Individuals[i] == b || a == b);

                    do
                    {
                        c = Individuals[rand.Next(Individuals.Count)];
                    }
                    while (Individuals[i] == c || a == c || b == c);

                    for (int j = 0; j < y.m_List.Length; ++j)
                    {
                        double ri = rand.NextDouble();
                        if (ri <= c_CrossProbability)
                        {
                            y.m_List[j] = a.m_List[j] + c_DifferentialFactor * (b.m_List[j] - c.m_List[j]);
                        }
                    }

                    if (x.GetError() >= y.GetError())
                        Individuals[i] = y;
                }
                elite = Elitism();
            }
            Console.WriteLine("Epoca: " + CurrentEpoch + "\n");
            Console.WriteLine("Elita este:" + elite);
            Console.WriteLine("Cu eroarea: " + elite.GetError());

            return elite.m_List;
        }
    }
}
