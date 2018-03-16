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
    public partial class MainPanel : Form
    {
        private Form1 mainForm;

        public MainPanel(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
            string[] data = { "987456123", "Jan", "Kowalski", "1999.01.05", "547896213", "jan@wp.pl", "54-778" };
            dataGridView1.Rows.Add(data);
        }

        private void MainPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainForm.Close();

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            editTextBox.Text = cell;
            Color colorRed = new Color();

            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }
    }
}
