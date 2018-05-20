﻿using System;
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
    public partial class Form1 : Form
    {
        public string chosenProfile;
        public string userLogin;
        public string userPassword;

        public Form1()
        {
            InitializeComponent();
        }

        private void zalogujButton_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(
                "Data Source=SUNNIVRANDELL;" +
                "Initial Catalog=BSKproj2;" +
                "Trusted_Connection=yes;");
            try
            {
                sqlConnection.Open();

                SqlDataReader sqlReader = null;
                string command = "SELECT *" 
                    +" FROM Uzytkownicy" 
                    +" WHERE _login='"+this.loginTextBox.Text+"'" 
                        +" AND haslo='"+this.hasloTextBox.Text+"'";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlReader = sqlCommand.ExecuteReader();

                if (!sqlReader.HasRows)
                {
                    this.loginTextBox.Text = "login lub haslo niepoprawne";
                    this.hasloTextBox.Text = "";
                    return;
                }

                sqlReader.Read();
                if(sqlReader[6].Equals("true"))
                {
                    this.loginTextBox.Text = "uzytkownik juz zalogowany";
                    this.hasloTextBox.Text = "";
                    return;
                }
                sqlCommand.Dispose();
                sqlReader.Close();

                command = "SELECT _Role.nazwa"
                    +" FROM Uzytkownicy, Uzytkownicy_Role, _Role"
                    +" WHERE Uzytkownicy._login='"+this.loginTextBox.Text+"'" 
                        +" AND Uzytkownicy_Role.FK_Uzytkownik=Uzytkownicy.Id_uzytkownika" 
                        +" AND Uzytkownicy_Role.FK_Rola=_Role.Id_roli";
                sqlCommand = new SqlCommand(command, sqlConnection);
                sqlReader = sqlCommand.ExecuteReader();

                while(sqlReader.Read())
                {
                    this.profilComboBox.Items.Add(sqlReader[0]);
                }
                sqlCommand.Dispose();
                sqlReader.Close();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            this.loginTextBox.Enabled = false;
            this.hasloTextBox.Enabled = false;
            this.zalogujButton.Enabled = false;
            this.profilComboBox.Enabled = true;
            this.wybierzButton.Enabled = true;
        }

        private void wybierzButton_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(
                "Data Source=SUNNIVRANDELL;" +
                "Initial Catalog=BSKproj2;" +
                "Trusted_Connection=yes;");
            try
            {
                sqlConnection.Open();

                SqlDataReader sqlReader = null;
                string command = "UPDATE Uzytkownicy" 
                    +" SET czy_zalogowany='true'" 
                    +" WHERE _login='"+this.loginTextBox.Text+"'" 
                        +" AND haslo='"+this.hasloTextBox.Text+"'";
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

            this.chosenProfile = this.profilComboBox.SelectedItem.ToString();
            this.userLogin = this.loginTextBox.Text;
            this.userPassword = this.hasloTextBox.Text;
            MainPanel mainPanel = new MainPanel(this);
            mainPanel.Show();
            this.Hide();
        }

        public void resetWindow()
        {
            this.loginTextBox.Enabled = true;
            this.hasloTextBox.Text = "";
            this.hasloTextBox.Enabled = true;
            this.zalogujButton.Enabled = true;

            this.profilComboBox.Items.Clear();
            this.profilComboBox.Text = "";
            this.profilComboBox.Enabled = false;
            this.wybierzButton.Enabled = false;
        }
    }
}
