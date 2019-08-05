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
        MyFunctions myFunction = new MyFunctions();
        public frmMaster()
        {
            InitializeComponent();
            myConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=TrueDB_01.mdb;User Id=admin;Password=;";
        }

        private void buttonGetOperationMaster_Click(object sender, EventArgs e)
        {
            PrintDBofPrerations();
        }

        private void PrintDBofPrerations()
        {
            try
            {
                string tq= "SELECT ORDER.ID_ORDER as [Замовлення №], " +
                        "ORDER_MODEL.ID_MODEL_AND_SIZE as [Модель №], " +
                        "PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION as [Операція №], " +
                        "PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION as [Назва операції], " +
                        "ORDER_MODEL.NUMBER_OF_MODELS as [Кількість операцій у замовленні] " +
                        "FROM PRODUCTION_OPERATION INNER JOIN ([ORDER] INNER JOIN ((MODEL_AND_SIZE INNER JOIN PRODUCTION_OPERATION_FOR_MODEL " +
                        "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) INNER JOIN ORDER_MODEL " +
                        "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) ON ORDER.ID_ORDER = ORDER_MODEL.ID_ORDER) " +
                        "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION ";
                string query=null;
                int order = 0;
                int model = 0;

                if (string.IsNullOrEmpty(comboBoxNumberOfOrderForMasterView.Text)) order = 0;
                else order = int.Parse(comboBoxNumberOfOrderForMasterView.Text);
             
                if (string.IsNullOrEmpty(comboBoxNumberOfModelForMaster.Text)) model = 0;
                else model = int.Parse(comboBoxNumberOfModelForMaster.Text);


                myConnection.Open();
                lblConnections.Text = "Connection Successful";
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                if (order==0 && model==0)
                {
                    query = tq;
                    ErrorMaster1.Text = "";
                }
                else if (model==0)
                {
                    query = tq + "WHERE((ORDER.ID_ORDER) = " + order + ");";
                    ErrorMaster1.Text = "";
                }
                else
                {
                    query = tq +"WHERE(((ORDER.ID_ORDER) = " + order + ") AND((ORDER_MODEL.ID_MODEL_AND_SIZE) = " + model + "));";
                    ErrorMaster1.Text = "";
                }
                
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewMaster.DataSource = dt;
               
                myConnection.Close();
            }
            catch(Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  "+ ex);
            } 
        }
       
        private void comboBoxNumberOfOrderMaster_SelectedIndexChanged(object sender, MouseEventArgs e)
        {
            
        }
       // int selectedOrderInComboBox;
        private void comboBoxNumberOfOrderMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedOrderInComboBox= int.Parse(comboBoxNumberOfOrderMaster.Text);
        }

        private void comboBoxNumberOfModelMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxNumberOfOrderMaster_DropDown(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                lblConnections.Text = "Connection Successful";
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "SELECT ORDER.ID_ORDER FROM [ORDER]";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                comboBoxNumberOfOrderMaster.Items.Clear();
                while (reader.Read())
                {
                    comboBoxNumberOfOrderMaster.Items.Add(reader["ID_ORDER"].ToString());
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxNumberOfModelMaster_DropDown(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBoxNumberOfOrderMaster.Text))
            {
                myFunction.MessageChooseOrder();
            }
            else
            {
                try
                {
                    myConnection.Open();
                    lblConnections.Text = "Connection Successful";
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;

                    int namberOfOrder = int.Parse(comboBoxNumberOfOrderMaster.Text);
                    string query = "SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE " +
                        "FROM[ORDER] INNER JOIN(MODEL_AND_SIZE INNER JOIN ORDER_MODEL ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) " +
                        "ON ORDER.ID_ORDER = ORDER_MODEL.ID_ORDER " +
                    "WHERE((ORDER.ID_ORDER) = " + namberOfOrder + ");";
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    comboBoxNumberOfModelMaster.Items.Clear();
                    while (reader.Read())
                    {
                        comboBoxNumberOfModelMaster.Items.Add(reader["ID_MODEL_AND_SIZE"].ToString());
                    }
                    comboBoxNumberOfModelMaster.Show();
                    myConnection.Close();

                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
        }
        //вывод списка заказов в комбобокс для  просмотра всех операций
        private void comboBoxNumberOfOrderForMasterView_DropDown(object sender, EventArgs e)
        {
            try
            {
                //открытие подключениия к базе данных
                myConnection.Open();
                //вывод текста индикатора соединения
                lblConnections.Text = "Connection Successful";
                //создание обьекта для инструкции
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                //текст команбы
                string query = "SELECT ORDER.ID_ORDER FROM [ORDER]";
                //создание SQL команды 
                command.CommandText = query;
                //считывание данных из базы данных
                OleDbDataReader reader = command.ExecuteReader();
                //создание обьекта для считывания данных из базы данных
                comboBoxNumberOfOrderForMasterView.Items.Clear();

                while (reader.Read())
                {
                    //считывание данных из базы данных и вывод в комбобокс
                    comboBoxNumberOfOrderForMasterView.Items.Add(reader["ID_ORDER"].ToString());
                }
                //закрытие соединения с базой данных
                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
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
                comboBoxNumberOfModelForMaster.Text = "";
            }
            else
            {
                ErrorMaster1.Text = "";
                //переменная которая принимает выбраное значение из комбабокса заказов
                int namberfOrder = int.Parse(comboBoxNumberOfOrderForMasterView.Text);
                try
                {

                    //открытие подключениия к базе данных
                    myConnection.Open();
                    //вывод текста индикатора соединения
                    lblConnections.Text = "Connection Successful";
                    //создание обьекта для инструкции
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;

                    //текст команбы
                    string query = "SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE " +
                        "FROM[ORDER] INNER JOIN(MODEL_AND_SIZE INNER JOIN ORDER_MODEL ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) " +
                        "ON ORDER.ID_ORDER = ORDER_MODEL.ID_ORDER " +
                        "WHERE((ORDER.ID_ORDER) = " + namberfOrder + ");";
                    //создание SQL команды
                    command.CommandText = query;
                    //создание обьекта для считывания данных из базы данных
                    OleDbDataReader reader = command.ExecuteReader();
                    //очистка комбобокса перед выводом данных
                    comboBoxNumberOfModelForMaster.Items.Clear();

                    while (reader.Read())
                    {
                        //считывание данных из базы данных и вывод в комбобокс
                        comboBoxNumberOfModelForMaster.Items.Add(reader["ID_MODEL_AND_SIZE"].ToString());
                    }
                    //закрытие соединения с базой данных
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
           
        }

        //комбобокс вывод списка имен операций для заказа и модели
        private void comboBoxNumberOfOperationMaster_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxNumberOfModelMaster.Text))
            {
                myFunction.MessageChooseModel();            }
            else
            {
                try
                {
                    //открытие подключениия к базе данных
                    myConnection.Open();
                    //вывод текста индикатора соединения
                    lblConnections.Text = "Connection Successful";
                    //создание обьекта для инструкции
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = myConnection;
                    //переменная которая принимает выбраное значение из комбабокса заказов 
                    int a = int.Parse(comboBoxNumberOfModelMaster.Text);
                    //текст запроса
                    string query = "SELECT PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION " +
                        "FROM PRODUCTION_OPERATION INNER JOIN(MODEL_AND_SIZE INNER JOIN PRODUCTION_OPERATION_FOR_MODEL " +
                        "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) " +
                        "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION " +
                        "WHERE(((MODEL_AND_SIZE.ID_MODEL_AND_SIZE) = " + a + ")); ";
                    //создание SQL команды
                    command.CommandText = query;
                    //создание обьекта для считывания данных из базы данных
                    OleDbDataReader reader = command.ExecuteReader();
                    //очистка комбобокса перед выводом данных
                    comboBoxNumberOfOperationMaster.Items.Clear();

                    while (reader.Read())
                    {
                        //считывание данных из базы данных и вывод в комбобокс
                        comboBoxNumberOfOperationMaster.Items.Add(reader["ID_PRODUCTION_OPERATION"].ToString());
                    }
                    //закрытие соединения с базой данных
                    myConnection.Close();

                }
                catch (Exception ex)
                {
                    myConnection.Close();
                    MessageBox.Show("Error  " + ex);
                }
            }
           
        }

        private void comboBoxIDWorkerMaster_DropDown(object sender, EventArgs e)
        {
            try
            {
                //открытие подключениия к базе данных
                myConnection.Open();
                //вывод текста индикатора соединения
                lblConnections.Text = "Connection Successful";
                //создание обьекта для инструкции
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                //текст команбы
                string query = "SELECT EMPLOYEE.ID_EMPLOYEE , EMPLOYEE.NAME_EMPLOYEE " +
                    "FROM POSITION_OF_EMPLOYEE INNER JOIN EMPLOYEE ON POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE = EMPLOYEE.ID_POSITION_OF_EMPLOYEE " +
                    "WHERE(((POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE)LIKE 'Швея')); ";
                //создание SQL команды 
                command.CommandText = query;
                //считывание данных из базы данных
                OleDbDataReader reader = command.ExecuteReader();
                //создание обьекта для считывания данных из базы данных
                comboBoxIDWorkerMaster.Items.Clear();

                while (reader.Read())
                {
                    //считывание данных из базы данных и вывод в комбобокс
                    comboBoxIDWorkerMaster.Items.Add(reader["ID_EMPLOYEE"].ToString()+" "+reader["NAME_EMPLOYEE"].ToString());
                }
                //закрытие соединения с базой данных
                myConnection.Close();
            }
            catch (Exception ex)
            {
                //закрытие соединения
                myConnection.Close();
                //вывод сообщения ошибки
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddOperationForWorker_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxNumberOfOrderMaster.Text) || string.IsNullOrEmpty(comboBoxNumberOfModelMaster.Text)
                || string.IsNullOrEmpty(comboBoxNumberOfOperationMaster.Text) || string.IsNullOrEmpty(comboBoxIDWorkerMaster.Text) || string.IsNullOrEmpty(textBoxNumberOfOperation.Text))
            {
                myFunction.MessageBlankFields();
            }
            else
            {
                int order = int.Parse(comboBoxNumberOfOrderMaster.Text + comboBoxNumberOfModelMaster.Text);
                int model = int.Parse(comboBoxNumberOfModelMaster.Text);
                int operation = int.Parse(comboBoxNumberOfOperationMaster.Text);
                int employer = int.Parse(comboBoxIDWorkerMaster.Text.Split(' ')[0]);
                int namberOfOperations = int.Parse(textBoxNumberOfOperation.Text);
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

                    string query = "INSERT INTO [ORDER_OF_PRODCTION_OPERATIONS] " +
                        "(ID_ORDER_LIST_MODEL,	ID_PRODUCTION_OPERATIONS_FOR_MODEL,	ID_EMPLOYEE,	NAMBER_OF_OPERATIONS_IS_DONE, [DATE]) " +
                        "VALUES ('" + order + "' , '" + operationForModel + "' , '" + employer + "' , '" + namberOfOperations + "' , '" + dateTime + "')";

                    command.CommandText = query;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        myFunction.MessageDataSeved();
                        textBoxNumberOfOperation.Clear();
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

        private void buttonGetOperationOfEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string tq = "SELECT " +
                    "ORDER_OF_PRODCTION_OPERATIONS.ID_ORDER_OF_PRODUCTION_OPERATION as [id], " +
                    "ORDER.ID_ORDER as [заказ № ], " +
                    "ORDER_MODEL.ID_MODEL_AND_SIZE as [модель № ], " +
                    "PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION as [операция № ], " +
                    "EMPLOYEE.ID_EMPLOYEE as [исполнитель № ], " +
                    "EMPLOYEE.NAME_EMPLOYEE as [имя исполнителя ], " +
                    "ORDER_OF_PRODCTION_OPERATIONS.NAMBER_OF_OPERATIONS_IS_DONE as [количество выпоненых операций], " +
                    "ORDER_OF_PRODCTION_OPERATIONS.DATE as [Дата] " +
                    "FROM PRODUCTION_OPERATION INNER JOIN(PRODUCTION_OPERATION_FOR_MODEL INNER JOIN([ORDER] INNER JOIN(ORDER_MODEL " +
                    "INNER JOIN(EMPLOYEE INNER JOIN ORDER_OF_PRODCTION_OPERATIONS ON EMPLOYEE.ID_EMPLOYEE = ORDER_OF_PRODCTION_OPERATIONS.ID_EMPLOYEE) " +
                    "ON ORDER_MODEL.ID_ORDER_LIST_MODEL = ORDER_OF_PRODCTION_OPERATIONS.ID_ORDER_LIST_MODEL) ON ORDER.ID_ORDER = ORDER_MODEL.ID_ORDER) " +
                    "ON PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATIONS_FOR_MODEL = ORDER_OF_PRODCTION_OPERATIONS.ID_PRODUCTION_OPERATIONS_FOR_MODEL) " +
                    "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION ";
                string query = null;
                int order = 0;
                int model = 0;

                if (string.IsNullOrEmpty(comboBoxNumberOfOrderForMasterView.Text)) order = 0;
                else order = int.Parse(comboBoxNumberOfOrderForMasterView.Text);
                
                if (string.IsNullOrEmpty(comboBoxNumberOfModelForMaster.Text)) model = 0;
                else model = int.Parse(comboBoxNumberOfModelForMaster.Text);
                
                myConnection.Open();
                lblConnections.Text = "Connection Successful";
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                if (order == 0 && model == 0)
                {
                    query = tq;
                    ErrorMaster1.Text = "";
                }
                else if (model == 0)
                {
                    query= tq + "WHERE((ORDER.ID_ORDER) = " + order + ")";
                    ErrorMaster1.Text = "";
                }
                else
                {
                    query = tq + "WHERE(((ORDER.ID_ORDER) = " + order + ") AND((ORDER_MODEL.ID_MODEL_AND_SIZE) = " + model + "))";
                    ErrorMaster1.Text = "";
                }

                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewMaster.DataSource = dt;

                myConnection.Close();
            }
            catch (Exception ex)
            {
                myConnection.Close();
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
            try
            {
                myConnection.Open();
                lblConnections.Text = "Connection Successful";
                OleDbCommand command = new OleDbCommand();
                command.Connection = myConnection;
                string query = "DELETE ORDER_OF_PRODCTION_OPERATIONS.*, ORDER_OF_PRODCTION_OPERATIONS.ID_ORDER_OF_PRODUCTION_OPERATION " +
                    "FROM ORDER_OF_PRODCTION_OPERATIONS " +
                    "WHERE(((ORDER_OF_PRODCTION_OPERATIONS.ID_ORDER_OF_PRODUCTION_OPERATION) = " + id + ")); ";
                command.CommandText = query;
                if (command.ExecuteNonQuery() == 1)
                {
                    myFunction.MessageDataDeleted();
                }
                myConnection.Close();
                textBoxIdOperanionIsDone.Clear();
                buttonGetOperationOfEmployee_Click(sender, e);
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxNumberOfModelForMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNumberOfOperation_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void textBoxIdOperanionIsDone_KeyPress(object sender, KeyPressEventArgs e)
        {
            myFunction.MyDigitKeyPress(sender, e);
        }

        private void ComboBoxIDWorkerMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}




