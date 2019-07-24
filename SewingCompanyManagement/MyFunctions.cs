﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingCompanyManagement
{
    class MyFunctions
    {
        public void MyDigitKeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        public void MessageBlankFields()
        {
            MessageBox.Show("Усі поля повинні бути заповнені !!");
        }
        public void MessageChooseModel()
        {
            MessageBox.Show("Оберіть або введіть значення для моделі !");
        }
        public void MessageDataSeved()
        {
            MessageBox.Show("Данні збережено !");
        }
        public void MessageDataUpdate()
        {
            MessageBox.Show("Данні оновлено !");
        }
        public void MessageChooseOperation()
        {
            MessageBox.Show("Оберіть або введіть значення для операції !");
        }
        public void MessageChooseSize()
        {
            MessageBox.Show("Оберіть значення для поля розмір !");
        }
        public void MessageChooseOrder()
        {
            MessageBox.Show("Оберіть значення для поля замовлення !");
        }
        public void MessageDataDeleted()
        {
            MessageBox.Show("Запис видалено !");
        }
    }
}