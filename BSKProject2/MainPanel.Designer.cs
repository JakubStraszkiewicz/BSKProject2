namespace BSKProject2
{
    partial class MainPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PESEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwisko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data_urodzenia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nr_telefonu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kod_pocztowy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateButton = new System.Windows.Forms.Button();
            this.insertButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Pacjenci",
            "Karty pacjentow",
            "Lekarze",
            "Wizyty",
            "Diagnozy",
            "Recepty",
            "Leki"});
            this.comboBox1.Location = new System.Drawing.Point(9, 10);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridCheckbox,
            this.PESEL,
            this.Imie,
            this.Nazwisko,
            this.Data_urodzenia,
            this.Nr_telefonu,
            this.Email,
            this.Kod_pocztowy});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dataGridView1.Location = new System.Drawing.Point(9, 45);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(764, 348);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // dataGridCheckbox
            // 
            this.dataGridCheckbox.HeaderText = "";
            this.dataGridCheckbox.Name = "dataGridCheckbox";
            this.dataGridCheckbox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridCheckbox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridCheckbox.Width = 20;
            // 
            // PESEL
            // 
            this.PESEL.HeaderText = "PESEL";
            this.PESEL.Name = "PESEL";
            // 
            // Imie
            // 
            this.Imie.HeaderText = "Imię";
            this.Imie.Name = "Imie";
            // 
            // Nazwisko
            // 
            this.Nazwisko.HeaderText = "Nazwisko";
            this.Nazwisko.Name = "Nazwisko";
            // 
            // Data_urodzenia
            // 
            this.Data_urodzenia.HeaderText = "Data urodzenia";
            this.Data_urodzenia.Name = "Data_urodzenia";
            // 
            // Nr_telefonu
            // 
            this.Nr_telefonu.HeaderText = "Nr telefonu";
            this.Nr_telefonu.Name = "Nr_telefonu";
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            // 
            // Kod_pocztowy
            // 
            this.Kod_pocztowy.HeaderText = "Kod pocztowy";
            this.Kod_pocztowy.Name = "Kod_pocztowy";
            // 
            // updateButton
            // 
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.updateButton.Location = new System.Drawing.Point(562, 398);
            this.updateButton.Margin = new System.Windows.Forms.Padding(2);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(103, 31);
            this.updateButton.TabIndex = 4;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // insertButton
            // 
            this.insertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.insertButton.Location = new System.Drawing.Point(454, 398);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(103, 31);
            this.insertButton.TabIndex = 5;
            this.insertButton.Text = "Insert";
            this.insertButton.UseVisualStyleBackColor = true;
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteButton.Location = new System.Drawing.Point(669, 398);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(103, 31);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.insertButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainPanel";
            this.Text = "MainPanel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainPanel_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridCheckbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn PESEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwisko;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data_urodzenia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nr_telefonu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kod_pocztowy;
    }
}