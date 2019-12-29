using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SewingCompanyManagement
{
    public partial class EntranceForm : Form
    {
        private OleDbConnection myConnection = new OleDbConnection();
        public EntranceForm()
        {
            InitializeComponent();
            myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=TrueDB_01.mdb;User Id=admin;Password=;";
        }
        public EntranceType CurrentEntranceType
        {
            get;
            set;
        }

        //private void btnTechnolog_Click(object sender, EventArgs e)
        //{
        //    CurrentEntranceType = EntranceType.Technolog;
        //    Hide();
        //}

        //private void btnMaster_Click(object sender, EventArgs e)
        //{
        //    CurrentEntranceType = EntranceType.Master;
        //    Hide();
        //}

        //private void btnManager_Click(object sender, EventArgs e)
        //{
        //    CurrentEntranceType = EntranceType.Manager;
        //    Hide();
        //}

        private void btnExit_Click(object sender, EventArgs e)
        {
            CurrentEntranceType = EntranceType.Exit;
            Hide();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT LOGIN , PASSWORD, POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE_EN " +
                    "FROM POSITION_OF_EMPLOYEE LEFT JOIN EMPLOYEE ON POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE = EMPLOYEE.ID_POSITION_OF_EMPLOYEE " +
                    "WHERE LOGIN = '" + textBoxLogin.Text + "'AND PASSWORD = '"+ textBoxPassword.Text + "'";
                textBoxLogin.Clear();
                textBoxPassword.Clear();
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                
                int count = 0;
                string logPass=null;
                while (reader.Read())
                {
                    count++;
                    logPass = reader["NAME_POSITION_OF_EMPLOYEE_EN"].ToString();
                }
                //if we have one result
                if (count==1)
                {
                    if (logPass.CompareTo("Master")==0)
                    {
                        CurrentEntranceType = EntranceType.Master;
                        Hide();
                    }
                    if (logPass.CompareTo("Technolog") == 0)
                    {
                        CurrentEntranceType = EntranceType.Technolog;
                        Hide();
                    }
                    if (logPass.CompareTo("Manager") == 0)
                    {
                        CurrentEntranceType = EntranceType.Manager;
                        Hide();
                    }
                    if (logPass.CompareTo("Admin") == 0)
                    {
                        CurrentEntranceType = EntranceType.Admin;
                        Hide();
                    }

                }
                else if (count>1)
                {
                    MessageBox.Show("FALSE");
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
