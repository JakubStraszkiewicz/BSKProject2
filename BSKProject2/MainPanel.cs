using System;
using System.Collections;
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
    struct RowEditedListType
    {
        public int rowIndex;
        public ArrayList columnIndexes;
    }

    public partial class MainPanel : Form
    {
        //// Pola stale
        string tempCellValue = "/0";
        Color rowsEditedColor;
        Color rowsAddedColor;

        //// Pola zmienne
        private Form1 mainForm;

        string temp_editedCellValue;

        List<RowEditedListType> rowsEdited;
        List<int> rowsAdded;

        public MainPanel(Form1 form)
        {
            InitializeComponent();
            mainForm = form;

            rowsAddedColor = new Color();
            rowsAddedColor = Color.Green;
            rowsEditedColor = new Color();
            rowsEditedColor = Color.Yellow;

            rowsEdited = new List<RowEditedListType>();
            rowsAdded = new List<int>();

            ////
            string[] data1 = { "987456123", "Jan", "Kowalski", "1999.01.07", "547896213", "jan@wp.pl", "54-778" };
            string[] data2 = { "213443543", "Ewa", "Nowak", "1984.03.25", "786046477", "ewa@gmail.com", "63-018" };
            string[] data3 = { "875675676", "Adam", "Wisniewski", "1968.11.30", "688748209", "adam@op.pl", "24-358" };
            string[] data4 = { "765857645", "Maria", "Nowicka", "1990.06.15", "786138445", "maria@vp.pl", "87-735" };
            string[] data5 = { "487477967", "Mateusz", "Kaczmarek", "1956.08.05", "845314065", "mateusz@wp.pl", "35-578" };
            dataGridView1.Rows.Add(data1);
            dataGridView1.Rows.Add(data2);
            dataGridView1.Rows.Add(data3);
            dataGridView1.Rows.Add(data4);
            dataGridView1.Rows.Add(data5);
        }

        private void MainPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainForm.Close();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.temp_editedCellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (NullReferenceException ex)
            {
                this.temp_editedCellValue = this.tempCellValue;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.temp_editedCellValue.Equals(
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                return; // nie wykonano zmian w komorce

            foreach (int rowIndex in this.rowsAdded)
            {
                if (rowIndex == e.RowIndex)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = this.rowsAddedColor;
                    return;  //edytowano swiezo dodany wiersz
                }
            }

            foreach (RowEditedListType row in this.rowsEdited)
            {
                if (row.rowIndex == e.RowIndex)  //edytowano juz ten wiersz
                {
                    foreach (int column in row.columnIndexes)
                    {
                        if (column == e.RowIndex)
                        {
                            return;  //edytowano juz ta komorke
                        }
                    }
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = this.rowsEditedColor;
                    row.columnIndexes.Add(e.ColumnIndex);  //dodajemy nowy indeks kolumny      
                    return;
                }
            }
            RowEditedListType newRowEdited;
            newRowEdited.rowIndex = e.RowIndex;
            newRowEdited.columnIndexes = new ArrayList();
            newRowEdited.columnIndexes.Add(e.ColumnIndex);

            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = this.rowsEditedColor;
            rowsEdited.Add(newRowEdited);   //dodajemy nowy wiersz

            /*
            string cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            editTextBox.Text = cell;
             */
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex == dataGridView1.NewRowIndex)
            {
                this.rowsAdded.Add(e.RowIndex-1); //wykryto nowy wiersz, ALE nie ten co modyfikujemy, a nastepny zaraz za nim!
            }
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            //Zaladowanie do bazy nowych wierszy
            //Nalezy wykorzystac rowsAdded
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //Zaladowanie do bazy zuaktualizowane wiersze
            //Nalezy wykorzystac rowsEdited
        }

    }
}
