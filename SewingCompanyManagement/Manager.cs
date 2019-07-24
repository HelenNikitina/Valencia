using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingCompanyManagement
{
    public partial class frmManager : Form
    {
        private OleDbConnection myConnection = new OleDbConnection();
        MyFunctions myFunction = new MyFunctions();
        public frmManager()
        {
            InitializeComponent();
            myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=TrueDB_01.mdb;User Id=admin;Password=;";
        }

        private void buttonViewOrder_Click(object sender, EventArgs e)
        {
            try
            {

                string query = "SELECT " +
                    "ID_ORDER as [Замовлення №], " +
                    "DATE_OF_ORDER as [Дата замовлення], " +
                    "NAME_OF_CUSTOMER as [Им'я замовника], " +
                    "[COMMENT] as [Коментар] "+
                    "FROM [ORDER] " ;
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewForManager.DataSource = dt;

                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewModel_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT " +
                    "[ID_MODEL] as [модель №], " +
                    "[SHORT_NAME_OF_MODEL] as [Назва моделі], " +
                    "[MODEL_DESCRIPTION] as [Опис моделі] " +
                    "FROM [MODEL] ";
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewForManager.DataSource = dt;

                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }

        }

        private void buttonViewEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT EMPLOYEE.ID_EMPLOYEE as [Табельний номер працівника], " +
                    "EMPLOYEE.NAME_EMPLOYEE as [ПІП працівника], " +
                    "EMPLOYEE.PHONE_EMPLOYEE as [Номер телефона працівника], " +
                    "POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE as [Посада працівника] " +
                    "FROM POSITION_OF_EMPLOYEE INNER JOIN EMPLOYEE ON POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE = EMPLOYEE.ID_POSITION_OF_EMPLOYEE; ";
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewForManager.DataSource = dt;

                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewPositionOfEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE as [Код посади], " +
                    "POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE as [Назва посади] " +
                    "FROM POSITION_OF_EMPLOYEE; ";
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewForManager.DataSource = dt;

                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }
      
        private void textBoxNumberModelsInOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void textBoxTelephoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);

        }

        private void buttonAddNewOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateTimePickerManager.Text)|| string.IsNullOrEmpty(textBoxNameOfCostumer.Text) || string.IsNullOrEmpty(textBoxComment.Text) )
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                //DateTime time = dateTimePickerManager.Value.Date;
                //MessageBox.Show( time.ToString());
                string customersName = textBoxNameOfCostumer.Text;
                string comment = textBoxComment.Text;
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "INSERT INTO [ORDER](DATE_OF_ORDER, NAME_OF_CUSTOMER, [COMMENT]) VALUES ('" + dateTimePickerManager.Text + "', '" + customersName + "', '" + comment + "' ); ";
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        myFunction.MessageDataSeved();
                        textBoxNameOfCostumer.Clear();
                        textBoxComment.Clear();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
        
        }

        private void buttonAddModelForOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxOrderNumber.Text)|| string.IsNullOrEmpty(comboBoxModelNumber.Text) || string.IsNullOrEmpty(comboBoxModelSize.Text) || string.IsNullOrEmpty(textBoxNumberModelsInOrder.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                int idOrder = int.Parse(comboBoxOrderNumber.Text + comboBoxModelNumber.Text + comboBoxModelSize.Text);
                int order = int.Parse(comboBoxOrderNumber.Text);
                int modelAndSize = int.Parse(comboBoxModelNumber.Text+ comboBoxModelSize.Text);
                int number = int.Parse(textBoxNumberModelsInOrder.Text);
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "INSERT INTO ORDER_MODEL VALUES (" + idOrder + ", '" + order + "', '" + modelAndSize + "', '" + number + "' ); ";
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        myFunction.MessageDataSeved();
                       
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
        }

        private void comboBoxOrderNumber_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT ORDER.ID_ORDER FROM [ORDER]";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxOrderNumber.Items.Clear();
                while (reader.Read())
                {
                    comboBoxOrderNumber.Items.Add(reader["ID_ORDER"].ToString());
                }
                comboBoxOrderNumber.Show();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelNumber_DropDown(object sender, EventArgs e)
        {
       
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "SELECT ID_MODEL FROM [MODEL]";
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    comboBoxModelNumber.Items.Clear();
                    while (reader.Read())
                    {
                    comboBoxModelNumber.Items.Add(reader["ID_MODEL"].ToString());
                    }
                    comboBoxModelNumber.Show();
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
        }

        private void comboBoxModelSize_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxModelNumber.Text))
            {
                myFunction.MessageChooseModel();
            }
            else
            {
                int model = int.Parse(comboBoxModelNumber.Text);
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "SELECT MODEL_AND_SIZE.ID_MODEL_SIZE_STATURE, MODEL_AND_SIZE.ID_MODEL " +
                        "FROM MODEL_AND_SIZE " +
                        "WHERE(((MODEL_AND_SIZE.ID_MODEL) = "+model+")); ";
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    comboBoxModelSize.Items.Clear();
                    while (reader.Read())
                    {
                        comboBoxModelSize.Items.Add(reader["ID_MODEL_SIZE_STATURE"].ToString());
                    }
                    comboBoxModelSize.Show();
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
        }

        private void comboBoxModelInOrder_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT ORDER.ID_ORDER FROM [ORDER]";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxModelInOrder.Items.Clear();
                while (reader.Read())
                {
                    comboBoxModelInOrder.Items.Add(reader["ID_ORDER"].ToString());
                }
                comboBoxModelInOrder.Show();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonListModelInOrder_Click(object sender, EventArgs e)
        {
            string query = null;
            string queryq = "SELECT " +
               // "ORDER_MODEL.ID_ORDER_LIST_MODEL, " +
                "ORDER_MODEL.ID_ORDER as [Номер замовлення], " +
                "MODEL_AND_SIZE.ID_MODEL as [Номер моделі], " +
                "MODEL.SHORT_NAME_OF_MODEL as [Назва моделі], " +
                "ORDER_MODEL.ID_MODEL_AND_SIZE as [Модель та розмір], " +
                "ORDER_MODEL.NUMBER_OF_MODELS as [Кількість моделей у замовленні] " +
                "FROM MODEL INNER JOIN(MODEL_AND_SIZE INNER JOIN ORDER_MODEL ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) ON MODEL.ID_MODEL = MODEL_AND_SIZE.ID_MODEL ";
            if (string.IsNullOrEmpty(comboBoxModelInOrder.Text))
            {
                query = queryq;
            }
            else
            {
                int order = int.Parse(comboBoxModelInOrder.Text);
                query = queryq + "WHERE(((ORDER_MODEL.ID_ORDER) = " + order + ")) ";
            }
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewForManager.DataSource = dt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddNewEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNameEmployee.Text)|| string.IsNullOrEmpty(textBoxTelephoneNumber.Text) || string.IsNullOrEmpty(comboBoxPositionOfEmployee.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                string name = textBoxNameEmployee.Text;
                int phone = int.Parse(textBoxTelephoneNumber.Text);
                int position = int.Parse(comboBoxPositionOfEmployee.Text);
                
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;

                    string query = "INSERT INTO [EMPLOYEE] (NAME_EMPLOYEE, PHONE_EMPLOYEE, ID_POSITION_OF_EMPLOYEE) VALUES ('" + name + "', " + phone + ","+ position + ") ";
                        
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        myFunction.MessageDataSeved();
                        textBoxNameEmployee.Clear();
                        textBoxTelephoneNumber.Clear();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
        }

        private void comboBoxPositionOfEmployee_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE " +
                    "FROM POSITION_OF_EMPLOYEE ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxPositionOfEmployee.Items.Clear();
                while (reader.Read())
                {
                    comboBoxPositionOfEmployee.Items.Add(reader["ID_POSITION_OF_EMPLOYEE"].ToString());
                }
                comboBoxPositionOfEmployee.Show();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        //Добавление новой должности
        private void buttonAddNewPosition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxIdPosition.Text)|| string.IsNullOrEmpty(textBoxNamePosition.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                string namePositin = textBoxNamePosition.Text;
                int idPosition = int.Parse(textBoxIdPosition.Text);

                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;

                    string query = "INSERT INTO [POSITION_OF_EMPLOYEE] (ID_POSITION_OF_EMPLOYEE, NAME_POSITION_OF_EMPLOYEE) VALUES (" + idPosition + ", '" + namePositin + "') ";

                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        myFunction.MessageDataSeved();
                        textBoxNameEmployee.Clear();
                        textBoxTelephoneNumber.Clear();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
        }
    }
}
