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
            //myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\GDrveSpecowka\Develop\#HELEN PROJECTS\SewingCompany\SewingCompanyManagement\TrueDB_01.mdb;User Id=admin;Password=;";
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
                if (String.IsNullOrEmpty(textBoxLogin.Text)|| String.IsNullOrEmpty(textBoxPassword.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    string logPass = null;

                    logPass = DataBaseHelper.Entrance_getPositionOfEmployeeENG(textBoxLogin.Text, textBoxPassword.Text);
                    if (!String.IsNullOrEmpty(logPass))
                    {
                        if (logPass.CompareTo("Master") == 0)
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
                        //if (logPass.CompareTo("Admin") == 0)
                        //{
                        //    CurrentEntranceType = EntranceType.Admin;
                        //    Hide();
                        //}

                    }
                    else
                    {
                        // MessageBox.Show("FALSE");
                    }
                    textBoxLogin.Clear();
                    textBoxPassword.Clear();
                }
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
