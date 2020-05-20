using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingCompanyManagement
{
    class MyFunctions
    {
        public static void MyDigitKeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        public static void ClearCbx(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Text = "";
        }
        public static void MessageBlankFields()
        {
            MessageBox.Show("Усі поля у доному блоці повинні бути заповнені !!");
        }
        public static void MessageNothingToChange()
        {
            MessageBox.Show("Хочаб одне поле повинно бути заповнене! ");
        }
        public static void MessageSomethingWrong()
        {
            MessageBox.Show("Something went wrong please try again!!");
        }

        public static void MessageChooseModel()
        {
            MessageBox.Show("Оберіть або введіть значення для моделі !");
        }
        public static void MessageDataSeved()
        {
            MessageBox.Show("Данні збережено !");
        }
        public static void MessageDataUpdate()
        {
            MessageBox.Show("Данні оновлено !");
        }
        public static void MessageChooseOperation()
        {
            MessageBox.Show("Оберіть або введіть значення для операції !");
        }
        public static void MessageChooseSize()
        {
            MessageBox.Show("Оберіть значення для поля розмір !");
        }
        public static void MessageChooseOrder()
        {
            MessageBox.Show("Оберіть значення для поля замовлення !");
        }
        public static void MessageDataDeleted()
        {
            MessageBox.Show("Запис видалено !");
        }
        public static void MessageDataNotFound()
        {
            MessageBox.Show("Запис не знайдено ! Перевірте коректність введених даних. ");
        }
        public static void MessageAllOperationsIsDone()
        {
            MessageBox.Show("Данні операції виконані у повному овсязі, нема потреби вносити данний запис у базу даних.");
        }
        public static void MessageEnteredDataIsWrong()
        {
            MessageBox.Show("Введені данні перевищують допустиме значення. У базу даних записано максимальне допустиме значення для даного поля. Для перевірки перегляньте базу даних виконаних операцій.");
        }
        public static void MesageServerIsntAlive()
        {
            MessageBox.Show("Немає зв'язку з базою данних!");
        }
        public static void MessageInputStringLength()
        {
            MessageBox.Show("Довжира вхідного тексту перевищує допустиме! Максимальна довжина 255 символів.");
        }
        public static void MessageDataIsntCorrect(string message ="")
        {
            MessageBox.Show($"input data is not correct. {message}");
        }
    }
}
