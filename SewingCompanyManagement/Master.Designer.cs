namespace SewingCompanyManagement
{
    partial class frmMaster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGetOperationMaster = new System.Windows.Forms.Button();
            this.groupBoxMaster = new System.Windows.Forms.GroupBox();
            this.comboBoxIDWorkerMaster = new System.Windows.Forms.ComboBox();
            this.comboBoxNumberOfOperationMaster = new System.Windows.Forms.ComboBox();
            this.comboBoxNumberOfModelMaster = new System.Windows.Forms.ComboBox();
            this.comboBoxNumberOfOrderMaster = new System.Windows.Forms.ComboBox();
            this.buttonAddOperationForWorker = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNumberOfOperation = new System.Windows.Forms.TextBox();
            this.lblConnections = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewMaster = new System.Windows.Forms.DataGridView();
            this.comboBoxNumberOfOrderForMasterView = new System.Windows.Forms.ComboBox();
            this.comboBoxNumberOfModelForMaster = new System.Windows.Forms.ComboBox();
            this.buttonGetOperationOfEmployee = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonDelRowOerationIsDone = new System.Windows.Forms.Button();
            this.textBoxIdOperanionIsDone = new System.Windows.Forms.TextBox();
            this.ErrorMaster1 = new System.Windows.Forms.Label();
            this.groupBoxMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaster)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGetOperationMaster
            // 
            this.buttonGetOperationMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetOperationMaster.Location = new System.Drawing.Point(360, 678);
            this.buttonGetOperationMaster.Name = "buttonGetOperationMaster";
            this.buttonGetOperationMaster.Size = new System.Drawing.Size(278, 31);
            this.buttonGetOperationMaster.TabIndex = 2;
            this.buttonGetOperationMaster.Text = "Перегляд операцій для виконання";
            this.buttonGetOperationMaster.UseVisualStyleBackColor = true;
            this.buttonGetOperationMaster.Click += new System.EventHandler(this.buttonGetOperationMaster_Click);
            // 
            // groupBoxMaster
            // 
            this.groupBoxMaster.Controls.Add(this.comboBoxIDWorkerMaster);
            this.groupBoxMaster.Controls.Add(this.comboBoxNumberOfOperationMaster);
            this.groupBoxMaster.Controls.Add(this.comboBoxNumberOfModelMaster);
            this.groupBoxMaster.Controls.Add(this.comboBoxNumberOfOrderMaster);
            this.groupBoxMaster.Controls.Add(this.buttonAddOperationForWorker);
            this.groupBoxMaster.Controls.Add(this.label5);
            this.groupBoxMaster.Controls.Add(this.label4);
            this.groupBoxMaster.Controls.Add(this.label3);
            this.groupBoxMaster.Controls.Add(this.label2);
            this.groupBoxMaster.Controls.Add(this.label1);
            this.groupBoxMaster.Controls.Add(this.textBoxNumberOfOperation);
            this.groupBoxMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxMaster.Location = new System.Drawing.Point(13, 6);
            this.groupBoxMaster.Name = "groupBoxMaster";
            this.groupBoxMaster.Size = new System.Drawing.Size(316, 330);
            this.groupBoxMaster.TabIndex = 5;
            this.groupBoxMaster.TabStop = false;
            this.groupBoxMaster.Text = "Додати виконані операції";
            this.groupBoxMaster.Enter += new System.EventHandler(this.groupBoxMaster_Enter);
            // 
            // comboBoxIDWorkerMaster
            // 
            this.comboBoxIDWorkerMaster.FormattingEnabled = true;
            this.comboBoxIDWorkerMaster.Location = new System.Drawing.Point(33, 187);
            this.comboBoxIDWorkerMaster.Name = "comboBoxIDWorkerMaster";
            this.comboBoxIDWorkerMaster.Size = new System.Drawing.Size(262, 24);
            this.comboBoxIDWorkerMaster.TabIndex = 14;
            this.comboBoxIDWorkerMaster.DropDown += new System.EventHandler(this.comboBoxIDWorkerMaster_DropDown);
            this.comboBoxIDWorkerMaster.SelectedIndexChanged += new System.EventHandler(this.ComboBoxIDWorkerMaster_SelectedIndexChanged);
            // 
            // comboBoxNumberOfOperationMaster
            // 
            this.comboBoxNumberOfOperationMaster.FormattingEnabled = true;
            this.comboBoxNumberOfOperationMaster.Location = new System.Drawing.Point(33, 143);
            this.comboBoxNumberOfOperationMaster.Name = "comboBoxNumberOfOperationMaster";
            this.comboBoxNumberOfOperationMaster.Size = new System.Drawing.Size(262, 24);
            this.comboBoxNumberOfOperationMaster.TabIndex = 13;
            this.comboBoxNumberOfOperationMaster.DropDown += new System.EventHandler(this.comboBoxNumberOfOperationMaster_DropDown);
            this.comboBoxNumberOfOperationMaster.SelectedIndexChanged += new System.EventHandler(this.ComboBoxNumberOfOperationMaster_SelectedIndexChanged);
            // 
            // comboBoxNumberOfModelMaster
            // 
            this.comboBoxNumberOfModelMaster.FormattingEnabled = true;
            this.comboBoxNumberOfModelMaster.Location = new System.Drawing.Point(33, 99);
            this.comboBoxNumberOfModelMaster.Name = "comboBoxNumberOfModelMaster";
            this.comboBoxNumberOfModelMaster.Size = new System.Drawing.Size(262, 24);
            this.comboBoxNumberOfModelMaster.TabIndex = 12;
            this.comboBoxNumberOfModelMaster.DropDown += new System.EventHandler(this.comboBoxNumberOfModelMaster_DropDown);
            // 
            // comboBoxNumberOfOrderMaster
            // 
            this.comboBoxNumberOfOrderMaster.FormattingEnabled = true;
            this.comboBoxNumberOfOrderMaster.Location = new System.Drawing.Point(33, 55);
            this.comboBoxNumberOfOrderMaster.Name = "comboBoxNumberOfOrderMaster";
            this.comboBoxNumberOfOrderMaster.Size = new System.Drawing.Size(262, 24);
            this.comboBoxNumberOfOrderMaster.TabIndex = 11;
            this.comboBoxNumberOfOrderMaster.DropDown += new System.EventHandler(this.comboBoxNumberOfOrderMaster_DropDown);
            // 
            // buttonAddOperationForWorker
            // 
            this.buttonAddOperationForWorker.Location = new System.Drawing.Point(33, 281);
            this.buttonAddOperationForWorker.Name = "buttonAddOperationForWorker";
            this.buttonAddOperationForWorker.Size = new System.Drawing.Size(262, 27);
            this.buttonAddOperationForWorker.TabIndex = 10;
            this.buttonAddOperationForWorker.Text = "Додати";
            this.buttonAddOperationForWorker.UseVisualStyleBackColor = true;
            this.buttonAddOperationForWorker.Click += new System.EventHandler(this.buttonAddOperationForWorker_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Кількість виконаних операцій";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Табельний номер виконавця";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Номер опрерації";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Номер моделі";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Номер замовлення";
            // 
            // textBoxNumberOfOperation
            // 
            this.textBoxNumberOfOperation.Location = new System.Drawing.Point(33, 233);
            this.textBoxNumberOfOperation.Name = "textBoxNumberOfOperation";
            this.textBoxNumberOfOperation.Size = new System.Drawing.Size(262, 22);
            this.textBoxNumberOfOperation.TabIndex = 4;
            this.textBoxNumberOfOperation.TextChanged += new System.EventHandler(this.TextBoxNumberOfOperation_TextChanged);
            this.textBoxNumberOfOperation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumberOfOperation_KeyPress);
            // 
            // lblConnections
            // 
            this.lblConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnections.AutoSize = true;
            this.lblConnections.Location = new System.Drawing.Point(12, 691);
            this.lblConnections.Name = "lblConnections";
            this.lblConnections.Size = new System.Drawing.Size(37, 13);
            this.lblConnections.TabIndex = 15;
            this.lblConnections.Text = "Status";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(500, 630);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Номер моделі";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(360, 631);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Номер замовлення";
            // 
            // dataGridViewMaster
            // 
            this.dataGridViewMaster.AllowUserToAddRows = false;
            this.dataGridViewMaster.AllowUserToDeleteRows = false;
            this.dataGridViewMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMaster.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaster.Location = new System.Drawing.Point(335, 14);
            this.dataGridViewMaster.Name = "dataGridViewMaster";
            this.dataGridViewMaster.ReadOnly = true;
            this.dataGridViewMaster.Size = new System.Drawing.Size(740, 608);
            this.dataGridViewMaster.TabIndex = 8;
            // 
            // comboBoxNumberOfOrderForMasterView
            // 
            this.comboBoxNumberOfOrderForMasterView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxNumberOfOrderForMasterView.FormattingEnabled = true;
            this.comboBoxNumberOfOrderForMasterView.Location = new System.Drawing.Point(360, 651);
            this.comboBoxNumberOfOrderForMasterView.Name = "comboBoxNumberOfOrderForMasterView";
            this.comboBoxNumberOfOrderForMasterView.Size = new System.Drawing.Size(126, 21);
            this.comboBoxNumberOfOrderForMasterView.TabIndex = 9;
            this.comboBoxNumberOfOrderForMasterView.DropDown += new System.EventHandler(this.comboBoxNumberOfOrderForMasterView_DropDown);
            // 
            // comboBoxNumberOfModelForMaster
            // 
            this.comboBoxNumberOfModelForMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxNumberOfModelForMaster.FormattingEnabled = true;
            this.comboBoxNumberOfModelForMaster.Location = new System.Drawing.Point(499, 651);
            this.comboBoxNumberOfModelForMaster.Name = "comboBoxNumberOfModelForMaster";
            this.comboBoxNumberOfModelForMaster.Size = new System.Drawing.Size(139, 21);
            this.comboBoxNumberOfModelForMaster.TabIndex = 10;
            this.comboBoxNumberOfModelForMaster.DropDown += new System.EventHandler(this.comboBoxNumberOfModelForMasterView_DropDown);
            this.comboBoxNumberOfModelForMaster.SelectedIndexChanged += new System.EventHandler(this.comboBoxNumberOfModelForMaster_SelectedIndexChanged);
            // 
            // buttonGetOperationOfEmployee
            // 
            this.buttonGetOperationOfEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetOperationOfEmployee.Location = new System.Drawing.Point(644, 678);
            this.buttonGetOperationOfEmployee.Name = "buttonGetOperationOfEmployee";
            this.buttonGetOperationOfEmployee.Size = new System.Drawing.Size(242, 31);
            this.buttonGetOperationOfEmployee.TabIndex = 11;
            this.buttonGetOperationOfEmployee.Text = "Перегляд виконаних операцій у замовленні";
            this.buttonGetOperationOfEmployee.UseVisualStyleBackColor = true;
            this.buttonGetOperationOfEmployee.Click += new System.EventHandler(this.buttonGetOperationOfEmployee_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.buttonDelRowOerationIsDone);
            this.groupBox1.Controls.Add(this.textBoxIdOperanionIsDone);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(15, 343);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 155);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Видалити запис про виконану операцію";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Номер запису (id)";
            // 
            // buttonDelRowOerationIsDone
            // 
            this.buttonDelRowOerationIsDone.Location = new System.Drawing.Point(31, 90);
            this.buttonDelRowOerationIsDone.Name = "buttonDelRowOerationIsDone";
            this.buttonDelRowOerationIsDone.Size = new System.Drawing.Size(262, 32);
            this.buttonDelRowOerationIsDone.TabIndex = 1;
            this.buttonDelRowOerationIsDone.Text = "Видалити";
            this.buttonDelRowOerationIsDone.UseVisualStyleBackColor = true;
            this.buttonDelRowOerationIsDone.Click += new System.EventHandler(this.buttonDelRowOerationIsDone_Click);
            // 
            // textBoxIdOperanionIsDone
            // 
            this.textBoxIdOperanionIsDone.Location = new System.Drawing.Point(31, 48);
            this.textBoxIdOperanionIsDone.Name = "textBoxIdOperanionIsDone";
            this.textBoxIdOperanionIsDone.Size = new System.Drawing.Size(262, 22);
            this.textBoxIdOperanionIsDone.TabIndex = 0;
            this.textBoxIdOperanionIsDone.TextChanged += new System.EventHandler(this.textBoxIdOperanionIsDone_TextChanged);
            this.textBoxIdOperanionIsDone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIdOperanionIsDone_KeyPress);
            // 
            // ErrorMaster1
            // 
            this.ErrorMaster1.AutoSize = true;
            this.ErrorMaster1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMaster1.ForeColor = System.Drawing.Color.Red;
            this.ErrorMaster1.Location = new System.Drawing.Point(654, 651);
            this.ErrorMaster1.Name = "ErrorMaster1";
            this.ErrorMaster1.Size = new System.Drawing.Size(13, 20);
            this.ErrorMaster1.TabIndex = 17;
            this.ErrorMaster1.Text = " \r\n";
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 713);
            this.Controls.Add(this.ErrorMaster1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblConnections);
            this.Controls.Add(this.buttonGetOperationOfEmployee);
            this.Controls.Add(this.comboBoxNumberOfModelForMaster);
            this.Controls.Add(this.comboBoxNumberOfOrderForMasterView);
            this.Controls.Add(this.dataGridViewMaster);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBoxMaster);
            this.Controls.Add(this.buttonGetOperationMaster);
            this.Name = "frmMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master";
            this.Load += new System.EventHandler(this.frmMaster_Load);
            this.groupBoxMaster.ResumeLayout(false);
            this.groupBoxMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaster)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonGetOperationMaster;
        private System.Windows.Forms.GroupBox groupBoxMaster;
        private System.Windows.Forms.Button buttonAddOperationForWorker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewMaster;
        private System.Windows.Forms.ComboBox comboBoxIDWorkerMaster;
        private System.Windows.Forms.ComboBox comboBoxNumberOfOperationMaster;
        private System.Windows.Forms.ComboBox comboBoxNumberOfModelMaster;
        private System.Windows.Forms.ComboBox comboBoxNumberOfOrderMaster;
        private System.Windows.Forms.TextBox textBoxNumberOfOperation;
        private System.Windows.Forms.ComboBox comboBoxNumberOfOrderForMasterView;
        private System.Windows.Forms.ComboBox comboBoxNumberOfModelForMaster;
        private System.Windows.Forms.Label lblConnections;
        private System.Windows.Forms.Button buttonGetOperationOfEmployee;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonDelRowOerationIsDone;
        private System.Windows.Forms.TextBox textBoxIdOperanionIsDone;
        private System.Windows.Forms.Label ErrorMaster1;
    }
}