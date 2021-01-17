using System;
using System.Collections.Generic;
using System.Text;

namespace EvolutieDiferentialaIA
{
    public class Polynom : Equation
    {
        public Complex[] Factors { get; private set; }
        public Polynom(Complex[] factors) => Factors = (Complex[])factors.Clone();
        internal int GetDegree() => Factors.Length - 1;
        public Complex ValueInPoint(Complex x)
        {
            Complex result = new Complex(0);

            for (int i = 0; i < Factors.Length; ++i)
            {
                result = result + Factors[i] * (x ^ (GetDegree() - i));
            }

            return result;
        }

        // The main function that does the algorithm of polynomial division with a found root
        // The factors are like this : a0 *x^n + a1*x^n-1 + ... + an * x^0
        // The relation on which the calculus is based is that t[i] = a[i] - t[i-1] with t[0] = a[0]
        // with {t} we have the coeffs of the new polynom
        // with {a} we have the coeffs of the intial polynom
        public Polynom SimplifyEquation(Complex root)
        {
            Polynom result = new Polynom(new Complex[GetDegree()]);
            result.Factors[0] = Factors[0];

            for (int i = 1; i < GetDegree(); ++i)
            {
                result.Factors[i] = Factors[i] + root * result.Factors[i - 1];
            }

            return result;
        }
    }
}
