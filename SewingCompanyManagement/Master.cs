using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
//using System.SqlConn;

namespace SewingCompanyManagement
{
    public partial class frmMaster : Form
    {
       private OleDbConnection myConnection = new OleDbConnection();
        public frmMaster()
        {
            InitializeComponent();
           // myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=TrueDB_01.mdb;User Id=admin;Password=;";
        }

        private void buttonGetOperationMaster_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorMaster1.Text = "";
                int order = 0;
                int model = 0;
                if (string.IsNullOrEmpty(comboBoxNumberOfOrderForMasterView.Text)) order = 0;
                else order = int.Parse(comboBoxNumberOfOrderForMasterView.Text);
                if (string.IsNullOrEmpty(comboBoxNumberOfModelForMaster.Text)) model = 0;
                else model = int.Parse(comboBoxNumberOfModelForMaster.Text);
                dataGridViewMaster.DataSource = DataBaseHelper.Master_GetOperationsToDo(order, model);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }
        }
        
        private void comboBoxNumberOfOrderMaster_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxNumberOfOrderMaster);
                comboBoxNumberOfOrderMaster.Items.AddRange(DataBaseHelper.GetNumberOfOrder().ToArray());
                MyFunctions.ClearCbx(comboBoxNumberOfModelMaster);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxNumberOfModelMaster_DropDown(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(comboBoxNumberOfOrderMaster.Text))
            {
                MyFunctions.MessageChooseOrder();
            }
            else
            {
                try
                {
                    int namberOfOrder = int.Parse(comboBoxNumberOfOrderMaster.Text);
                    MyFunctions.ClearCbx(comboBoxNumberOfModelMaster);
                    comboBoxNumberOfModelMaster.Items.AddRange(DataBaseHelper.GetNumberOfModelByOrder(namberOfOrder).ToArray());
                    MyFunctions.ClearCbx(comboBoxNumberOfOperationMaster);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error  " + ex);
                }
            }
        }
        //вывод списка заказов в комбобокс для  просмотра всех операций
        private void comboBoxNumberOfOrderForMasterView_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxNumberOfOrderForMasterView);
                comboBoxNumberOfOrderForMasterView.Items.AddRange(DataBaseHelper.GetNumberOfOrder().ToArray());
            }
            catch (Exception ex)
            {
                //вывод сообщения ошибки
                MessageBox.Show("Error  " + ex);
            }
        }

        //комбобокс для вывода списка моделей в заказе для просмотра всех операций
        private void comboBoxNumberOfModelForMasterView_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxNumberOfOrderForMasterView.Text))
            {
                ErrorMaster1.Text = "Оберіть значення для поля замовлення! ";
                //очистка комбобокса
                MyFunctions.ClearCbx(comboBoxNumberOfModelForMaster);
            }
            else
            {  
                try
                {
                    ErrorMaster1.Text = "";
                    //переменная которая принимает выбраное значение из комбабокса заказов
                    int order = int.Parse(comboBoxNumberOfOrderForMasterView.Text);
                    MyFunctions.ClearCbx(comboBoxNumberOfModelForMaster);
                    comboBoxNumberOfModelForMaster.Items.AddRange(DataBaseHelper.GetNumberOfModelByOrder(order).ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error  " + ex);
                }
            }
           
        }

        //комбобокс вывод списка имен операций для заказа и модели
        private void comboBoxNumberOfOperationMaster_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxNumberOfModelMaster.Text))
            {
                MyFunctions.MessageChooseModel();            
            }
            else
            {
                try
                {
                    int model = int.Parse(comboBoxNumberOfModelMaster.Text);
                    MyFunctions.ClearCbx(comboBoxNumberOfOperationMaster);
                    comboBoxNumberOfOperationMaster.Items.AddRange(DataBaseHelper.GetNumberOfOperationByModel(model).ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error  " + ex);
                }
            }        
        }
        private void comboBoxIDWorkerMaster_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxIDWorkerMaster);
                comboBoxIDWorkerMaster.Items.AddRange(DataBaseHelper.GetNumberIdAndNameOfEmployee().ToArray());
            }
            catch (Exception ex)
            {
                //вывод сообщения ошибки
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddOperationForWorker_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxNumberOfOrderMaster.Text) || string.IsNullOrEmpty(comboBoxNumberOfModelMaster.Text)
                               || string.IsNullOrEmpty(comboBoxNumberOfOperationMaster.Text) || string.IsNullOrEmpty(comboBoxIDWorkerMaster.Text) || string.IsNullOrEmpty(textBoxNumberOfOperation.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int order = int.Parse(comboBoxNumberOfOrderMaster.Text + comboBoxNumberOfModelMaster.Text);
                    int model = int.Parse(comboBoxNumberOfModelMaster.Text);
                    int operation = int.Parse(comboBoxNumberOfOperationMaster.Text);
                    int employer = int.Parse(comboBoxIDWorkerMaster.Text.Split(' ')[0]);
                    int namberOfOperations = int.Parse(textBoxNumberOfOperation.Text);
                    //сравнение введенного текста с балансом невыполненых операций в базеданных 
                    //введенный текст не должен превышать остаток операций
                    if (compareBalanceAndEntredText(namberOfOperations) == 0)
                    {
                        namberOfOperations = getBalanceOfOperation();
                        MyFunctions.MessageEnteredDataIsWrong();
                    }
                    else
                    {
                        int operationForModel = DataBaseHelper.GetProductionOperationForModel(operation, model);
                        if (DataBaseHelper.InsertIntoOrderOfProductionOperation(order, operationForModel, employer, namberOfOperations) == true)//true-insert is done, false - is not 
                        {
                            MyFunctions.MessageDataSeved();
                            textBoxNumberOfOperation.Clear();
                        }
                        else
                        {
                            MyFunctions.MessageAllOperationsIsDone();
                            textBoxNumberOfOperation.Text = getBalanceOfOperation().ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }
        //after click data table shows operations of employee
        private void buttonGetOperationOfEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int order = 0;
                int model = 0;

                if (string.IsNullOrEmpty(comboBoxNumberOfOrderForMasterView.Text)) order = 0;
                else order = int.Parse(comboBoxNumberOfOrderForMasterView.Text);

                if (string.IsNullOrEmpty(comboBoxNumberOfModelForMaster.Text)) model = 0;
                else model = int.Parse(comboBoxNumberOfModelForMaster.Text);
                ErrorMaster1.Text = "";
                dataGridViewMaster.DataSource = DataBaseHelper.Master_GetOperationsEmployeeDone(order, model);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonDelRowOerationIsDone_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxIdOperanionIsDone.Text);
                DataBaseHelper.Master_DelRowOerationIsDoneById(id);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            } 
        }

        private void textBoxNumberOfOperation_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void textBoxIdOperanionIsDone_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void TextBoxNumberOfOperation_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(textBoxNumberOfOperation.Text) > getBalanceOfOperation() || int.Parse(textBoxNumberOfOperation.Text) < 0)
            {
                textBoxNumberOfOperation.Text = getBalanceOfOperation().ToString();
            }
        }
        private int compareBalanceAndEntredText( int text)
        {
            int balance = getBalanceOfOperation();
            if (balance <= text)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        private void ComboBoxNumberOfOperationMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxNumberOfOperation.Text = getBalanceOfOperation().ToString();
        }
        private int getBalanceOfOperation()
        {          
           
                int operation = int.Parse(comboBoxNumberOfOperationMaster.Text);
                int orderModel = int.Parse(comboBoxNumberOfOrderMaster.Text + comboBoxNumberOfModelMaster.Text);
                int modelAndSize = int.Parse(comboBoxNumberOfModelMaster.Text);
                int operationToDo = DataBaseHelper.Master_getNamberOfModelToDo(orderModel);
                int operationIsDone = DataBaseHelper.Master_getNamberOfOperationInOrder(orderModel, modelAndSize, operation);
                int balance = operationToDo - operationIsDone;
                return balance;        
        }

    }
}




