using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvolutieDiferentialaIA.Implementations;

namespace InterfataProiect
{
    public partial class Form1 : Form
    {
        Client client;
        List<TextBox> _real = new List<TextBox>();
        List<TextBox> _imag = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();
            client = Client.GetInstance();
        }

        private void show_Click(object sender, EventArgs e)
        {
            double[] coeffs = new double[2 * _imag.Count];
            //double[] coeffs = { 1, 0, /**/ 3, 3, /**/ 0, 9, /**/ -6, 6, /**/ -4, 0 };
            int k = 0;
            textBox1.ResetText();

            try
            {
                for (int i = 0; i < _imag.Count; ++i)
                {
                    if (_real[i].Text == "")
                        _real[i].Text = "0";
                    if (_imag[i].Text == "")
                        _imag[i].Text = "0";

                    coeffs[k++] = Convert.ToDouble(_real[i].Text);
                    coeffs[k++] = Convert.ToDouble(_imag[i].Text);

                    //textBox1.AppendText($"{ Convert.ToDouble(_real[i].Text)} {Convert.ToDouble(_imag[i].Text)} i \\");
                }
                textBox1.Text = client.Resolve(coeffs, _imag.Count - 1);
            }
            catch (Exception ee)
            {
                textBox1.Text = ee.Message;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.panelEcuation.Controls.Clear();
                _real.Clear();
                _imag.Clear();
                if (e.KeyChar == (char)Keys.Return)
                {
                    const int textBoxWidth = 30;  // control variables for TextBox placement
                    const int textBoxHeight = 50;
                    const int textBoxMargin = 5;

                    const int labelWidth = 9;
                    const int labelHeight = 50;
                    const int labelMargin = 5;

                    const int distance = 9*labelWidth + 2*textBoxWidth;
                    int grad = Convert.ToInt32(textBoxGrad.Text);

                    for (int i = grad; i >=0; i--)
                    {
                        TextBox realBox = new TextBox();
                        TextBox imagBox = new TextBox();

                        Label braceO = new Label();
                        Label imaginary = new Label();
                        Label plus = new Label();
                        Label exponent = new Label();
                        Label unknow = new Label();
                        Label plus2 = new Label();


                        braceO.Text = "(";
                        imaginary.Text = "* i";
                        plus.Text = "+";
                        exponent.Text = $"{i}";
                        unknow.Text = ") * X";
                        plus2.Text = "+";

                        realBox.Top = 2 * textBoxMargin;
                        imagBox.Top = 2 * textBoxMargin;
                        braceO.Top = 2 * labelMargin;
                        imaginary.Top = 3 * labelMargin;
                        plus.Top = 2 * labelMargin;
                        exponent.Top = labelMargin;
                        unknow.Top = 2 * labelMargin;
                        plus2.Top = 2 * labelMargin;

                        realBox.Width = textBoxWidth;
                        imagBox.Width = textBoxWidth;
                        braceO.Width = labelWidth;
                        imaginary.Width = 2 * labelWidth;
                        plus.Width = labelWidth;
                        exponent.Width = labelWidth;
                        unknow.Width = 3 * labelWidth;
                        plus2.Width = labelWidth;

                        realBox.Height = textBoxHeight;
                        imagBox.Height = textBoxHeight;
                        braceO.Height = labelHeight;
                        imaginary.Height = 3*labelHeight;
                        plus.Height = labelHeight;
                        exponent.Height = labelHeight;
                        unknow.Height = labelHeight;
                        plus2.Height = 3*labelHeight;

                        braceO.Left = labelMargin + (grad-i) * distance;
                        realBox.Left = braceO.Left + labelWidth;
                        plus.Left = realBox.Left + textBoxWidth;
                        imagBox.Left = plus.Left + labelWidth;
                        imaginary.Left = imagBox.Left + textBoxWidth;
                        unknow.Left = imaginary.Left + 2 * labelWidth;
                        exponent.Left = unknow.Left + Convert.ToInt32(3 * labelWidth);
                        plus2.Left = exponent.Left + labelWidth;

                        _real.Add(realBox);
                        _imag.Add(imagBox);

                        this.panelEcuation.Controls.Add(braceO);
                        //this.Controls.Add(braceO);
                        this.panelEcuation.Controls.Add(realBox);
                        this.panelEcuation.Controls.Add(plus);
                        this.panelEcuation.Controls.Add(imagBox);
                        this.panelEcuation.Controls.Add(imaginary);
                        this.panelEcuation.Controls.Add(unknow);
                        this.panelEcuation.Controls.Add(exponent);
                        if (i == 0)
                        {
                            plus2.Text = " = 0";
                            plus2.Width = 4 * labelWidth;
                        }
                        this.panelEcuation.Controls.Add(plus2);
                    }
                }
            }
            catch (Exception ee)
            {
                textBox1.Text = ee.Message;
            }
        }
    }
}
