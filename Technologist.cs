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
    public partial class frmTechnologist : Form
    {
        private OleDbConnection myConnection = new OleDbConnection();
        MyFunctions myFunction = new MyFunctions();
        public frmTechnologist()
        {
            InitializeComponent();
            myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=TrueDB_01.mdb;User Id=admin;Password=;";
        }

        private void buttonViewTableOfModel_Click(object sender, EventArgs e)
        {
            try
            {
               
                string query = "SELECT MODEL.ID_MODEL as [Модель № ], MODEL.SHORT_NAME_OF_MODEL as [Назва моделі], MODEL.MODEL_DESCRIPTION as [Опис моделі] FROM MODEL; ";
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewTechnologist.DataSource = dt;
             
               

                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewTableOfOperation_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION as [Операція №], " +
                    "PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION as [Назва операції], " +
                    "PRODUCTION_OPERATION.PRODUCTION_OPERATION_DESCRIPTION as [Опис операції] " +
                    "FROM PRODUCTION_OPERATION ";
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewTechnologist.DataSource = dt;

                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxViewModel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxViewModel_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE FROM MODEL_AND_SIZE ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxViewModelAndSize.Items.Clear();
                while (reader.Read())
                {
                    comboBoxViewModelAndSize.Items.Add(reader["ID_MODEL_AND_SIZE"].ToString());
                }
                comboBoxViewModelAndSize.Show();
                myConnection.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonViewOperationsForModel_Click(object sender, EventArgs e)
        {
            try
            {
                int model ;
                if (string.IsNullOrEmpty(comboBoxViewModelAndSize.Text)) model = 0;
                else model=int.Parse(comboBoxViewModelAndSize.Text);
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = null;
                string qt = "SELECT " +
                    "PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATIONS_FOR_MODEL as [ID], " +
                    "MODEL.SHORT_NAME_OF_MODEL as [Назва моделі], " +
                    "MODEL_AND_SIZE.ID_MODEL as [Номер моделі], " +
                    "SIZE_OF_MODEL_STATURE.ID_MODEL_SIZE_STATURE as [Розмір моделі], " +
                    "PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION as [Назва операції], " +
                    "PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION as [Номер операції ], " +
                    "PRODUCTION_OPERATION_FOR_MODEL.TIME_FOR_PRODUCTION_OPERATION as [Час виконання операції] " +
                    "FROM SIZE_OF_MODEL_STATURE INNER JOIN(PRODUCTION_OPERATION INNER JOIN (MODEL INNER JOIN (MODEL_AND_SIZE INNER JOIN PRODUCTION_OPERATION_FOR_MODEL ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) ON MODEL.ID_MODEL = MODEL_AND_SIZE.ID_MODEL) " +
                    "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION) ON SIZE_OF_MODEL_STATURE.ID_MODEL_SIZE_STATURE = MODEL_AND_SIZE.ID_MODEL_SIZE_STATURE ";
                if (model==0)
                {
                    query = qt;
                }
                else
                {
                    query = qt + "WHERE(((PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) = " + model + ")) ";
                }
                 
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewTechnologist.DataSource = dt;
                myConnection.Close();

            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }

        }

        private void buttonModelAndSize_Click(object sender, EventArgs e)
        {
            try
            {
                int model;
                if (string.IsNullOrEmpty(comboBoxViewModel.Text)) model = 0;
                else model = int.Parse(comboBoxViewModel.Text);
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = null;
                string qt = "SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE as [Модель и размір №], MODEL_AND_SIZE.ID_MODEL as [Модель №], MODEL_AND_SIZE.ID_MODEL_SIZE_STATURE as [Зріст та Размір] FROM MODEL_AND_SIZE "; 
                if (model == 0)
                {
                    query = qt;
                }
                else
                {
                    query = qt + "WHERE (((MODEL_AND_SIZE.ID_MODEL) = " + model + "))";
                }
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewTechnologist.DataSource = dt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }
        //
        private void comboBoxViewModel_DropDown_1(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT ID_MODEL FROM MODEL ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxViewModel.Items.Clear();
                while (reader.Read())
                {
                    comboBoxViewModel.Items.Add(reader["ID_MODEL"].ToString());
                }
                comboBoxViewModel.Show();
                myConnection.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddNewModel_Click(object sender, EventArgs e)
        {     
            if (string.IsNullOrEmpty(textBoxNumberOfModel.Text)|| string.IsNullOrEmpty(textBoxNameOfModel.Text)|| string.IsNullOrEmpty(textBoxDescriptOfModel.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                int modelId = int.Parse(textBoxNumberOfModel.Text);
                string modelName = textBoxNameOfModel.Text;
                string modelDescription = textBoxDescriptOfModel.Text;
             
                try
                {
                    myConnection.Open();

                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "INSERT INTO MODEL VALUES (" + modelId + ", '" + modelName + "', '" + modelDescription + "' ); ";
                    command.CommandText = query;
                    if (command.ExecuteNonQuery()==1)
                    {
                        myFunction.MessageDataSeved();
                        textBoxNumberOfModel.Clear();
                        textBoxNameOfModel.Clear();
                        textBoxDescriptOfModel.Clear();
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

        private void textBoxNumberOfModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void textBoxIdOperatinToNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void buttonAddSizeForModel_Click(object sender, EventArgs e)
        {
            int modelNumber, stature, size, modelAndSize, sizeAndSrature;
            if (string.IsNullOrEmpty(comboBoxNumberOfModelToAddSize.Text) || string.IsNullOrEmpty(comboBoxStatureOfModel.Text) || string.IsNullOrEmpty(comboBoxSizeOfModel.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                modelNumber = int.Parse(comboBoxNumberOfModelToAddSize.Text);
                stature = int.Parse(comboBoxStatureOfModel.Text);
                size = int.Parse(comboBoxSizeOfModel.Text);
                sizeAndSrature= int.Parse(comboBoxStatureOfModel.Text+comboBoxSizeOfModel.Text);
                modelAndSize = int.Parse(comboBoxNumberOfModelToAddSize.Text + comboBoxStatureOfModel.Text + comboBoxSizeOfModel.Text);
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "INSERT INTO MODEL_AND_SIZE VALUES (" + modelAndSize + ", '" + modelNumber + "', '" + sizeAndSrature + "' ); ";
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

        private void comboBoxNumberOfModelToAddSize_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT ID_MODEL FROM MODEL ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxNumberOfModelToAddSize.Items.Clear();
                while (reader.Read())
                {
                    comboBoxNumberOfModelToAddSize.Items.Add(reader["ID_MODEL"].ToString());
                }
                comboBoxNumberOfModelToAddSize.Show();
                myConnection.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelAndSizeForAdd_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT * FROM MODEL ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxModelAndSizeForAdd.Items.Clear();
                while (reader.Read())
                {
                    comboBoxModelAndSizeForAdd.Items.Add(reader["ID_MODEL"].ToString()+" "+reader["SHORT_NAME_OF_MODEL"].ToString());
                }
                comboBoxModelAndSizeForAdd.Show();
                myConnection.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxStatureOfModel_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT NUMBER_OF_STATURE FROM STATURE ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();

                comboBoxStatureOfModel.Items.Clear();
                while (reader.Read())
                {
                    comboBoxStatureOfModel.Items.Add(reader["NUMBER_OF_STATURE"].ToString());
                }
                comboBoxStatureOfModel.Show();
                myConnection.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxSizeOfModel_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT [SIZE] FROM SIZE_OF_MODEL; ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxSizeOfModel.Items.Clear();
                while (reader.Read())
                {
                    comboBoxSizeOfModel.Items.Add(reader["SIZE"].ToString());
                }
                comboBoxSizeOfModel.Show();
                myConnection.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }

        }

        private void buttonAddNewOperation_Click(object sender, EventArgs e)
        {
            int numberOfOperation;
            string nameOfOperation, descriptionOfOperation;
            if (string.IsNullOrEmpty(textBoxIdOperatinToNew.Text)|| string.IsNullOrEmpty(textBoxNameOperationToNew.Text)|| string.IsNullOrEmpty(textBoxOperatinnDescriptorToNew.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                numberOfOperation = int.Parse(textBoxIdOperatinToNew.Text);
                nameOfOperation = textBoxNameOperationToNew.Text;
                descriptionOfOperation = textBoxOperatinnDescriptorToNew.Text;

                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "INSERT INTO PRODUCTION_OPERATION VALUES (" + numberOfOperation + ", '" + nameOfOperation + "', '" + descriptionOfOperation + "' ); ";
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        myFunction.MessageDataSeved();
                        textBoxIdOperatinToNew.Clear();
                        textBoxNameOperationToNew.Clear();
                        textBoxOperatinnDescriptorToNew.Clear();
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxStature_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxModelAndSizeForAdd.Text))
            {
                myFunction.MessageChooseModel();
            }
            else
            {
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    //int a = int.Parse(s.Split(' ')[0]);
                    int a = int.Parse(comboBoxModelAndSizeForAdd.Text.Split(' ')[0]);
                    string query = "SELECT MODEL_AND_SIZE.ID_MODEL, SIZE_OF_MODEL_STATURE.ID_MODEL_SIZE_STATURE " +
                        "FROM SIZE_OF_MODEL_STATURE INNER JOIN MODEL_AND_SIZE ON SIZE_OF_MODEL_STATURE.ID_MODEL_SIZE_STATURE = MODEL_AND_SIZE.ID_MODEL_SIZE_STATURE " +
                        "WHERE(((MODEL_AND_SIZE.ID_MODEL) = " + a + "))";
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    comboBoxStature.Items.Clear();
                    while (reader.Read())
                    {
                        comboBoxStature.Items.Add(reader["ID_MODEL_SIZE_STATURE"].ToString());
                    }
                    comboBoxStature.Show();
                    myConnection.Close();

                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
            
        }

        private void comboBoxOperation_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxStature.Text))
            {
                myFunction.MessageChooseSize();
            }
            else
            {
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "SELECT PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION FROM PRODUCTION_OPERATION;";
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    comboBoxOperation.Items.Clear();
                    while (reader.Read())
                    {
                        comboBoxOperation.Items.Add(reader["ID_PRODUCTION_OPERATION"].ToString());
                    }
                    comboBoxOperation.Show();
                    myConnection.Close();
                    reader.Close();

                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
           
        }
       
        private void textBoxTimeForOperation_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void textBoxModelForDel_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void textBoxOperationForDel_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void textBoxModelForDelOperation_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void textBoxOperationForDelFromModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void buttonDeleteModele_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxModelNumberForUpdate.Text))
            {
                myFunction.MessageChooseModel();
            }
            else
            {
                int model = int.Parse(comboBoxModelNumberForUpdate.Text);
                string newName=null;
                string newDescription = null;
                string query = null;
                if (string.IsNullOrEmpty(textBoxNewNamedelForUpdate.Text)&& string.IsNullOrEmpty(textBoxNewDescriptModelForUpdate.Text))
                {
                    myFunction.MessageChooseModel();
                }
                else
                {
                    if (string.IsNullOrEmpty(textBoxNewDescriptModelForUpdate.Text))
                    {
                        newName = textBoxNewNamedelForUpdate.Text;
                        query = "UPDATE MODEL SET MODEL.SHORT_NAME_OF_MODEL = '" + newName + "' " +
                       "WHERE(((MODEL.ID_MODEL) = " + model + ")); ";
                    }
                    else if (string.IsNullOrEmpty(textBoxNewNamedelForUpdate.Text))
                    {
                        newDescription = textBoxNewDescriptModelForUpdate.Text;
                        query = "UPDATE MODEL SET MODEL.MODEL_DESCRIPTION = '" + newDescription + "' " +
                        "WHERE(((MODEL.ID_MODEL) = " + model + ")); ";
                    }
                    else
                    {
                        newName = textBoxNewNamedelForUpdate.Text;
                        newDescription = textBoxNewDescriptModelForUpdate.Text;
                        query = "UPDATE MODEL SET MODEL.SHORT_NAME_OF_MODEL = '" + newName + "', MODEL.MODEL_DESCRIPTION = '" + newDescription + "' " +
                        "WHERE(((MODEL.ID_MODEL) = " + model + ")); ";
                    }

                    try
                    {
                        myConnection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = myConnection;
                        command.CommandText = query;
                        if (command.ExecuteNonQuery() == 1)
                        {
                            myFunction.MessageDataUpdate();
                            textBoxNewNamedelForUpdate.Clear();
                            textBoxNewDescriptModelForUpdate.Clear();         
                        }
                        myConnection.Close();
                        buttonViewTableOfModel_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        myConnection.Close();
                        MessageBox.Show("Error  " + ex);
                    }

                }

            }

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT ID_MODEL FROM MODEL ";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxModelNumberForUpdate.Items.Clear();
                while (reader.Read())
                {
                    comboBoxModelNumberForUpdate.Items.Add(reader["ID_MODEL"].ToString());
                }
                comboBoxModelNumberForUpdate.Show();
                myConnection.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddOperationsForModel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxModelAndSizeForAdd.Text)|| string.IsNullOrEmpty(comboBoxStature.Text)|| string.IsNullOrEmpty(comboBoxOperation.Text)|| string.IsNullOrEmpty(textBoxTimeForOperation.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                int model = int.Parse(comboBoxModelAndSizeForAdd.Text.Split(' ')[0] + comboBoxStature.Text);
                int operation = int.Parse(comboBoxOperation.Text);
                int time = int.Parse(textBoxTimeForOperation.Text);
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "INSERT INTO PRODUCTION_OPERATION_FOR_MODEL(ID_MODEL_AND_SIZE, ID_PRODUCTION_OPERATION, TIME_FOR_PRODUCTION_OPERATION) VALUES (" + model + ", " + operation + ", " + time + " ); ";
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        myFunction.MessageDataSeved();
                        comboBoxOperation.Items.Clear();
                        textBoxTimeForOperation.Clear();
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

        private void buttonUpdateOperation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxNumberOperationForUpdate.Text))
            {
                myFunction.MessageChooseOperation();
            }
            else
            {
                int operation = int.Parse(comboBoxNumberOperationForUpdate.Text);
                string newName = null;
                string newDescription = null;
                string query = null;
                if (string.IsNullOrEmpty(textBoxNewNemOperationForUpdate.Text) && string.IsNullOrEmpty(textBoxNewOperationDescriptorForUpdate.Text))
                {
                    MessageBox.Show("Пустые поля! Для изменения введите новые значения");
                }
                else
                {
                    if (string.IsNullOrEmpty(textBoxNewOperationDescriptorForUpdate.Text))
                    {
                        newName = textBoxNewNemOperationForUpdate.Text;
                        query = "UPDATE PRODUCTION_OPERATION SET PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION = '" + newName + "' " +
                       "WHERE(((PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION) = " + operation + ")); ";
                    }
                    else if (string.IsNullOrEmpty(textBoxNewNemOperationForUpdate.Text))
                    {
                        newDescription = textBoxNewOperationDescriptorForUpdate.Text;
                        query = "UPDATE PRODUCTION_OPERATION SET PRODUCTION_OPERATION.PRODUCTION_OPERATION_DESCRIPTION = '" + newDescription + "' " +
                        "WHERE(((PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION) = " + operation + ")); ";
                    }
                    else
                    {
                        newName = textBoxNewNemOperationForUpdate.Text;
                        newDescription = textBoxNewOperationDescriptorForUpdate.Text;
                        query = "UPDATE PRODUCTION_OPERATION SET PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION = '" + newName + "', PRODUCTION_OPERATION.PRODUCTION_OPERATION_DESCRIPTION = '" + newDescription + "' " +
                        "WHERE(((PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION) = " + operation + ")); ";
                    }
                    try
                    {
                        myConnection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = myConnection;
                        command.CommandText = query;
                        if (command.ExecuteNonQuery() == 1)
                        {
                            myFunction.MessageDataUpdate();
                            textBoxNewNemOperationForUpdate.Clear();
                            textBoxNewOperationDescriptorForUpdate.Clear();
                        }
                        myConnection.Close();
                        buttonViewTableOfOperation_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        myConnection.Close();
                        MessageBox.Show("Error  " + ex);
                    }

                }

            }
        }

        private void comboBoxNumberOperationForUpdate_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION FROM PRODUCTION_OPERATION;";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxNumberOperationForUpdate.Items.Clear();
                while (reader.Read())
                {
                    comboBoxNumberOperationForUpdate.Items.Add(reader["ID_PRODUCTION_OPERATION"].ToString());
                }
                comboBoxNumberOperationForUpdate.Show();
                myConnection.Close();
                reader.Close();

            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelAndSizeForDeliteOperation_DropDown(object sender, EventArgs e)
        {
            //try
            //{
            //    myConnection.Open();
            //    OleDbCommand command = new OleDbCommand();
            //    command.Connection = myConnection;
            //    string query = "SELECT ID_MODEL_AND_SIZE FROM MODEL_AND_SIZE ";
            //    command.CommandText = query;
            //    OleDbDataReader reader = command.ExecuteReader();
            //    comboBoxModelAndSizeForDeliteOperation.Items.Clear();
            //    while (reader.Read())
            //    {
            //        comboBoxModelAndSizeForDeliteOperation.Items.Add(reader["ID_MODEL_AND_SIZE"].ToString());
            //    }
            //    comboBoxModelAndSizeForDeliteOperation.Show();
            //    myConnection.Close();
            //    reader.Close();
            //}
            //catch (Exception ex)
            //{
            //    myConnection.Close();
            //    MessageBox.Show("Error  " + ex);
            //}
        }

        private void comboBoxNumberOperetionForDelite_DropDown(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(comboBoxModelAndSizeForDeliteOperation.Text))
            //{
            //    MessageBox.Show("Выбирите значение для МодельРостРазмер");
            //}
            //else
            //{
            //    int model = int.Parse(comboBoxModelAndSizeForDeliteOperation.Text);
            //    try
            //    {
            //        myConnection.Open();
            //        OleDbCommand command = new OleDbCommand();
            //        command.Connection = myConnection;
            //        string query = "SELECT PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE, PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION " +
            //            "FROM PRODUCTION_OPERATION_FOR_MODEL " +
            //            "WHERE(((PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) = "+ model + "));";
            //        command.CommandText = query;
            //        OleDbDataReader reader = command.ExecuteReader();
            //        comboBoxNumberOperetionForDelite.Items.Clear();
            //        while (reader.Read())
            //        {
            //            comboBoxNumberOperetionForDelite.Items.Add(reader["ID_PRODUCTION_OPERATION"].ToString());
            //        }
            //        comboBoxNumberOperetionForDelite.Show();
            //        myConnection.Close();
            //        reader.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        myConnection.Close();
            //        MessageBox.Show("Error  " + ex);
            //    }
            //}
            
        }

        private void buttonDeleteOperationFromModel_Click(object sender, EventArgs e)
        {
        //    int modelAndSize = int.Parse(comboBoxModelAndSizeForDeliteOperation.Text);
        //    int operation= int.Parse(comboBoxNumberOperetionForDelite.Text);
        //    string query = null;
        //    try
        //    {
        //        myConnection.Open();
        //        OleDbCommand command = new OleDbCommand();
        //        command.Connection = myConnection;
        //        query = "DELETE PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE, PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION " +
        //            "FROM PRODUCTION_OPERATION_FOR_MODEL " +
        //            "WHERE(((PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) = "+ modelAndSize + ") AND((PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION) = "+ operation + ")) ";
        //        command.CommandText = query;
        //        if (command.ExecuteNonQuery() == 1)
        //        {
        //            MessageBox.Show("Data deleted");
        //        }
        //        myConnection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        myConnection.Close();
        //        MessageBox.Show("Error  " + ex);
        //    }
        }

        private void comboBoxStature_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTimeForOperation_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxModelAndSizeForAdd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
