using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using simulation;

namespace DS_Sem3_Letisko
{
    public partial class Form1 : Form
    {
        private MySimulation Simulation;

        public Form1()
        {
            InitializeComponent();

            Simulation = new MySimulation();
        }
    }
}
