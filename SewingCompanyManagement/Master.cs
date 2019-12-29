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
            ErrorMaster1.Text = "";
            int order = 0;
            int model = 0;
            if (string.IsNullOrEmpty(comboBoxNumberOfOrderForMasterView.Text)) order = 0;
            else order = int.Parse(comboBoxNumberOfOrderForMasterView.Text);
            if (string.IsNullOrEmpty(comboBoxNumberOfModelForMaster.Text)) model = 0;
            else model = int.Parse(comboBoxNumberOfModelForMaster.Text);
            dataGridViewMaster.DataSource = DataBaseHelper.Master_GetOperationsToDo(order,model);

        }

   
        private void comboBoxNumberOfOrderMaster_DropDown(object sender, EventArgs e)
        {
            try
            {
                comboBoxNumberOfOrderMaster.DataSource = null; ;
                comboBoxNumberOfOrderMaster.DataSource = DataBaseHelper.GetNumberOfOrder();
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
                    comboBoxNumberOfModelMaster.DataSource = null; ;
                    comboBoxNumberOfModelMaster.DataSource = DataBaseHelper.GetNumberOfModelByOrder(namberOfOrder);
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
                comboBoxNumberOfOrderMaster.DataSource = null; ;
                comboBoxNumberOfOrderForMasterView.DataSource= DataBaseHelper.GetNumberOfOrder();
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
                comboBoxNumberOfModelForMaster.Items.Clear();
              //  comboBoxNumberOfModelForMaster.Text = "";
            }
            else
            {  
                try
                {
                    ErrorMaster1.Text = "";
                    //переменная которая принимает выбраное значение из комбабокса заказов
                    int order = int.Parse(comboBoxNumberOfOrderForMasterView.Text);
                    comboBoxNumberOfModelForMaster.DataSource=null;
                    comboBoxNumberOfModelForMaster.DataSource = DataBaseHelper.GetNumberOfModelByOrder(order);
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
                MyFunctions.MessageChooseModel();            }
            else
            {
                try
                {
                    int model = int.Parse(comboBoxNumberOfModelMaster.Text);
                    comboBoxNumberOfOperationMaster.DataSource = null; ;
                    comboBoxNumberOfOperationMaster.DataSource = DataBaseHelper.GetNumberOfOperationByModel(model);
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
                comboBoxIDWorkerMaster.DataSource = null; ;
                comboBoxIDWorkerMaster.DataSource = DataBaseHelper.GetNumberIdAndNameOfEmployee();
            }
            catch (Exception ex)
            {
                //вывод сообщения ошибки
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddOperationForWorker_Click(object sender, EventArgs e)
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
                if (compareBalanceAndEntredText(sender,e, namberOfOperations)==0)
                {
                    namberOfOperations = getBalanceOfOperation();
                    MyFunctions.MessageEnteredDataIsWrong();
                }
                
                int operationForModel = 0;
                string dateTime = DateTime.Now.ToShortDateString();
                try
                {
                    myConnection.Open();
                    lblConnections.Text = "Connection Successful";
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    string oprtQery = "SELECT PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATIONS_FOR_MODEL, PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION, MODEL_AND_SIZE.ID_MODEL_AND_SIZE " +
                        "FROM PRODUCTION_OPERATION INNER JOIN(PRODUCTION_OPERATION_FOR_MODEL INNER JOIN MODEL_AND_SIZE ON PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE = MODEL_AND_SIZE.ID_MODEL_AND_SIZE) " +
                        "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION " +
                        "WHERE(((PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION) = " + operation + ") AND((MODEL_AND_SIZE.ID_MODEL_AND_SIZE) = " + model + ")); ";
                    command.CommandText = oprtQery;
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //считывание данных из базы данных                  
                        operationForModel = int.Parse(reader["ID_PRODUCTION_OPERATIONS_FOR_MODEL"].ToString());
                    }
                    reader.Close();

                    if (namberOfOperations>0)
                    {
                        string query = "INSERT INTO [ORDER_OF_PRODCTION_OPERATIONS] " +
                        "(ID_ORDER_LIST_MODEL,	ID_PRODUCTION_OPERATIONS_FOR_MODEL,	ID_EMPLOYEE,	NAMBER_OF_OPERATIONS_IS_DONE, [DATE]) " +
                        "VALUES ('" + order + "' , '" + operationForModel + "' , '" + employer + "' , '" + namberOfOperations + "' , '" + dateTime + "')";

                        command.CommandText = query;
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MyFunctions.MessageDataSeved();
                            textBoxNumberOfOperation.Clear();
                        }
                        myConnection.Close();
                    }
                    else
                    {     
                        MyFunctions.MessageAllOperationsIsDone();
                        myConnection.Close();
                        textBoxNumberOfOperation.Text = getBalanceOfOperation().ToString();
                    }
                    
                }
                    
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
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

        private void groupBoxMaster_Enter(object sender, EventArgs e)
        {

        }

        private void textBoxIdOperanionIsDone_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonDelRowOerationIsDone_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBoxIdOperanionIsDone.Text);
            DataBaseHelper.Master_DelRowOerationIsDoneById(id);
        }

        private void comboBoxNumberOfModelForMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNumberOfOperation_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void textBoxIdOperanionIsDone_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void ComboBoxIDWorkerMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxNumberOfOperation_TextChanged(object sender, EventArgs e)
        {
            
        }
        private int compareBalanceAndEntredText(object sender, EventArgs e, int text)
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
            if (DataBaseHelper.IsServerAlive())
            {
                int operation = int.Parse(comboBoxNumberOfOperationMaster.Text);
                int orderModel = int.Parse(comboBoxNumberOfOrderMaster.Text + comboBoxNumberOfModelMaster.Text);
                int modelAndSize = int.Parse(comboBoxNumberOfModelMaster.Text);
                int operationToDo = DataBaseHelper.Master_getNamberOfModelToDo(orderModel);
                int operationIsDone = DataBaseHelper.Master_getNamberOfOperationInOrder(orderModel, modelAndSize, operation);
                int balance = operationToDo - operationIsDone;
                return balance;
            }
            else
            {
                MyFunctions.MesageServerIsntAlive();
                return -1;
            }           
        }  
    }
}




