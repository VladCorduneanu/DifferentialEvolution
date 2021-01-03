using System;
using System.Collections.Generic;
using System.Text;

namespace EvolutieDiferentialaIA
{
    public class Complex
    {
        public double Re { get; private set; }
        public double Im { get; private set; }

        public Complex(double re, double im)
        {
            Re = re;
            Im = im;
        }
        public Complex(double re)
        {
            Re = re;
            Im = 0;
        }
        static public bool operator ==(Complex a, Complex b) => a.Re == b.Re && a.Im == b.Im;
        static public bool operator !=(Complex a, Complex b) => a.Re != b.Re || a.Im != b.Im;
        static public Complex operator +(Complex a, Complex b) => new Complex(a.Re + b.Re, a.Im + b.Im);
        static public Complex operator +(Complex a, double b) => new Complex(a.Re + b, a.Im);
        static public Complex operator -(Complex a, Complex b) => new Complex(a.Re - b.Re, a.Im - b.Im);
        static public Complex operator -(Complex a, double b) => new Complex(a.Re - b, a.Im);
        static public Complex operator -(Complex a) => new Complex(-a.Re, -a.Im);
        static public Complex operator !(Complex a) => new Complex(a.Re, -a.Im);
        static public Complex operator *(Complex a, Complex b) => new Complex(a.Re * b.Re - a.Im * b.Im, a.Re * b.Im + a.Im * b.Re);
        static public Complex operator *(Complex a, double f) => new Complex(a.Re * f, a.Im * f);
        static public Complex operator /(Complex a, Complex b) => a * (!b) * (1.0f / (b.Re * b.Re + b.Im * b.Im));
        static public Complex operator /(Complex a, double f) => a * (1.0f / f);
        static public Complex operator ^(Complex a, int pow)
        {
            if (pow == 0)
                return new Complex(1, 0);
            if (pow > 0)
                return a * (a ^ (pow - 1));
            return (a ^ (pow + 1)) / a;
        }
        public double Module() => Math.Sqrt(Re * Re + Im * Im);

        public override bool Equals(object obj)
        {
            var complex = obj as Complex;
            return complex != null &&
                   Re == complex.Re &&
                   Im == complex.Im;
        }

        public override int GetHashCode()
        {
            var hashCode = 29377563;
            hashCode = hashCode * -1521134295 + Re.GetHashCode();
            hashCode = hashCode * -1521134295 + Im.GetHashCode();
            return hashCode;
        }
    }
}
