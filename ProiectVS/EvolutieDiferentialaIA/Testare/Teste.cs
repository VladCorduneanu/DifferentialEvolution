using NUnit.Framework;
using EvolutieDiferentialaIA;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Adunare()
        {
            Complex a = new Complex(1, 1);
            Complex b = new Complex(2, 3);

            Complex res = new Complex(3, 4);
            Complex actual_res = a + b;

            Assert.IsTrue(actual_res == res);
        }

        [Test]
        public void Scadere()
        {
            Complex a = new Complex(1, 1);
            Complex b = new Complex(2, 3);

            Complex res = new Complex(-1, -2);
            Complex actual_res = a - b;

            Assert.IsTrue(actual_res == res);
        }

        [Test]
        public void Inmultire()
        {
            Complex a = new Complex(1, 1);
            Complex b = new Complex(2, 3);

            Complex res = new Complex(-1, 5);
            Complex actual_res = a * b;

            Assert.IsTrue(actual_res == res);
        }

        [Test]
        public void Impartire()
        {
            Complex a = new Complex(-1, 5);
            Complex b = new Complex(1, 1);

            Complex res = new Complex(2, 3);
            Complex actual_res = a / b;

            Assert.IsTrue(actual_res == res);
        }

        [Test]
        public void Conjugare()
        {
            Complex a = new Complex(-1, 5);

            Complex res = new Complex(-1, -5);
            Complex actual_res = !a;

            Assert.IsTrue(actual_res == res);
        }

        [Test]
        public void Modul()
        {
            Complex a = new Complex(3, 4);

            double res = 5;
            double actual_res = a.Module();

            Assert.IsTrue(actual_res == res);
        }

        [Test]
        public void Putere()
        {
            Complex a = new Complex(3, 4);

            Complex res = a ^ 4;
            Complex actual_res = a * a * a * a;

            Assert.IsTrue(actual_res == res);
        }

        [Test]
        public void ImpartirePolinomiala()
        {
            Complex[] lista = new Complex[5];
            lista[0] = new Complex(1);
            lista[1] = new Complex(0);
            lista[2] = new Complex(-5);
            lista[3] = new Complex(0);
            lista[4] = new Complex(4);

            Polynom ecuatie = new Polynom(lista);

            Polynom result = ecuatie.SimplifyEquation(new Complex(-1));

            bool res = true;

            res = res && result.Factors[0] == new Complex(1);
            res = res && result.Factors[1] == new Complex(-1);
            res = res && result.Factors[2] == new Complex(-4);
            res = res && result.Factors[3] == new Complex(4);

            Assert.IsTrue(res);
        }

        [Test]
        public void ImpartirePolinomialaImaginara()
        {
            Complex[] lista = new Complex[5];
            lista[0] = new Complex(1);
            lista[1] = new Complex(0);
            lista[2] = new Complex(5);
            lista[3] = new Complex(0);
            lista[4] = new Complex(5);

            Polynom ecuatie = new Polynom(lista);

            Polynom result = ecuatie.SimplifyEquation(new Complex(0, -1));

            bool res = true;

            res = res && result.Factors[0] == new Complex(1);
            res = res && result.Factors[1] == new Complex(0, -1);
            res = res && result.Factors[2] == new Complex(4);
            res = res && result.Factors[3] == new Complex(0, -4);

            Assert.IsTrue(res);
        }

        [Test]
        public void CalculEroare()
        {
            double[] coeffs = { 1, 0, /**/ 3, 3, /**/ 0, 9, /**/ -6, 6, /**/ -4, 0 };
            const int degree = 4;

            Complex[] lista = new Complex[degree + 1];
            for (int i = 0; i <= degree; ++i)
            {
                lista[i] = new Complex(coeffs[2 * i], coeffs[2 * i + 1]);
            }

            Polynom polynom = new Polynom(lista);

            Complex result = polynom.ValueInPoint(new Complex(-1));

            Assert.IsTrue(result == new Complex(0));
        }
    }
}