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
        public Form1()
        {
            InitializeComponent();
            client = Client.GetInstance();
        }

        private void ResolveButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = client.Resolve();
        }
    }
}
