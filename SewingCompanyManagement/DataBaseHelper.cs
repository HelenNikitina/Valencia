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
        private static string dataSource = "TrueDB_01.mdb";
        private static string userId = "admin";
        private static string password = "";
        //private static string provider = "";
        private static OleDbConnection GetNewConnection()
        {
            return new OleDbConnection($@"Provider={provider};Data Source={dataSource};User Id={userId};Password={password};");
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
                    "WHERE((ID_ORDER_LIST_MODEL = " + order + ") " +
                    "AND (ID_PRODUCTION_OPERATIONS_FOR_MODEL = " + idOperation + "));",con);
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
                    "WHERE(ID_PRODUCTION_OPERATION=" + operation + ") " +
                    "AND (ID_MODEL_AND_SIZE=" + modelAndSize + ");", con);
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
                    "WHERE(ID_ORDER_LIST_MODEL=" + orderModelSize + ");", con);
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
                    "WHERE(((ORDER_OF_PRODCTION_OPERATIONS.ID_ORDER_OF_PRODUCTION_OPERATION) = " + Id + ")); ", con);
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
                    query = tq + "WHERE((ORDER.ID_ORDER) = " + order + ")";
                }
                else
                {
                    query = tq + "WHERE(((ORDER.ID_ORDER) = " + order + ") AND((ORDER_MODEL.ID_MODEL_AND_SIZE) = " + model + "))";
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
                    query = tq + "WHERE((ORDER.ID_ORDER) = " + order + ");"; 
                }
                else
                {
                    query = tq + "WHERE(((ORDER.ID_ORDER) = " + order + ") AND((ORDER_MODEL.ID_MODEL_AND_SIZE) = " + model + "))";
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
        public static List<string> GetNumberOfModelByOrder(int numberOfOrder)
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT MODEL_AND_SIZE.ID_MODEL_AND_SIZE " +
                        "FROM[ORDER] INNER JOIN(MODEL_AND_SIZE INNER JOIN ORDER_MODEL ON MODEL_AND_SIZE.ID_MODEL_AND_SIZE = ORDER_MODEL.ID_MODEL_AND_SIZE) " +
                        "ON ORDER.ID_ORDER = ORDER_MODEL.ID_ORDER " +
                    "WHERE((ORDER.ID_ORDER) = " + numberOfOrder + ");", con);
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
                        "WHERE(((MODEL_AND_SIZE.ID_MODEL_AND_SIZE) = " + numberOfModel + ")); ", con);
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
        public static List<string> GetNumberIdAndNameOfEmployee()
        {
            List<string> result = new List<string>();
            using (var con = GetNewConnection())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT EMPLOYEE.ID_EMPLOYEE , EMPLOYEE.NAME_EMPLOYEE " +
                    "FROM POSITION_OF_EMPLOYEE INNER JOIN EMPLOYEE ON POSITION_OF_EMPLOYEE.ID_POSITION_OF_EMPLOYEE = EMPLOYEE.ID_POSITION_OF_EMPLOYEE " +
                    "WHERE(((POSITION_OF_EMPLOYEE.NAME_POSITION_OF_EMPLOYEE)LIKE 'Швея') AND (EMPLOYEE.STATUS=1)); ", con);
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
    }
}
