using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace SewingCompanyManagement
{
    class DataBaseHelper
    {
        private static string provider = "Microsoft.Jet.OLEDB.4.0";
        private static string dataSource = @"D:\GDrveSpecowka\Develop\#HELEN PROJECTS\SewingCompany\SewingCompanyManagement\TrueDB_01.mdb";
        private static string userId = "admin";
        private static string password = "";
        //private static string provider = "";
        private static OleDbConnection GetNewConnection()
        {
            return new OleDbConnection($"Provider={provider};Data Source={dataSource};User Id={userId};Password={password};");
        }
        public static bool IsServerAlive()
        {
            try
            {
                using (var con = GetNewConnection())
                {
                    con.Open();
                    con.Close();
                }
            }
            catch (OleDbException)
            {
                return false;
            }

            return true;
        }
        public static int Master_getNamberOfOperationInOrder(int order, int modelAndSize, int operation)
        {
            int value = 0;
            int idOperation = Master_getIdOperationForModel(operation, modelAndSize);
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(
                    "SELECT SUM(NAMBER_OF_OPERATIONS_IS_DONE) " +
                    "FROM ORDER_OF_PRODCTION_OPERATIONS " +
                    $"WHERE((ID_ORDER_LIST_MODEL = {order}) " +
                    $"AND (ID_PRODUCTION_OPERATIONS_FOR_MODEL = {idOperation}));",con);
                var answer = cmd.ExecuteScalar().ToString();
                
                if (!string.IsNullOrWhiteSpace(answer) && !int.TryParse(answer, out value))
                {
                    MyFunctions.MessageDataIsntCorrect(answer);
                }
                con.Close();
            }
            return value;
        }
        
        public static int Master_getIdOperationForModel(int operation, int modelAndSize)
        {
            int result = 0;
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(
                    "SELECT ID_PRODUCTION_OPERATIONS_FOR_MODEL " +
                    "FROM PRODUCTION_OPERATION_FOR_MODEL " +
                    $"WHERE(ID_PRODUCTION_OPERATION= {operation}) " +
                    $"AND (ID_MODEL_AND_SIZE= {modelAndSize});", con);
                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        result = (int)reader.GetValue(0);
                    }
                    else
                    {
                        MyFunctions.MessageDataNotFound();
                    }
                }
                con.Close();
            }
            return result;
        }
        public static int Master_getNamberOfModelToDo(int orderModelSize)
        {
            int result = 0;
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(
                    "SELECT NUMBER_OF_MODELS " +
                    "FROM ORDER_MODEL " +
                    $"WHERE(ID_ORDER_LIST_MODEL= {orderModelSize});", con);
                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        result = (int)reader.GetValue(0);
                    }
                    else
                    {
                        MyFunctions.MessageDataNotFound();
                    }
                }
                con.Close();
            }
            return result;
        }
        public static void Master_DelRowOerationIsDoneById(int Id)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(
                    "DELETE ORDER_OF_PRODCTION_OPERATIONS.*, ORDER_OF_PRODCTION_OPERATIONS.ID_ORDER_OF_PRODUCTION_OPERATION " +
                    "FROM ORDER_OF_PRODCTION_OPERATIONS " +
                    $"WHERE(((ORDER_OF_PRODCTION_OPERATIONS.ID_ORDER_OF_PRODUCTION_OPERATION) = {Id})); ", con);
                if(cmd.ExecuteNonQuery()==1)
                {
                    MyFunctions.MessageDataDeleted();
                }
                con.Close();
            }
        }
        public static DataTable Master_GetOperationsEmployeeDone(int order, int model)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
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

                if (order == 0 && model == 0)
                {
                    query = tq;
                }
                else if (model == 0)
                {
                    query = tq + $"WHERE((ORDER.ID_ORDER) = {order})";
                }
                else
                {
                    query = tq + $"WHERE(((ORDER.ID_ORDER) = {order}) AND((ORDER_MODEL.ID_MODEL_AND_SIZE) = {model}))";
                }

                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static DataTable Master_GetOperationsToDo(int order, int model)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string tq = "SELECT ORDER.ID_ORDER as [Замовлення №], " +
                        "ORDER_MODEL.ID_MODEL_AND_SIZE as [Модель №], " +
                        "PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION as [Операція №], " +
                        "PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION as [Назва операції], " +
                        "ORDER_MODEL.NUMBER_OF_MODELS as [Кількість операцій у замовленні] " +
                        "FROM PRODUCTION_OPERATION INNER JOIN ([ORDER] INNER JOIN ((MODEL_AND_SIZE INNER JOIN PRODUCTION_OPERATION_FOR_MODEL " +
                        "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) INNER JOIN ORDER_MODEL " +
                        "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) ON ORDER.ID_ORDER = ORDER_MODEL.ID_ORDER) " +
                        "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION ";
                string query = null;

                if (order == 0 && model == 0)
                {
                    query = tq;
                }
                else if (model == 0)
                {
                    query = tq + $"WHERE((ORDER.ID_ORDER) = {order});"; 
                }
                else
                {
                    query = tq + $"WHERE(((ORDER.ID_ORDER) = {order}) AND((ORDER_MODEL.ID_MODEL_AND_SIZE) = {model}))";
                }

                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static List<string> GetNumberOfOrder()
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(@"SELECT ORDER.ID_ORDER FROM [ORDER]", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_ORDER"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }
        public static List<string> GetNumberOCustomer()
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM CUSTOMER", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["NAME_OF_CUSTOMER"].ToString()+" "+ reader["TELEPHONE_NUMBER"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }
        public static List<string> GetNumberOfModel()
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(@"SELECT * FROM MODEL ", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_MODEL"].ToString() + " " + reader["SHORT_NAME_OF_MODEL"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }
        public static List<string> GetNumberOfModelAndSizeByModel(int model)
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(@"SELECT MODEL_AND_SIZE.ID_MODEL_SIZE_STATURE, MODEL_AND_SIZE.ID_MODEL " +
                        "FROM MODEL_AND_SIZE " +
                        $"WHERE(((MODEL_AND_SIZE.ID_MODEL) = {model})); ", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_MODEL_SIZE_STATURE"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }
        public static List<string> GetNumberOfModelAndSize()
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(@"SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE FROM MODEL_AND_SIZE ", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_MODEL_AND_SIZE"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }

        public static List<string> GetNumberOfModelByOrder(int numberOfOrder)
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE " +
                        "FROM[ORDER] INNER JOIN(MODEL_AND_SIZE INNER JOIN ORDER_MODEL ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) " +
                        "ON ORDER.ID_ORDER = ORDER_MODEL.ID_ORDER " +
                        $"WHERE((ORDER.ID_ORDER) = {numberOfOrder});", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_MODEL_AND_SIZE"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }
        public static List<string> GetNumberOfOperationByModel(int numberOfModel)
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION " +
                        "FROM PRODUCTION_OPERATION INNER JOIN(MODEL_AND_SIZE INNER JOIN PRODUCTION_OPERATION_FOR_MODEL " +
                        "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) " +
                        "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION " +
                        $"WHERE(((MODEL_AND_SIZE.ID_MODEL_AND_SIZE) = {numberOfModel})); ", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_PRODUCTION_OPERATION"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }
        public static int GetProductionOperationForModel(int operation, int model)
        {
            int result = 0;
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATIONS_FOR_MODEL, PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION, MODEL_AND_SIZE.ID_MODEL_AND_SIZE " +
                        "FROM PRODUCTION_OPERATION INNER JOIN(PRODUCTION_OPERATION_FOR_MODEL INNER JOIN MODEL_AND_SIZE ON PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE = MODEL_AND_SIZE.ID_MODEL_AND_SIZE) " +
                        "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION " +
                        $"WHERE(((PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION) = {operation}) AND((MODEL_AND_SIZE.ID_MODEL_AND_SIZE) = {model})); ", con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = int.Parse(reader["ID_PRODUCTION_OPERATIONS_FOR_MODEL"].ToString());
                    }
                    
                }
                con.Close();
            }
            return result;
        }
        public static bool InsertIntoOrderOfProductionOperation(int order, int operationForModel, int employer, int namberOfOperations)
        {
            string dateTime = DateTime.Now.ToShortDateString();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO [ORDER_OF_PRODCTION_OPERATIONS] " +
                        "(ID_ORDER_LIST_MODEL,	ID_PRODUCTION_OPERATIONS_FOR_MODEL,	ID_EMPLOYEE,	NAMBER_OF_OPERATIONS_IS_DONE, [DATE]) " +
                        $"VALUES ({order}, {operationForModel}, {employer}, {namberOfOperations}, {dateTime})", con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static bool AddNewEmployee(string name, int phone, int position)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO [EMPLOYEE] (NAME_EMPLOYEE, PHONE_EMPLOYEE, ID_POSITION_OF_EMPLOYEE) " +
                    $"VALUES ('{name}', { phone }, { position}) ", con); ;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static bool AddNewPosition(int idPosition, string namePositin)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO [POSITION_OF_EMPLOYEE] (ID_POSITION_OF_EMPLOYEE, NAME_POSITION_OF_EMPLOYEE) " +
                    $"VALUES ({idPosition}, '{namePositin}') ", con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static bool AddNewOrder(string date, int customerID, string comment, int numberOfModels)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO [ORDER](DATE_OF_ORDER, ID_OF_CUSTOMER, [COMMENT], NAMBER_OF_MODELS) " +
                    $"VALUES ('{date}', {customerID}, '{comment}', {numberOfModels}); ", con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static bool AddNewModelForOrder(int idOrder,int order, int modelAndSize,int number)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO ORDER_MODEL " +
                    $"VALUES ({idOrder}, {order}, {modelAndSize}, {number}); ", con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static bool AddNewCustomer(string name, int phone, int orderCount)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO CUSTOMER(NAME_OF_CUSTOMER, TELEPHONE_NUMBER, NUMBER_OF_ORDERS) " +
                    $"VALUES ('{name}', {phone}, {orderCount}); ", con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static bool UpdateNumberOfOrderForCustomer(int customerId)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE CUSTOMER SET NUMBER_OF_ORDERS=NUMBER_OF_ORDERS+1 " +
                    $"WHERE ID = {customerId};", con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static int GetCustomerId(string name)
        {
            using (var con = GetNewConnection())
            {
                int result = 0;
                con.Open();
                OleDbCommand cmd = new OleDbCommand($"SELECT CUSTOMER.ID FROM CUSTOMER WHERE NAME_OF_CUSTOMER='{name}';", con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                       result = int.Parse(reader["ID"].ToString());
                    }

                }
                con.Close();
                return result;
            }
        }


        public static bool SetStatusForEmployee(int IdEmployee, int status)//1-работает, 2-уволен
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand($"UPDATE EMPLOYEE SET STATUS={status} " +
                        $"WHERE EMPLOYEE.ID_EMPLOYEE={IdEmployee};", con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }

        public static List<string> GetNumberIdAndNameOfEmployee(string position)
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT EMPLOYEE.ID_EMPLOYEE , EMPLOYEE.NAME_EMPLOYEE " +
                    "FROM POSITION_OF_EMPLOYEE INNER JOIN EMPLOYEE ON POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE = EMPLOYEE.ID_POSITION_OF_EMPLOYEE " +
                    $"WHERE(((POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE)LIKE '{position}') AND (EMPLOYEE.STATUS=1)); ", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_EMPLOYEE"].ToString() + " " + reader["NAME_EMPLOYEE"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }
        public static List<string> GetNumberOfPosition()
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE, POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE " +
                    "FROM POSITION_OF_EMPLOYEE; ", con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        result.Add(reader["ID_POSITION_OF_EMPLOYEE"].ToString() + " " + reader["NAME_POSITION_OF_EMPLOYEE"].ToString());
                    }
                }
                con.Close();
            }
            return result;
        }

        public static DataTable GetOrders()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = "SELECT " +
                    "ORDER.ID_ORDER as [Замовлення №], " +
                    "ORDER.DATE_OF_ORDER as [Дата замовлення], " +
                    "CUSTOMER.NAME_OF_CUSTOMER as [Им'я замовника], " +
                    "ORDER.COMMENT as [Коментар], " +
                    "ORDER.NAMBER_OF_MODELS as [Кількість одиниць у замовленні], " +
                    "ORDER.PERSENTAGE_OF_READINESS as [Готовність у процентах] " +
                    "FROM CUSTOMER INNER JOIN[ORDER] ON CUSTOMER.ID = ORDER.ID_OF_CUSTOMER;";
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static DataTable GetModelAndSizes(int model)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = null;
                string qt = "SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE as [Модель и размір №], " +
                    "MODEL_AND_SIZE.ID_MODEL as [Модель №], " +
                    "MODEL_AND_SIZE.ID_MODEL_SIZE_STATURE as [Зріст та Размір] " +
                    "FROM MODEL_AND_SIZE ";
                if (model == -1)
                {
                    query = qt;
                }
                else
                {
                    query = qt + "WHERE (((MODEL_AND_SIZE.ID_MODEL) = " + model + "))";
                }
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }

        public static DataTable GetModels()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = "SELECT " +
                    "[ID_MODEL] as [модель №], " +
                    "[SHORT_NAME_OF_MODEL] as [Назва моделі], " +
                    "[MODEL_DESCRIPTION] as [Опис моделі] " +
                    "FROM [MODEL] ";
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static DataTable GetOperatoins()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = "SELECT PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION as [Операція №], " +
                    "PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION as [Назва операції], " +
                    "PRODUCTION_OPERATION.PRODUCTION_OPERATION_DESCRIPTION as [Опис операції] " +
                    "FROM PRODUCTION_OPERATION ";
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }

        public static DataTable GetEmployees()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = "SELECT EMPLOYEE.ID_EMPLOYEE as [Табельний номер працівника], " +
                    "EMPLOYEE.NAME_EMPLOYEE as [ПІП працівника], " +
                    "EMPLOYEE.PHONE_EMPLOYEE as [Номер телефона працівника], " +
                    "POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE as [Посада працівника], " +
                    "EMPLOYEE_STATUS.NAME_OF_STATUS as [Статус працівника]" +
                    "FROM EMPLOYEE_STATUS INNER JOIN (POSITION_OF_EMPLOYEE INNER JOIN EMPLOYEE ON " +
                    "POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE = EMPLOYEE.ID_POSITION_OF_EMPLOYEE) ON " +
                    "EMPLOYEE_STATUS.ID_STATUS = EMPLOYEE.STATUS; ";
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static DataTable GetPositionsOfEmployee()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = "SELECT POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE as [Код посади], " +
                    "POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE as [Назва посади] " +
                    "FROM POSITION_OF_EMPLOYEE; ";
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static DataTable GetModelsForOrder(int order)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = null;
                string queryq = "SELECT " +
                    "ORDER_MODEL.ID_ORDER as [Номер замовлення], " +
                    "MODEL_AND_SIZE.ID_MODEL as [Номер моделі], " +
                    "MODEL.SHORT_NAME_OF_MODEL as [Назва моделі], " +
                    "ORDER_MODEL.ID_MODEL_AND_SIZE as [Модель та розмір], " +
                    "ORDER_MODEL.NUMBER_OF_MODELS as [Кількість моделей у замовленні] " +
                    "FROM MODEL INNER JOIN(MODEL_AND_SIZE INNER JOIN ORDER_MODEL " +
                    "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) " +
                    "ON MODEL.ID_MODEL = MODEL_AND_SIZE.ID_MODEL ";
                if (order==-1)
                {
                    query = queryq;
                }
                else
                {
                    query = queryq + $"WHERE(((ORDER_MODEL.ID_ORDER) = {order})) ";
                }
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static DataTable Technologist_GetOperationsForModel(int model)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            using (var con = GetNewConnection())
            {
                con.Open();
                string query = null;
                string queryq = "SELECT " +
                    "PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATIONS_FOR_MODEL as [ID], " +
                    "MODEL.SHORT_NAME_OF_MODEL as [Назва моделі], " +
                    "MODEL_AND_SIZE.ID_MODEL as [Номер моделі], " +
                    "SIZE_OF_MODEL_STATURE.ID_MODEL_SIZE_STATURE as [Розмір моделі], " +
                    "PRODUCTION_OPERATION.NAME_PRODUCTION_OPERATION as [Назва операції], " +
                    "PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION as [Номер операції ], " +
                    "PRODUCTION_OPERATION_FOR_MODEL.TIME_FOR_PRODUCTION_OPERATION as [Час виконання операції] " +
                    "FROM SIZE_OF_MODEL_STATURE INNER JOIN(PRODUCTION_OPERATION " +
                    "INNER JOIN (MODEL INNER JOIN (MODEL_AND_SIZE " +
                    "INNER JOIN PRODUCTION_OPERATION_FOR_MODEL " +
                    "ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) " +
                    "ON MODEL.ID_MODEL = MODEL_AND_SIZE.ID_MODEL) " +
                    "ON PRODUCTION_OPERATION.ID_PRODUCTION_OPERATION = PRODUCTION_OPERATION_FOR_MODEL.ID_PRODUCTION_OPERATION) " +
                    "ON SIZE_OF_MODEL_STATURE.ID_MODEL_SIZE_STATURE = MODEL_AND_SIZE.ID_MODEL_SIZE_STATURE ";
                if (model == -1)
                {
                    query = queryq;
                }
                else
                {
                    query = queryq + $"WHERE(((PRODUCTION_OPERATION_FOR_MODEL.ID_MODEL_AND_SIZE) = " + model + ")) ";
                }
                OleDbCommand cmd = new OleDbCommand(query);
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = query;
                adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
    }
}
