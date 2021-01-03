using System;
using System.Collections.Generic;
using System.Text;

namespace EvolutieDiferentialaIA
{
    public interface Equation
    {
        Complex ValueInPoint(Complex x);
    }
}
