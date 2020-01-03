using System;
using System.Windows.Forms;

namespace SewingCompanyManagement
{
    public partial class frmManager : Form
    {
        public frmManager()
        {
            InitializeComponent();
        }

        private void buttonViewOrder_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewForManager.DataSource=DataBaseHelper.GetOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewModel_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewForManager.DataSource = DataBaseHelper.GetModels();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewForManager.DataSource = DataBaseHelper.GetEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonViewPositionOfEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewForManager.DataSource = DataBaseHelper.GetPositionsOfEmployee();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }
      
        private void textBoxNumberModelsInOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void textBoxTelephoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void buttonAddNewOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dateTimePickerManager.Text) || string.IsNullOrEmpty(comboBoxNameOfCustomer.Text) 
                    || string.IsNullOrEmpty(textBoxComment.Text) || string.IsNullOrEmpty(textBoxNumberOfModelForNewOrder.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    string date = dateTimePickerManager.Text;
                    int idCustomer = DataBaseHelper.GetCustomerId(comboBoxNameOfCustomer.Text.Split(' ')[0]+" "
                        +comboBoxNameOfCustomer.Text.Split(' ')[1]+" "
                        + comboBoxNameOfCustomer.Text.Split(' ')[2]) ;
                    string comment = textBoxComment.Text;
                    int numberOfModels= int.Parse(textBoxNumberOfModelForNewOrder.Text);

                    if (DataBaseHelper.AddNewOrder(date, idCustomer, comment, numberOfModels))
                    {   
                        MyFunctions.ClearCbx(comboBoxNameOfCustomer);
                        textBoxComment.Clear();
                        textBoxNumberOfModelForNewOrder.Clear();
                        DataBaseHelper.UpdateNumberOfOrderForCustomer(idCustomer);
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

        private void buttonAddModelForOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxOrderNumber.Text) || string.IsNullOrEmpty(comboBoxModelNumber.Text) || string.IsNullOrEmpty(comboBoxModelSize.Text) || string.IsNullOrEmpty(textBoxNumberModelsInOrder.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int idOrder = int.Parse(comboBoxOrderNumber.Text + comboBoxModelNumber.Text + comboBoxModelSize.Text);
                    int order = int.Parse(comboBoxOrderNumber.Text);
                    int modelAndSize = int.Parse(comboBoxModelNumber.Text + comboBoxModelSize.Text);
                    int number = int.Parse(textBoxNumberModelsInOrder.Text);

                    if (DataBaseHelper.AddNewModelForOrder(idOrder, order, modelAndSize, number))
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

        private void comboBoxOrderNumber_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxOrderNumber);
                comboBoxOrderNumber.Items.AddRange(DataBaseHelper.GetNumberOfOrder().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelNumber_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxModelNumber);
                comboBoxModelNumber.Items.AddRange(DataBaseHelper.GetNumberOfModel().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelSize_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxModelNumber.Text))
                {
                    MyFunctions.MessageChooseModel();
                }
                else
                {
                    int model = int.Parse(comboBoxModelNumber.Text);
                    MyFunctions.ClearCbx(comboBoxModelSize);
                    comboBoxModelSize.Items.AddRange(DataBaseHelper.GetNumberOfModelAndSizeByModel(model).ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxModelInOrder_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxModelInOrder);
                comboBoxModelInOrder.Items.AddRange(DataBaseHelper.GetNumberOfOrder().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonListModelInOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int order = 0;
                if (string.IsNullOrEmpty(comboBoxModelInOrder.Text))
                {
                    order = -1;                //если в строка пустая значение заказа =-1 
                }
                else
                {
                    order = int.Parse(comboBoxModelInOrder.Text); //иначе значение заказа равно номеру заказа
                }
                dataGridViewForManager.DataSource = DataBaseHelper.GetModelsForOrder(order); //заполнение таблицы полученными данными
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void buttonAddNewEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxNameEmployee.Text) || string.IsNullOrEmpty(textBoxTelephoneNumber.Text) || string.IsNullOrEmpty(comboBoxPositionOfEmployee.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    string name = textBoxNameEmployee.Text;
                    int phone = int.Parse(textBoxTelephoneNumber.Text);
                    int position = int.Parse(comboBoxPositionOfEmployee.Text.Split(' ')[0]);

                    if (DataBaseHelper.AddNewEmployee(name, phone, position) == true)
                    {
                        MyFunctions.MessageDataSeved();
                        textBoxNameEmployee.Clear();
                        textBoxTelephoneNumber.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void comboBoxPositionOfEmployee_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxPositionOfEmployee);
                comboBoxPositionOfEmployee.Items.AddRange(DataBaseHelper.GetNumberOfPosition().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        //Добавление новой должности
        private void buttonAddNewPosition_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxIdPosition.Text) || string.IsNullOrEmpty(textBoxNamePosition.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int idPosition = int.Parse(textBoxIdPosition.Text);
                    string namePositin = textBoxNamePosition.Text;
                    if (DataBaseHelper.AddNewPosition(idPosition, namePositin)==true)
                    {
                        MyFunctions.MessageDataSeved();
                        textBoxIdPosition.Clear();
                        textBoxNamePosition.Clear();
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

        private void ButtonDismiss_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxIDEmployeeForDismiss.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    int IdEmployee = int.Parse(textBoxIDEmployeeForDismiss.Text);
                    int status = 2;//2=уволен, 1=нанят на работу
                    if (DataBaseHelper.SetStatusForEmployee(IdEmployee, status)==true)
                    {
                        MyFunctions.MessageDataSeved();
                        textBoxIDEmployeeForDismiss.Clear();
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

        private void TextBoxIDEmployeeForDismiss_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }

        private void buttonAddNewCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxNameOfCustomer.Text) || string.IsNullOrEmpty(textBoxPhoneOfCustomer.Text))
                {
                    MyFunctions.MessageBlankFields();
                }
                else
                {
                    string name = textBoxNameOfCustomer.Text;
                    int phone = int.Parse(textBoxPhoneOfCustomer.Text);

                    if (DataBaseHelper.AddNewCustomer(name,phone,0))
                    {
                        MyFunctions.MessageDataSeved();
                        textBoxNameOfCustomer.Clear();
                        textBoxPhoneOfCustomer.Clear();
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

        private void comboBoxNameOfCustomer_DropDown(object sender, EventArgs e)
        {
            try
            {
                MyFunctions.ClearCbx(comboBoxNameOfCustomer);
                comboBoxNameOfCustomer.Items.AddRange(DataBaseHelper.GetNumberOCustomer().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }

        private void textBoxNumberOfModelForNewOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyFunctions.MyDigitKeyPress(sender, e);
        }
    }
}
