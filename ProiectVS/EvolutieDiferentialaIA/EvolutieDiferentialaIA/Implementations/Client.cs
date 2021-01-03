using System;
using System.Collections.Generic;
using System.Text;

namespace EvolutieDiferentialaIA.Implementations
{
    public class Client
    {
        // Singleton
        private static Client _instance = null;
        private Client(){ }
        public static Client GetInstance()
        {
            if (_instance == null)
                _instance = new Client();

            return _instance;
        }

        string TextToReturn = "";

        // Parameters
        public bool IsPolinom = true;
        public string PolynomCoeffs = "";
        public string EquationType = "";

        public string Resolve()
        {
            TextToReturn = "";
            Polynom polynom = ReadPolynom();
            int numberOfRoots = polynom.GetDegree();
            DifferentialEvolution alg = new DifferentialEvolution();

            for (int i = 0; i < numberOfRoots; ++i)
            {
                alg.SetEquation(polynom);

                double[] result = alg.ExecuteDifferential();

                Console.WriteLine("Solutia {0} : {1} + i*{2}", i, Math.Round(result[0], 4), Math.Round(result[1], 4));
                TextToReturn += string.Format("Solutia {0} : {1} + i*{2}\r\n", i, Math.Round(result[0], 4), Math.Round(result[1], 4));
                if (i < numberOfRoots - 1)
                {
                    polynom = polynom.SimplifyEquation(
                      new Complex(Math.Round(result[0], 5), Math.Round(result[1], 5))
                  );
                }
            }

            return TextToReturn;
        }
        private Polynom ReadPolynom()
        {
            // Hardcoded till we make the interface
            //double[] coeffs = { 1, 0, /**/ 0, 0, /**/ 5, 0, /**/ 0, 0, /**/ 4, 0 };
            //const int degree = 4;

            //double[] coeffs = { 1, 0, /**/ 2, 0, /**/ 1, 0};
            //const int degree = 2;

            //double[] coeffs = { 1, 0, /**/ 15, 3, /**/ 83, 45, /**/ 195, 255, /**/ 104, 675,
            //                /**/ -330, 822, /**/ -548, 360, /**/ -240, 0 };
            //const int degree = 7;

            double[] coeffs = { 1, 0, /**/ 3, 3, /**/ 0, 9, /**/ -6, 6, /**/ -4, 0 };
            const int degree = 4;

            Console.WriteLine("Polinomul introdus:");
            Console.WriteLine("");

            Complex[] lista = new Complex[degree + 1];
            for (int i = 0; i <= degree; ++i)
            {
                lista[i] = new Complex(coeffs[2 * i], coeffs[2 * i + 1]);
            }

            Polynom polynom = new Polynom(lista);

            return polynom;
        }
        private Polynom ParsePolynom()
        {
            List<double> coeffs = new List<double>();
            char[] separator = { ',' };
            string[] stringArray = PolynomCoeffs.Split(separator);

            for (int i = 0; i < stringArray.Length; ++i)
            {
                coeffs.Add(double.Parse(stringArray[i]));
            }

            int degree = coeffs.Count / 2 - 1;

            Console.WriteLine("Polinomul introdus:");
            Console.WriteLine("");

            Complex[] lista = new Complex[degree + 1];
            for (int i = 0; i <= degree; ++i)
            {
                Console.Write("({1} + {2} * i) * x^{3}:");
                if(i != degree -1)
                {
                    Console.Write(" + ");
                }
                lista[i] = new Complex(coeffs[2 * i], coeffs[2 * i + 1]);
            }

            Polynom polynom = new Polynom(lista);

            return polynom;
        }
    }
}
