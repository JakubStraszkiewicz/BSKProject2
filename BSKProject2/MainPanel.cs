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
using System.Data.SqlClient;

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
            
            SqlConnection sqlConnection = new SqlConnection(
                "Data Source=SUNNIVRANDELL;" +
                "Initial Catalog=BSKproj2;" +
                "Trusted_Connection=yes;");
            try
            {
                sqlConnection.Open();

                SqlDataReader sqlReader = null;
                string command = "SELECT Tabele.nazwa"
                    + " FROM _Role, Role_Tabele, Tabele"
                    + " WHERE _Role.nazwa='" + this.mainForm.chosenProfile + "'"
                        + " AND Role_Tabele.selects='true'"
                        + " AND Role_Tabele.FK_Rola=_Role.Id_roli"
                        + " AND Role_Tabele.FK_Tabela=Tabele.Id_tabeli;";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlReader = sqlCommand.ExecuteReader();

                while(sqlReader.Read())
                {
                    this.comboBox1.Items.Add(sqlReader[0]);
                }
                sqlCommand.Dispose();
                sqlReader.Close();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
            if (e.ColumnIndex == 1)
                newRowEdited.primaryKey = beforeEditCellValue;
            else
                newRowEdited.primaryKey = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            newRowEdited.primaryKeyColumnName = dataGridView1.Columns[1].Name;
            newRowEdited.rowIndex = e.RowIndex;
            newRowEdited.columnIndexes.Add(e.ColumnIndex);

            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = this.rowsEditedColor;
            rowsEdited.Add(newRowEdited); //dodajemy nowy wiersz
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex == dataGridView1.NewRowIndex && e.RowIndex != 0)
                this.rowsAdded.Add(e.RowIndex - 1); //wykryto nowy wiersz, ALE nie ten co modyfikujemy, a nastepny zaraz za nim!
        }

        private List<String> listOfColumnTypes()
        {
            List<String> listOfTypes = new List<String>();
            SqlConnection sqlConnection = new SqlConnection(
                    "Data Source=SUNNIVRANDELL;" +
                    "Initial Catalog=BSKproj2;" +
                    "Trusted_Connection=yes;");
            try
            {
                sqlConnection.Open();

                SqlDataReader sqlReader = null;
                string command = "SELECT *"
                    + " FROM " + this.comboBox1.SelectedItem.ToString();
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlReader = sqlCommand.ExecuteReader();

                int numberOfColumns = sqlReader.FieldCount;
                for (int i = 0; i < numberOfColumns; i++)
                {
                    listOfTypes.Add(sqlReader.GetDataTypeName(i));
                }
                sqlCommand.Dispose();
                sqlReader.Close();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listOfTypes;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            //Zaladowanie do bazy nowych wierszy
            //Nalezy wykorzystac rowsAdded
            List<String> listOfTypes = listOfColumnTypes();

            foreach (int rowIndex in rowsAdded)
            {
                string insertCommand = "INSERT INTO " + this.comboBox1.SelectedItem.ToString() + " VALUES(";
                int numberOfCells = dataGridView1.Rows[rowIndex].Cells.Count;
                for (int j = 1; j < numberOfCells; j++)
                {
                    dataGridView1.Rows[rowIndex].Cells[j].Style.BackColor = DefaultBackColor;
                    if (j == 1)
                        if (listOfTypes[j - 1] == "int")
                            insertCommand += dataGridView1.Rows[rowIndex].Cells[j].Value.ToString();
                        else
                            insertCommand += "'" + dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() + "'";
                    else
                        if (listOfTypes[j - 1] == "int")
                            insertCommand += "," + dataGridView1.Rows[rowIndex].Cells[j].Value.ToString();
                        else
                            insertCommand += ",'" + dataGridView1.Rows[rowIndex].Cells[j].Value.ToString() + "'";   
                }
                insertCommand += ");";

                SqlConnection sqlConnection = new SqlConnection(
                    "Data Source=SUNNIVRANDELL;" +
                    "Initial Catalog=BSKproj2;" +
                    "Trusted_Connection=yes;");
                try
                {
                    sqlConnection.Open();

                    SqlDataReader sqlReader = null;
                    string command = insertCommand;
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    sqlReader = sqlCommand.ExecuteReader();

                    sqlCommand.Dispose();
                    sqlReader.Close();

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            rowsAdded.Clear();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //Zaladowanie do bazy zuaktualizowane wiersze
            //Nalezy wykorzystac rowsEdited
            List<String> listOfTypes = listOfColumnTypes();

            foreach (RowEditedListType row in rowsEdited)
            {
                string updateCommand = "UPDATE " + this.comboBox1.SelectedItem.ToString() + " SET";
                bool firstParam = true;
                foreach (int columnIndex in row.columnIndexes)
                {
                    dataGridView1.Rows[row.rowIndex].Cells[columnIndex].Style.BackColor = DefaultBackColor;
                    if(firstParam==true)
                    {
                        if(listOfTypes[columnIndex-1]=="int")
                            updateCommand += " " + dataGridView1.Columns[columnIndex].Name + "="
                                + dataGridView1.Rows[row.rowIndex].Cells[columnIndex].Value.ToString();
                        else
                            updateCommand += " " + dataGridView1.Columns[columnIndex].Name + "="
                                +"'"+dataGridView1.Rows[row.rowIndex].Cells[columnIndex].Value.ToString()+"'";
                        
                        firstParam=false;
                    }
                    else
                        if (listOfTypes[columnIndex - 1] == "int")
                            updateCommand += ", " + dataGridView1.Columns[columnIndex].Name + "="
                                + dataGridView1.Rows[row.rowIndex].Cells[columnIndex].Value.ToString();
                        else
                            updateCommand += ", " + dataGridView1.Columns[columnIndex].Name + "="
                                + "'" + dataGridView1.Rows[row.rowIndex].Cells[columnIndex].Value.ToString() + "'";
                }
                updateCommand += " WHERE " + row.primaryKeyColumnName+"=";
                if (listOfTypes[0] == "int")
                    updateCommand += row.primaryKey;
                else
                    updateCommand += "'"+row.primaryKey+"'";

                SqlConnection sqlConnection = new SqlConnection(
                    "Data Source=SUNNIVRANDELL;" +
                    "Initial Catalog=BSKproj2;" +
                    "Trusted_Connection=yes;");
                try
                {
                    sqlConnection.Open();

                    SqlDataReader sqlReader = null;
                    string command = updateCommand;
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    sqlReader = sqlCommand.ExecuteReader();

                    sqlCommand.Dispose();
                    sqlReader.Close();

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
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
                string deleteCommandCondition = dataGridView1.Columns[1].Name+"="+rowToRemove.Cells[1].Value.ToString();
                foreach (RowEditedListType editedRow in rowsEdited.ToList())
                {
                    if (editedRow.rowIndex == rowToRemove.Index) //Usuniecie edytowanego wiersza
                    {
                        deleteCommandCondition = dataGridView1.Columns[1].Name + "=" + editedRow.primaryKey;
                        rowsEdited.Remove(editedRow);
                        for (int i = 0; i < rowsEdited.Count; i++)
                        {
                            if (rowsEdited[i].rowIndex > editedRow.rowIndex)
                                rowsEdited[i].decrementIndex();
                        }
                        break;
                    }
                }

                SqlConnection sqlConnection = new SqlConnection(
                    "Data Source=SUNNIVRANDELL;" +
                    "Initial Catalog=BSKproj2;" +
                    "Trusted_Connection=yes;");
                try
                {
                    sqlConnection.Open();

                    SqlDataReader sqlReader = null;
                    string command = "DELETE FROM "+this.comboBox1.SelectedItem.ToString()
                        +" WHERE "+deleteCommandCondition;
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    sqlReader = sqlCommand.ExecuteReader();

                    sqlCommand.Dispose();
                    sqlReader.Close();

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rowsAdded.Clear();
            this.rowsEdited.Clear();
            this.rowsSelected.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn());
            this.dataGridView1.Columns[0].Width = 20;

            SqlConnection sqlConnection = new SqlConnection(
                "Data Source=SUNNIVRANDELL;" +
                "Initial Catalog=BSKproj2;" +
                "Trusted_Connection=yes;");
            try
            {
                sqlConnection.Open();

                SqlDataReader sqlReader = null;
                string command = "SELECT *"
                    + " FROM " + this.comboBox1.SelectedItem.ToString();
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlReader = sqlCommand.ExecuteReader();
                
                int numberOfColumns = sqlReader.FieldCount;
                for(int i=0; i<numberOfColumns; i++)
                {
                    this.dataGridView1.Columns.Add(sqlReader.GetName(i),sqlReader.GetName(i));
                }
                while (sqlReader.Read())
                {
                    object[] data = new object[numberOfColumns+1];
                    data[0] = false;
                    for(int i=0; i<numberOfColumns; i++)
                    {
                        data[i+1]= sqlReader[i];
                    }
                    dataGridView1.Rows.Add(data);
                }
                sqlCommand.Dispose();
                sqlReader.Close();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    /// <summary>
    /// Klasa dla listy, ktora rejestrowalaby edycje komorek poszczegolnych wierszy.
    /// </summary>
    class RowEditedListType
    {
        public object primaryKey;
        public string primaryKeyColumnName;
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
