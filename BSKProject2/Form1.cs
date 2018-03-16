using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSKProject2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void zalogujButton_Click(object sender, EventArgs e)
        {
            MainPanel mainPanel = new MainPanel(this);
            mainPanel.Show();
            this.Hide();
        }
    }
}
