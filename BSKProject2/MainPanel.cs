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
    public partial class MainPanel : Form
    {
        //// Pola stale
        const string tempCellValue = "\0";
        Color rowsEditedColor = Color.Yellow;
        Color rowsAddedColor = Color.Green;

        //// Pola zmienne
        private Form1 mainForm;

        string beforeEditCellValue;

        List<RowEditedListType> rowsEdited;
        List<int> rowsAdded;
        List<int> rowsSelected;

        public MainPanel(Form1 form)
        {
            InitializeComponent();
            mainForm = form;

            rowsEdited = new List<RowEditedListType>();
            rowsAdded = new List<int>();
            rowsSelected = new List<int>();

            object[] data1 = { false, "987456123", "Jan", "Kowalski", "1999.01.07", "547896213", "jan_kowalski@wp.pl", "54-778" };
            object[] data2 = { false, "213443543", "Ewa", "Nowak", "1984.03.25", "786046477", "ewa.nowak@gmail.com", "63-018" };
            object[] data3 = { false, "875675676", "Bartosz", "Adamski", "1968.11.30", "688748209", "b.adamski@op.pl", "24-358" };
            object[] data4 = { false, "765857645", "Joanna", "Nowicka", "1990.06.15", "786138445", "joanow@vp.pl", "87-735" };
            object[] data5 = { false, "487477967", "Mateusz", "Kaczmarek", "1956.08.05", "845314065", "thelegend27@wp.pl", "35-578" };

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
            if (e.ColumnIndex == 0) //edycja checkboxa
                return;

            //komorki w nowopowstalym wierszu posiadaja wartosc null!
            try
            {
                this.beforeEditCellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (NullReferenceException ex)
            {
                this.beforeEditCellValue = tempCellValue; //nadajemy im sztucznie wartosc;
            }
        }

        private bool cellEndEdit_isCheckBoxEdited(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) //edycja checkboxa
            {
                if ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value == true)
                    this.rowsSelected.Add(e.RowIndex);
                else
                    this.rowsSelected.Remove(e.RowIndex);

                return true;
            }
            return false;
        }

        private bool cellEndEdit_isAddedRowEdited(DataGridViewCellEventArgs e)
        {
            foreach (int rowIndex in this.rowsAdded)
            {
                if (rowIndex == e.RowIndex)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = this.rowsAddedColor;
                    return true;  //edytowano swiezo dodany wiersz
                }
            }
            return false;
        }

        private bool cellEndEdit_isEditedRowEdited(DataGridViewCellEventArgs e)
        {
            foreach (RowEditedListType row in this.rowsEdited)
            {
                if (row.rowIndex == e.RowIndex) //edytowano juz ten wiersz
                {
                    foreach (int columnIndex in row.columnIndexes)
                    {
                        if (columnIndex == e.RowIndex)
                            return true; //edytowano juz ta komorke
                    }
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = this.rowsEditedColor;
                    row.columnIndexes.Add(e.ColumnIndex); //dodajemy nowy indeks kolumny      
                    return true; //edytowano nowa komorke w wierszu
                }
            }
            return false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cellEndEdit_isCheckBoxEdited(e))
                return;

            string endEditCellValue;
            try
            {
                endEditCellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (NullReferenceException ex)
            {
                endEditCellValue = tempCellValue;
            }
            if (this.beforeEditCellValue.Equals(endEditCellValue))
                return; //nie wykonano zmian w komorce

            if (cellEndEdit_isAddedRowEdited(e))
                return;

            if (cellEndEdit_isEditedRowEdited(e))
                return;
            
            RowEditedListType newRowEdited = new RowEditedListType();
            newRowEdited.rowIndex = e.RowIndex;
            newRowEdited.columnIndexes.Add(e.ColumnIndex);

            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = this.rowsEditedColor;
            rowsEdited.Add(newRowEdited); //dodajemy nowy wiersz
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex == dataGridView1.NewRowIndex)
                this.rowsAdded.Add(e.RowIndex - 1); //wykryto nowy wiersz, ALE nie ten co modyfikujemy, a nastepny zaraz za nim!
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            //Zaladowanie do bazy nowych wierszy
            //Nalezy wykorzystac rowsAdded
            foreach (int rowIndex in rowsAdded)
            {
                int numberOfCells = dataGridView1.Rows[rowIndex].Cells.Count;
                for (int j = 0; j < numberOfCells; j++)
                    dataGridView1.Rows[rowIndex].Cells[j].Style.BackColor = DefaultBackColor;
            }

            rowsAdded.Clear();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //Zaladowanie do bazy zuaktualizowane wiersze
            //Nalezy wykorzystac rowsEdited
            foreach (RowEditedListType row in rowsEdited)
            {
                foreach (int columnIndex in row.columnIndexes)
                    dataGridView1.Rows[row.rowIndex].Cells[columnIndex].Style.BackColor = DefaultBackColor;
            }

            rowsEdited.Clear();
        }

        private void deleteButton_handleAddedRowRemove(List<DataGridViewRow> rowsToRemoveList)
        {
            foreach (DataGridViewRow rowToRemove in rowsToRemoveList.ToList())
            {
                foreach (int addedRowIndex in rowsAdded.ToList())
                {
                    if (addedRowIndex == rowToRemove.Index) //Usuniecie dodanego wiersza
                    {
                        rowsAdded.Remove(addedRowIndex);
                        for (int i = 0; i < rowsAdded.Count; i++)
                        {
                            if (rowsAdded[i] > addedRowIndex)
                                rowsAdded[i]--;
                        }
                        dataGridView1.Rows.Remove(rowToRemove);
                        rowsToRemoveList.Remove(rowToRemove);
                        break;
                    }
                }
            }
        }

        private void deleteButton_handleEditedRowRemove(List<DataGridViewRow> rowsToRemoveList)
        {
            foreach (DataGridViewRow rowToRemove in rowsToRemoveList.ToList())
            {
                foreach (RowEditedListType editedRow in rowsEdited.ToList())
                {
                    if (editedRow.rowIndex == rowToRemove.Index) //Usuniecie edytowanego wiersza
                    {
                        rowsEdited.Remove(editedRow);
                        for (int i = 0; i < rowsEdited.Count; i++)
                        {
                            if (rowsEdited[i].rowIndex > editedRow.rowIndex)
                                rowsEdited[i].decrementIndex();
                        }
                        break;
                    }
                }

                dataGridView1.Rows.Remove(rowToRemove);
                rowsToRemoveList.Remove(rowToRemove);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //Usuniecie z bazy wybrane wiersze
            //Nalezy wykorzystac rowsSelected
            List<DataGridViewRow> rowsToRemoveList = new List<DataGridViewRow>();
            foreach (int selectedRowIndex in rowsSelected)
                rowsToRemoveList.Add(dataGridView1.Rows[selectedRowIndex]);

            deleteButton_handleAddedRowRemove(rowsToRemoveList);
            deleteButton_handleEditedRowRemove(rowsToRemoveList);

            rowsSelected.Clear();
        }
    }

    /// <summary>
    /// Klasa dla listy, ktora rejestrowalaby edycje komorek poszczegolnych wierszy.
    /// </summary>
    class RowEditedListType
    {
        public int rowIndex;
        public List<int> columnIndexes;

        public RowEditedListType()
        {
            columnIndexes = new List<int>();
        }

        public void decrementIndex()
        {
            this.rowIndex--;
        }
    }
}
