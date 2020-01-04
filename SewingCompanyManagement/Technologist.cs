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
        public frmTechnologist()
        {
            InitializeComponent();
            myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\GDrveSpecowka\Develop\#HELEN PROJECTS\SewingCompany\SewingCompanyManagement\TrueDB_01.mdb;User Id=admin;Password=;";
        }

        private void buttonViewTableOfModel_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewTechnologist.DataSource = DataBaseHelper.GetModels(); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewTableOfOperation_Click(object sender, EventArgs e)
        {
            try
            {            
                dataGridViewTechnologist.DataSource = DataBaseHelper.GetOperatoins();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxViewModel_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxViewModelAndSize);
                comboBoxViewModelAndSize.Items.AddRange(DataBaseHelper.GetNumberOfModelAndSize().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewOperationsForModel_Click(object sender, EventArgs e)
        {
            try
            {
                int model ;
                if (string.IsNullOrEmpty(comboBoxViewModelAndSize.Text)) model = -1;
                else model=int.Parse(comboBoxViewModelAndSize.Text);

                dataGridViewTechnologist.DataSource = DataBaseHelper.Technologist_GetOperationsForModel(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }

        }

        private void buttonModelAndSize_Click(object sender, EventArgs e)
        {
            try
            {
                int model;
                if (string.IsNullOrEmpty(comboBoxViewModel.Text.Split(' ')[0])) model = -1;
                else model = int.Parse(comboBoxViewModel.Text.Split(' ')[0]);
                dataGridViewTechnologist.DataSource = DataBaseHelper.GetModelAndSizes(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }
        //
        private void comboBoxViewModel_DropDown_1(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxViewModel);
                comboBoxViewModel.Items.AddRange(DataBaseHelper.GetNumberOfModel().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddNewModel_Click(object sender, EventArgs e)
        {     
            if (string.IsNullOrEmpty(textBoxNumberOfModel.Text)|| string.IsNullOrEmpty(textBoxNameOfModel.Text)|| string.IsNullOrEmpty(textBoxDescriptOfModel.Text))
            {
                MyFunctions.MessageBlankFields();
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
                        MyFunctions.MessageDataSeved();
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
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void textBoxIdOperatinToNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void buttonAddSizeForModel_Click(object sender, EventArgs e)
        {
            int modelNumber, stature, size, modelAndSize, sizeAndSrature;
            if (string.IsNullOrEmpty(comboBoxNumberOfModelToAddSize.Text) || string.IsNullOrEmpty(comboBoxStatureOfModel.Text) || string.IsNullOrEmpty(comboBoxSizeOfModel.Text))
            {
                MyFunctions.MessageBlankFields();
            }
            else
            {
                modelNumber = int.Parse(comboBoxNumberOfModelToAddSize.Text.Split(' ')[0]);
                stature = int.Parse(comboBoxStatureOfModel.Text);
                size = int.Parse(comboBoxSizeOfModel.Text);
                sizeAndSrature= int.Parse(comboBoxStatureOfModel.Text+comboBoxSizeOfModel.Text);
                modelAndSize = int.Parse(comboBoxNumberOfModelToAddSize.Text.Split(' ')[0] + comboBoxStatureOfModel.Text + comboBoxSizeOfModel.Text);
                try
                {
                    myConnection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string query = "INSERT INTO MODEL_AND_SIZE VALUES (" + modelAndSize + ", '" + modelNumber + "', '" + sizeAndSrature + "' ); ";
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MyFunctions.MessageDataSeved();
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
                MyFunctions.ClearCbx(comboBoxNumberOfModelToAddSize);
                comboBoxNumberOfModelToAddSize.Items.AddRange(DataBaseHelper.GetNumberOfModel().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelAndSizeForAdd_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxModelAndSizeForAdd);
                comboBoxModelAndSizeForAdd.Items.AddRange(DataBaseHelper.GetNumberOfModel().ToArray());
            }
            catch (Exception ex)
            {
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
                MyFunctions.MessageBlankFields();
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
                        MyFunctions.MessageDataSeved();
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

        private void comboBoxStature_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxModelAndSizeForAdd.Text))
            {
                MyFunctions.MessageChooseModel();
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
                MyFunctions.MessageChooseSize();
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
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        //private void textBoxModelForDel_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    MyFunctions.MyDigitKeyPress(sender, e);
        //}

        //private void textBoxOperationForDel_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    MyFunctions.MyDigitKeyPress(sender, e);
        //}

        //private void textBoxModelForDelOperation_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    MyFunctions.MyDigitKeyPress(sender, e);
        //}

        //private void textBoxOperationForDelFromModel_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    MyFunctions.MyDigitKeyPress(sender, e);
        //}

        private void buttonDeleteModele_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxModelNumberForUpdate.Text))
            {
                MyFunctions.MessageChooseModel();
            }
            else
            {
                int model = int.Parse(comboBoxModelNumberForUpdate.Text);
                string newName=null;
                string newDescription = null;
                string query = null;
                if (string.IsNullOrEmpty(textBoxNewNamedelForUpdate.Text)&& string.IsNullOrEmpty(textBoxNewDescriptModelForUpdate.Text))
                {
                    MyFunctions.MessageChooseModel();
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
                            MyFunctions.MessageDataUpdate();
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

        private void comboBoxModelNumberForUpdate_DropDown(object sender, EventArgs e)
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
                MyFunctions.MessageBlankFields();
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
                        MyFunctions.MessageDataSeved();
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
                MyFunctions.MessageChooseOperation();
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
                            MyFunctions.MessageDataUpdate();
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

    }
}
