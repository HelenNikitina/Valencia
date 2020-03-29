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
       // private OleDbConnection myConnection = new OleDbConnection();
        public frmTechnologist()
        {
            InitializeComponent();
            //myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\GDrveSpecowka\Develop\#HELEN PROJECTS\SewingCompany\SewingCompanyManagement\TrueDB_01.mdb;User Id=admin;Password=;";
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
            try
            {
                if (string.IsNullOrEmpty(textBoxNumberOfModel.Text) || string.IsNullOrEmpty(textBoxNameOfModel.Text) || string.IsNullOrEmpty(textBoxDescriptOfModel.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int modelId = int.Parse(textBoxNumberOfModel.Text);
                    string modelName = textBoxNameOfModel.Text;
                    string modelDescription = textBoxDescriptOfModel.Text;

                    if (DataBaseHelper.AddNewModel(modelId, modelName, modelDescription))
                    {
                        MyFunctions.MessageDataSeved();
                        textBoxNumberOfModel.Clear();
                        textBoxNameOfModel.Clear();
                        textBoxDescriptOfModel.Clear();
                    }
                    else
                    {
                        MyFunctions.MessageSomethingWrong();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
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
            try
            {
                if (string.IsNullOrEmpty(comboBoxNumberOfModelToAddSize.Text) || string.IsNullOrEmpty(comboBoxStatureOfModel.Text) || string.IsNullOrEmpty(comboBoxSizeOfModel.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int modelNumber = int.Parse(comboBoxNumberOfModelToAddSize.Text.Split(' ')[0]);
                    int sizeAndSrature = int.Parse(comboBoxStatureOfModel.Text + comboBoxSizeOfModel.Text);
                    int modelAndSize = int.Parse(comboBoxNumberOfModelToAddSize.Text.Split(' ')[0] + comboBoxStatureOfModel.Text + comboBoxSizeOfModel.Text);
                    if (DataBaseHelper.AddSizeForModel(modelAndSize, modelNumber, sizeAndSrature))
                    {
                        MyFunctions.MessageDataSeved();
                    }
                    else
                    {
                        MyFunctions.MessageSomethingWrong();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
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
                MyFunctions.ClearCbx(comboBoxModelForAddOperations);
                comboBoxModelForAddOperations.Items.AddRange(DataBaseHelper.GetNumberOfModel().ToArray());
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
                MyFunctions.ClearCbx(comboBoxStatureOfModel);
                comboBoxStatureOfModel.Items.AddRange(DataBaseHelper.GetStature().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxSizeOfModel_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxSizeOfModel);
                comboBoxSizeOfModel.Items.AddRange(DataBaseHelper.GetSize().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }

        }

        private void buttonAddNewOperation_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxIdOperatinToNew.Text) || string.IsNullOrEmpty(textBoxNameOperationToNew.Text) || string.IsNullOrEmpty(textBoxOperatinnDescriptorToNew.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int numberOfOperation = int.Parse(textBoxIdOperatinToNew.Text);
                    string nameOfOperation = textBoxNameOperationToNew.Text;
                    string descriptionOfOperation = textBoxOperatinnDescriptorToNew.Text;

                    if (DataBaseHelper.AddNewOperation(numberOfOperation, nameOfOperation, descriptionOfOperation))
                    {
                        MyFunctions.MessageDataSeved();
                        textBoxIdOperatinToNew.Clear();
                        textBoxNameOperationToNew.Clear();
                        textBoxOperatinnDescriptorToNew.Clear();
                    }
                    else
                    {
                        MyFunctions.MessageSomethingWrong();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxStatureAndSizeForModel_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxModelForAddOperations.Text))
                {
                    MyFunctions.MessageChooseModel();
                }
                else
                {
                    int model = int.Parse(comboBoxModelForAddOperations.Text.Split(' ')[0]);
                    MyFunctions.ClearCbx(comboBoxStatureAndSizeForModel);
                    comboBoxStatureAndSizeForModel.Items.AddRange(DataBaseHelper.GetIdModelSizeStature(model).ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
            
            
        }

        private void comboBoxOperation_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxStatureAndSizeForModel.Text))
                {
                    MyFunctions.MessageChooseSize();
                }
                else
                {
                    MyFunctions.ClearCbx(comboBoxOperation);
                    comboBoxOperation.Items.AddRange(DataBaseHelper.GetProductionOperation().ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
            
           
        }
       
        private void textBoxTimeForOperation_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void buttonUpdateModele_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxModelNumberForUpdate.Text))
                {
                    MyFunctions.MessageChooseModel();
                }
                else
                {

                    if (string.IsNullOrEmpty(textBoxNewNamedelForUpdate.Text) && string.IsNullOrEmpty(textBoxNewDescriptModelForUpdate.Text))
                    {
                        MyFunctions.MessageNothingToChange();
                    }
                    else
                    {
                        int model = int.Parse(comboBoxModelNumberForUpdate.Text.Split(' ')[0]);
                        string newName = textBoxNewNamedelForUpdate.Text;
                        string newDescription = textBoxNewDescriptModelForUpdate.Text;
                        if (DataBaseHelper.UpdateModel(model, newName, newDescription))
                        {
                            textBoxNewNamedelForUpdate.Clear();
                            textBoxNewDescriptModelForUpdate.Clear();
                            MyFunctions.MessageDataUpdate();
                        }
                        else
                        {
                            MyFunctions.MessageSomethingWrong();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelNumberForUpdate_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxModelNumberForUpdate);
                comboBoxModelNumberForUpdate.Items.AddRange(DataBaseHelper.GetNumberOfModel().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddOperationsForModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxModelForAddOperations.Text) || string.IsNullOrEmpty(comboBoxStatureAndSizeForModel.Text) || string.IsNullOrEmpty(comboBoxOperation.Text) || string.IsNullOrEmpty(textBoxTimeForOperation.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int model = int.Parse(comboBoxModelForAddOperations.Text.Split(' ')[0] + comboBoxStatureAndSizeForModel.Text);
                    int operation = int.Parse(comboBoxOperation.Text);
                    int time = int.Parse(textBoxTimeForOperation.Text);

                    if (DataBaseHelper.InsertIntoProductionOperationForModel(model, operation, time))
                    {
                        MyFunctions.MessageDataSeved();
                        comboBoxOperation.Items.Clear();
                        textBoxTimeForOperation.Clear();
                    }
                    else
                    {
                        MyFunctions.MessageSomethingWrong();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }  
        }

        private void buttonUpdateOperation_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxNumberOperationForUpdate.Text))
                {
                    MyFunctions.MessageChooseOperation();
                }
                else
                {
                    int operation = int.Parse(comboBoxNumberOperationForUpdate.Text);
                    if (string.IsNullOrEmpty(textBoxNewNemOperationForUpdate.Text) && string.IsNullOrEmpty(textBoxNewOperationDescriptorForUpdate.Text))
                    {
                        MyFunctions.MessageNothingToChange();
                    }
                    else
                    {
                        string newName = textBoxNewNemOperationForUpdate.Text;
                        string newDescription = textBoxNewOperationDescriptorForUpdate.Text;

                        if (DataBaseHelper.UpdateOperation(operation, newName, newDescription))
                        {
                            textBoxNewNemOperationForUpdate.Clear();
                            textBoxNewOperationDescriptorForUpdate.Clear();
                            MyFunctions.MessageDataUpdate();
                        }
                        else
                        {
                            MyFunctions.MessageSomethingWrong();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxNumberOperationForUpdate_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxNumberOperationForUpdate);
                comboBoxNumberOperationForUpdate.Items.AddRange(DataBaseHelper.GetProductionOperation().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

    }
}
