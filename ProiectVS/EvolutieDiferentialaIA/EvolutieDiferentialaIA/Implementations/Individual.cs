using System;
using System.Collections.Generic;
using System.Text;

namespace EvolutieDiferentialaIA
{
    public class Individual
    {
        internal Equation m_Equation { get; private set;}

        internal double[] m_List { get; private set; }

        internal Individual(double[] vect,Equation eq)
        {
            m_List = (double[])vect.Clone();
            m_Equation = eq;
        }

        internal double GetError()
        {
            Complex variable = new Complex(m_List[0], m_List[1]);
            Complex error = m_Equation.ValueInPoint(variable);

            return error.Module();
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < m_List.Length; ++i)
            {
                s = s + " " + m_List[i].ToString();
            }

            return s;
        }

        public override bool Equals(object obj)
        {
            var individual = obj as Individual;
            return individual != null &&
                   EqualityComparer<Equation>.Default.Equals(m_Equation, individual.m_Equation) &&
                   EqualityComparer<double[]>.Default.Equals(m_List, individual.m_List);
        }

        public override int GetHashCode()
        {
            var hashCode = 1727153484;
            hashCode = hashCode * -1521134295 + EqualityComparer<Equation>.Default.GetHashCode(m_Equation);
            hashCode = hashCode * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(m_List);
            return hashCode;
        }

        static public bool operator ==(Individual a, Individual b)
        {
            if (a.m_List.Length != b.m_List.Length)
                return false;

            for (int i = 0; i < a.m_List.Length; ++i)
            {
                if (a.m_List[i] != b.m_List[i])
                    return false;
            }

            return true;
        }

        static public bool operator !=(Individual a, Individual b) => !(a == b);
    }
}
